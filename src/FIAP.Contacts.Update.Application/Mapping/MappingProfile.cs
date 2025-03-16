using AutoMapper;
using FIAP.Contacts.Update.Application.Dto;
using FIAP.Contacts.Update.Application.Handlers.Commands.AddContact;
using FIAP.Contacts.Update.Domain.ContactAggregate;

namespace FIAP.Contacts.Update.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddContactRequest, Contact>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Contact, ContactWithIdDto>().ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();

        }
    }
}
