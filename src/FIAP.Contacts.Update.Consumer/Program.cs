using FIAP.Contacts.Update.Consumer.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using FIAP.Contacts.Update.Application.Shared;
using FIAP.Contacts.Update.Infra;
using FIAP.Contacts.Update.Consumer.DI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using System.Diagnostics;
using MassTransit.Logging;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;

var host = Host.CreateDefaultBuilder(args)
     .ConfigureLogging(logging =>
     {
         logging.AddConsole();
     })
    .ConfigureServices((builder, services) =>
    {
        services.AddInfraServices(builder.Configuration);
        services.AddApplicationService();
        services.AddConsumers();
        //services.AddMetricsConfig();

        var config = builder.Configuration;

        services.AddMassTransit(x =>
        {
            x.AddConsumer<UpdateContactConsumer>((x) =>
            {
                x.UseMessageRetry(r =>
                {
                    r.Interval(3, TimeSpan.FromMilliseconds(300));
                });

                x.ConcurrentMessageLimit = 1;
            });

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(config.GetValue<string>("RabbitMq:Host"));

                cfg.ReceiveEndpoint(config.GetValue("RabbitMq:QueueName", "contact.update"), e =>
                {
                    e.SetQueueArgument("x-dead-letter-exchange", "");
                    e.SetQueueArgument("x-dead-letter-routing-key", "contact.dlq");

                    e.ConfigureConsumer<UpdateContactConsumer>(context);
                });
            });
        });

        var serviceName = "fiap-contact-update";
        var serviceVersion = "1.0.0";

        // Create a single ActivitySource that can be used throughout the application
        var activitySource = new ActivitySource(serviceName, serviceVersion);
        services.AddSingleton(activitySource);

        var resourceBuilder = ResourceBuilder.CreateDefault()
          .AddService(serviceName: serviceName, serviceVersion: serviceVersion);

        services.AddOpenTelemetry()
               .ConfigureResource(resource => resource.AddService(
                   serviceName: serviceName,
                   serviceVersion: serviceVersion))
               .WithTracing(tracing => tracing
                   .AddSource(serviceName)
                   .AddSource(DiagnosticHeaders.DefaultListenerName)
                   .AddAspNetCoreInstrumentation()
                   .AddHttpClientInstrumentation()
                   .AddEntityFrameworkCoreInstrumentation(x => x.SetDbStatementForText = true) // Add if using EF Core
                   .AddOtlpExporter(options =>
                   {
                       options.Endpoint = new Uri("http://134.122.121.176:4317");
                       options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                   }));

        // Configure OpenTelemetry logging
        services.AddLogging(logging =>
        {
            logging.AddOpenTelemetry(options =>
            {
                options.SetResourceBuilder(resourceBuilder);
                options.IncludeFormattedMessage = true;
                options.IncludeScopes = true;

                options.AddOtlpExporter(exporterOptions =>
                {
                    exporterOptions.Endpoint = new Uri("http://134.122.121.176:4317");
                    exporterOptions.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                });
            });
        });
    })
    .Build();

await host.RunAsync();
