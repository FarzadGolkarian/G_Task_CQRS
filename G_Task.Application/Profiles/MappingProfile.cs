using AutoMapper;
using G_Task.Application.DTOs.Addresses;
using G_Task.Application.DTOs.Persons;
using G_Task.Common.Helpers;
using G_Task.Domain;

namespace G_Task.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Persons

            CreateMap<Person, PersonDto>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => $"{s.FirstName} {s.LastName}".Trim()))
                .ForMember(d => d.AddressTypeDescription,
                    o => o.MapFrom(s => s.Addresses.FirstOrDefault() != null
                        ? s.Addresses.FirstOrDefault().AddressType.GetDescription()
                        : null))
                .ReverseMap();


            CreateMap<Person, PersonListDto>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => $"{s.FirstName} {s.LastName}".Trim()))
                .ForMember(d => d.AddressTypeDescription,
                    o => o.MapFrom(s => s.Addresses.FirstOrDefault() != null
                        ? s.Addresses.FirstOrDefault().AddressType.GetDescription()
                        : null))
                .ReverseMap();


            CreateMap<Person, CreatePersonDto>()
              .ForMember(s => s.PersonAddress, ss => ss.MapFrom(src => src.Addresses))
              .ReverseMap();

            CreateMap<Person, UpdatePersonDto>().ReverseMap();


            #endregion


            #region Address

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressListDto>().ReverseMap();
            CreateMap<Address, CreateAddressDto>().ReverseMap();
            CreateMap<Address, UpdateAddressDto>().ReverseMap();


            #endregion
        }

    }
}
