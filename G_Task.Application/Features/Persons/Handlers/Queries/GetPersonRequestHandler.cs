using AutoMapper;
using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Application.DTOs.Persons;
using G_Task.Application.Features.Persons.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace G_Task.Application.Features.Persons.Handlers.Queries
{
    public class GetPersonRequestHandler(IMapper mapper,
                                   IPersonRepository personRepository,
                                   Serilog.ILogger logger) : IRequestHandler<GetPersonRequest, PersonDto>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly Serilog.ILogger _logger = logger;

        public async Task<PersonDto> Handle(GetPersonRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var person =
                    await _personRepository.GetPerson(request.ID);

                if (person == null)
                    throw new KeyNotFoundException($"Person with ID {request.ID} not found.");

                return _mapper.Map<PersonDto>(person);

            }
            catch (KeyNotFoundException ex)
            {
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(GetPersonRequestHandler), ex.Message, ex);

                return null;
            }
            catch (Exception ex)
            {
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(GetPersonRequestHandler), ex.Message, ex);

                return null;
            }
        }


    }
}
