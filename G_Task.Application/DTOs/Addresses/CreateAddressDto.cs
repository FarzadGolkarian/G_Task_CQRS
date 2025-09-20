using G_Task.Application.DTOs.Addresses.Interfaces;
using G_Task.Domain.Common;

namespace G_Task.Application.DTOs.Addresses
{
    public class CreateAddressDto : IAddress
    {
        public string PersonAddress { get; init; }
        public AddressTypeEnum AddressType { get; init; }
        public long PersonId { get; init; }
    }
}
