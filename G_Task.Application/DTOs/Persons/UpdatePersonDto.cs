using G_Task.Application.DTOs.Addresses;
using G_Task.Application.DTOs.Persons.Interfaces;

namespace G_Task.Application.DTOs.Persons
{
    public class UpdatePersonDto: IPerson
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalCode { get; set; }
        //public List<AddressDto>? PersonAddress { get; set; }
    }
}
