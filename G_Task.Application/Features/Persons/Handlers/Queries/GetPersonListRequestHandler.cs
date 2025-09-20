using AutoMapper;
using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Application.DTOs.Persons;
using G_Task.Application.Features.Persons.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace G_Task.Application.Features.Persons.Handlers.Queries
{

    public class GetPersonListRequestHandler : IRequestHandler<GetPersonListRequest, List<PersonListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly Serilog.ILogger _logger;

        public GetPersonListRequestHandler(IMapper mapper,
                                       IPersonRepository personRepository,
                                       Serilog.ILogger logger)
        {
            _mapper = mapper;
            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task<List<PersonListDto>> Handle(GetPersonListRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var personList =
                    await _personRepository.GetPersonList();

                if (personList == null || !personList.Any())
                {
                    _logger.Warning("{methodName} No persons found.", nameof(GetPersonListRequestHandler));

                    return new List<PersonListDto>();
                }

                return _mapper.Map<List<PersonListDto>>(personList);

            }
            catch (Exception ex)
            {
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(GetPersonListRequestHandler), ex.Message, ex);

                return null;
            }
        }
    }

}
