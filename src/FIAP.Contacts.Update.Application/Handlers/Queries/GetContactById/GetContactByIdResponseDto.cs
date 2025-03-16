using FIAP.Contacts.Update.Application.Dto;

namespace FIAP.Contacts.Update.Application.Handlers.Queries.GetContactById;

public class GetContactByIdResponseDto
{
    public required ContactDto Contact { get; set; }
}
