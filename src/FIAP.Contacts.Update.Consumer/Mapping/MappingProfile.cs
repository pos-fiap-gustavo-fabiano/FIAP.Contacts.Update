using AutoMapper;
using FIAP.Contacts.Update.Application.Dto;
using FIAP.Contacts.Update.Application.Handlers.Commands.UpdateContact;
using FIAP.Contacts.Update.Application.Handlers.Commands.UpdateContact.Dto;
using FIAP.Contacts.Update.Consumer.Dtos;
using FIAP.Contacts.Update.Domain.ContactAggregate;

namespace FIAP.Contacts.Update.Consumer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateContactRequest, UpdateContactDto>().ReverseMap();
            CreateMap<Contact, ContactForUpdateDto>().ReverseMap();
            CreateMap<ContactForUpdateDto, Application.Dto.ContactDto>().ReverseMap();
            CreateMap<Contact, Application.Dto.ContactDto>().ReverseMap();
            CreateMap<Contact, ContactWithIdDto>().ReverseMap();
            CreateMap<Phone, Application.Dto.PhoneDto>().ReverseMap();
            CreateMap<Address, Application.Dto.AddressDto>().ReverseMap();

        }
    }
}
