using FIAP.Contacts.Update.Application.Dto;

namespace FIAP.Contacts.Update.Application.Handlers.Commands.UpdateContact.Dto;

public record ContactForUpdateDto(string Name, PhoneDto Phone, string Email, AddressDto Address);
