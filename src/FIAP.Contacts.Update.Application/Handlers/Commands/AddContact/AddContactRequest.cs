using FIAP.Contacts.Update.Application.Dto;

namespace FIAP.Contacts.Update.Application.Handlers.Commands.AddContact;

public class AddContactRequest : ContactDto, IRequest<ErrorOr<AddContactResponse>>
{
    
}
