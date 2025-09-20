using G_Task.Domain.Common;

namespace G_Task.Application.DTOs.Addresses
{
    public class AddressListDto
    {
        public string PersonAddress { get; init; }
        public AddressTypeEnum AddressType { get; init; }
        public long PersonId { get; init; }
    }
}
