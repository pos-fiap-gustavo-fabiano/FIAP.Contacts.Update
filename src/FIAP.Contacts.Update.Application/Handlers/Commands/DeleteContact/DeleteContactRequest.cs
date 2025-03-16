namespace FIAP.Contacts.Update.Application.Handlers.Commands.DeleteContact;

public class DeleteContactRequest : IRequest<ErrorOr<Deleted>>
{
    public Guid Id { get; set; }
}
