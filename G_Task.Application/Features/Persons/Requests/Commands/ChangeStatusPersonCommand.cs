using G_Task.Common.Responses;
using MediatR;

namespace G_Task.Application.Features.Persons.Requests.Commands
{
    public class ChangeStatusPersonCommand : IRequest<BaseCommandResponse>
    {
        public long ID { get; set; }
        public bool IsActive { get; set; }
    }
}
