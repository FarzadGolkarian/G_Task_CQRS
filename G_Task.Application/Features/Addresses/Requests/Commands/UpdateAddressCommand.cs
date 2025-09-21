using G_Task.Application.DTOs.Addresses;
using G_Task.Common.Responses;
using MediatR;

namespace G_Task.Application.Features.Addresses.Requests.Commands
{
    public class UpdateAddressCommand : IRequest<BaseCommandResponse>
    {
        public long ID { get; set; }
        public UpdateAddressDto? UpdateAddressDto { get; set; }
    }
}
