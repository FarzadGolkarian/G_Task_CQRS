using AutoMapper;
using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Application.Features.Persons.Requests.Commands;
using G_Task.Common.Responses;
using G_Task.Domain;
using MediatR;
using Serilog;

namespace G_Task.Application.Features.Persons.Handlers.Commands
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly ILogger _logger;

        public DeletePersonCommandHandler(IMapper mapper, ILogger logger, IPersonRepository personRepository)
        {
            _mapper = mapper;
            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task<BaseCommandResponse> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var response = new Common.Responses.BaseCommandResponse();

            try
            {
              
                var person = await _personRepository.GetAsync(request.ID);
                
                if (person == null) throw new Common.Exceptions.NotFoundException(nameof(Person), request.ID);
              
                await _personRepository.Delete(person);
        
                response.Success = true;
                response.Message = "Person deleted successfully.";

                return response;
            }
            catch (Common.Exceptions.NotFoundException ex)
            {
             
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(DeletePersonCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = ex.Message; 

                return response;
            }
            catch (Exception ex)
            {
                
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(DeletePersonCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = "An error occurred while deleting the person.";

                return response;
            }
        }

    }
}
