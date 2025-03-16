using AutoMapper;
using FIAP.Contacts.Update.Application.Handlers.Commands.UpdateContact;
using FIAP.Contacts.Update.Application.Handlers.Commands.UpdateContact.Dto;
using FIAP.Contacts.Update.Consumer.Dtos;
using FIAP.Contacts.Update.Consumer.Shared;
using MassTransit;
using MediatR;

namespace FIAP.Contacts.Update.Consumer.Consumers;

public class UpdateContactConsumer : IConsumer<UpdateContactDto>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UpdateContactConsumer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<UpdateContactDto> context)
    {
        var request = new UpdateContactRequest
        { 
            Id = context.Message.Id,
            Contact = _mapper.Map<ContactForUpdateDto>(context.Message.Contact)
        };

        var response = await _mediator.Send(request, context.CancellationToken);

        if (response.IsError)
            throw new RetryException(string.Join(',', response.Errors.Select(x => x.Description)));
    }
}
