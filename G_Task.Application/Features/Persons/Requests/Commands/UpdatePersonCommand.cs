using G_Task.Application.DTOs.Persons;
using G_Task.Common.Responses;
using MediatR;

namespace G_Task.Application.Features.Persons.Requests.Commands
{
    public class UpdatePersonCommand : IRequest<BaseCommandResponse>
    {
        public long ID { get; set; }
        public UpdatePersonDto? UpdatePersonDto { get; set; }
    }
}
