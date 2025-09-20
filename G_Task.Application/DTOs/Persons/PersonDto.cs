using G_Task.Application.DTOs.Addresses;
using G_Task.Application.DTOs.Common;
using G_Task.Application.DTOs.Persons.Interfaces;
using G_Task.Common.Helpers;
using G_Task.Domain.Common;
using System.Text.Json.Serialization;

namespace G_Task.Application.DTOs.Persons
{
    public class PersonDto : BaseEntityDto, IPerson
    {
        [JsonIgnore]
        public string FirstName { get; set; }
        [JsonIgnore]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string NationalCode { get; set; }

        public List<AddressDto> PersonAddress { get; set; }
        [JsonIgnore]
        public AddressTypeEnum AddressType { get; init; }
        public string AddressTypeDescription => AddressType.GetDescription();


    }
}
