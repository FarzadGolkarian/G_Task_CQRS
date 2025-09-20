using AutoMapper;
using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Application.DTOs.Persons.Validators;
using G_Task.Application.Features.Persons.Requests.Commands;
using G_Task.Common.Responses;
using MediatR;
using Serilog;

namespace G_Task.Application.Features.Persons.Handlers.Commands
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly ILogger _logger;

        public CreatePersonCommandHandler(IMapper mapper,
                                          ILogger logger,
                                          IPersonRepository personRepository)
        {
            _mapper = mapper;
            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task<BaseCommandResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                var validator = new CreatePersonValidator();

                var result = await validator.ValidateAsync(request.CreatePersonDto);

                if (!result.IsValid) throw new Common.Exceptions.ValidationException(result);
        
                var person = _mapper.Map<Domain.Person>(request.CreatePersonDto);

                var personId = (await _personRepository.Add(person)).ID;
               
                response.Success = true;
                response.Message = "Person created successfully.";
                response.ID = personId;

                return response;
            }
            catch (Common.Exceptions.ValidationException ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(CreatePersonCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
            catch (Exception ex)
            {
           
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(CreatePersonCommandHandler), ex.Message, ex);
               
                response.Success = false;
                response.Message = "An error occurred while creating the person.";

                return response;
            }
        }
    }
}
