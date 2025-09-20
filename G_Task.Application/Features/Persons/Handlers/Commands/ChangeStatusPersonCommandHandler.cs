using AutoMapper;
using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Application.Features.Persons.Requests.Commands;
using G_Task.Common.Responses;
using G_Task.Domain;
using MediatR;
using Serilog;

namespace G_Task.Application.Features.Persons.Handlers.Commands
{
    public class ChangeStatusPersonCommandHandler : IRequestHandler<ChangeStatusPersonCommand, BaseCommandResponse>
    {

        private readonly IPersonRepository _personRepository;
        private readonly ILogger _logger;

        public ChangeStatusPersonCommandHandler(
                                          ILogger logger,
                                          IPersonRepository personRepository)
        {

            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task<BaseCommandResponse> Handle(ChangeStatusPersonCommand request, CancellationToken cancellationToken)
        {
            var response = new Common.Responses.BaseCommandResponse();

            try
            {

                var person = await _personRepository.GetAsync(request.ID);

                if (person == null) throw new Common.Exceptions.NotFoundException(nameof(Person), request.ID);

                var success = await _personRepository.ChangeStatus(request.ID , request.IsActive);

                if (!success) throw new Exception("Failed to change person status.");                

                response.Success = true;
                response.Message = "Change Person Status successfully.";

                return response;
            }
            catch (Common.Exceptions.NotFoundException ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(ChangeStatusPersonCommand), ex.Message, ex);

                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
            catch (Exception ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(ChangeStatusPersonCommand), ex.Message, ex);

                response.Success = false;
                response.Message = "An error occurred while Change Person Status.";

                return response;
            }
        }
    }

}
