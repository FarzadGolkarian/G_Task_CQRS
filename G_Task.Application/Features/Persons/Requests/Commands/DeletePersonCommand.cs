using G_Task.Common.Responses;
using MediatR;

namespace G_Task.Application.Features.Persons.Requests.Commands
{
    public class DeletePersonCommand : IRequest<BaseCommandResponse>
    {
        private long _id;

        public long ID
        {
            get => _id;
            init
            {
                if (value <= 0)
                    throw new ArgumentException("ID must be greater than 0.");
                _id = value;
            }
        }
    }
}
