using FIAP.Contacts.Update.Application.Dto;

namespace FIAP.Contacts.Update.Application.Handlers.Queries.GetAllContacts;

public class GetAllContactsResponseDto
{
    public required PaginationDto<ContactWithIdDto> Contacts { get; set; }
}
