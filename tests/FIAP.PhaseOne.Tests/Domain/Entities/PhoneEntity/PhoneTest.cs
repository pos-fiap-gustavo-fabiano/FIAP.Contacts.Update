using FIAP.Contacts.Update.Domain.ContactAggregate;
using FIAP.Contacts.Update.Tests.Domain.Mock;

namespace FIAP.Contacts.Update.Tests.Domain.Entities.PhoneEntity;

public class PhoneTest : DomainTest
{
    [Fact]
    public void UpdatePhone_WithValidData_Succeeded()
    {
        var ddd = new Random().Next(1, 99);
        var phoneNumber = _faker.Phone.PhoneNumber();

        var phone = PhoneMock.Create();

        phone.Update(ddd, phoneNumber);

        Assert.Equal(ddd, phone.DDD);
        Assert.Equal(phoneNumber, phone.Number);
    }
}