using G_Task.Common.Helpers;
using G_Task.Domain.Common;

namespace G_Task.Application.DTOs.Addresses
{
    public class AddressDto
    {
        public string PersonAddress { get; init; }
        public AddressTypeEnum AddressType { get; init; }
        public string AddressTypeDescription => AddressType.GetDescription();
    }
}
