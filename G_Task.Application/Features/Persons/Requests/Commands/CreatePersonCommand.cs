using G_Task.Application.DTOs.Persons;
using G_Task.Common.Responses;
using MediatR;


namespace G_Task.Application.Features.Persons.Requests.Commands
{
    public class CreatePersonCommand : IRequest<BaseCommandResponse>
    {
        public CreatePersonDto CreatePersonDto { get; set; }
    }
}
