using Bogus;
using FIAP.Contacts.Update.Domain.ContactAggregate;

namespace FIAP.Contacts.Update.Tests.Domain.Mock;

public static class PhoneMock
{
    private static readonly Faker _faker = new("pt_BR");

    public static Phone Create() =>
        new Phone(new Random().Next(1, 99), _faker.Phone.PhoneNumber());

}