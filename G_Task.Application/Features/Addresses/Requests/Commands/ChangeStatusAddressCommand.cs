using G_Task.Common.Responses;
using MediatR;

namespace G_Task.Application.Features.Addresses.Requests.Commands
{
    public class ChangeStatusAddressCommand : IRequest<BaseCommandResponse>
    {
        public long ID { get; set; }
        public bool IsActive { get; set; }
    }
}
