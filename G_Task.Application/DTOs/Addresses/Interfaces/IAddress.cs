using G_Task.Domain.Common;

namespace G_Task.Application.DTOs.Addresses.Interfaces
{
    public interface IAddress
    {
        public string PersonAddress { get; init; }
        public AddressTypeEnum AddressType { get; init; }
       
    }
}
