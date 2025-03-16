﻿using AutoMapper;
using FIAP.Contacts.Update.Domain.ContactAggregate;

namespace FIAP.Contacts.Update.Application.Handlers.Commands.AddContact;

public class AddContactHandler(
    IContactRepository contactRepository,
    IMapper mapper) : IRequestHandler<AddContactRequest, ErrorOr<AddContactResponse>>
{
    public async Task<ErrorOr<AddContactResponse>> Handle(
        AddContactRequest request,
        CancellationToken ct)
    {
        var contact = mapper.Map<Contact>(request);

        if (contact is null)
            return Error.Failure(description: "não foi possível criar contato");

        await contactRepository.Add(contact, ct);

        return new AddContactResponse() { Id = contact.Id };
    }
}
