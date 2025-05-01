FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/FIAP.Contacts.Update.Consumer/*.csproj ./
RUN dotnet restore

COPY . ./
WORKDIR /app/src/FIAP.Contacts.Update.Consumer
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /out .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "FIAP.Contacts.Update.Consumer.dll"]
