namespace FIAP.Contacts.Update.Domain.ContactAggregate;

public interface IContactRepository
{
    Task<Contact?> GetById(Guid id, CancellationToken ct);
    Task Update(Contact contact, CancellationToken ct);
    Task SaveChanges(CancellationToken ct);
}
