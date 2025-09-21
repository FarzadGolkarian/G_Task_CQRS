using AutoMapper;
using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Application.DTOs.Persons;
using G_Task.Application.Features.Persons.Requests.Queries;
using G_Task.Common.Exceptions;
using G_Task.Domain;
using MediatR;

namespace G_Task.Application.Features.Persons.Handlers.Queries
{
    public class GetClientPersonRequestHandler : IRequestHandler<GetClientPersonRequest, PersonDto>
    {
        private readonly IMapper _mapper ;
        private readonly IPersonRepository _personRepository ;
        private readonly Serilog.ILogger _logger;

        public GetClientPersonRequestHandler(IMapper mapper,
                               IPersonRepository personRepository,
                               Serilog.ILogger logger)
        {
            _mapper = mapper;
            _personRepository = personRepository;
            _logger = logger;
        }
        public async Task<PersonDto> Handle(GetClientPersonRequest request, CancellationToken cancellationToken)
        {
            var response = new Common.Responses.BaseCommandResponse();
            try
            {
                var person =
                    await _personRepository.GetClientPerson(request.ID);

                if (person == null) throw new NotFoundException(nameof(Person), request.ID);

                return _mapper.Map<PersonDto>(person);

            }
            catch (Common.Exceptions.NotFoundException ex)
            {
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(GetClientPersonRequestHandler), ex.Message, request.ID);

                return null;
            }

            catch (Exception ex)
            {
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(GetClientPersonRequestHandler), ex.Message, ex);

                throw;
            }
        }

    }
}
