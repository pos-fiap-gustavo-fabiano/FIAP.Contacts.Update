using FIAP.Contacts.Update.Application.Dto;
using MassTransit;

namespace FIAP.Contacts.Update.Consumer.Dtos;

[MessageUrn("ContactQueueService.Dto:UpdateContactDto")]
public record UpdateContactDto(Guid Id, ContactDto Contact);
