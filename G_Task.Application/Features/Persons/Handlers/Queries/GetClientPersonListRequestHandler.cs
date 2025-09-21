using AutoMapper;
using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Application.DTOs.Persons;
using G_Task.Application.Features.Persons.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace G_Task.Application.Features.Persons.Handlers.Queries
{

    public class GetClientPersonListRequestHandler : IRequestHandler<GetClientPersonListRequest, List<PersonListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly Serilog.ILogger _logger;

        public GetClientPersonListRequestHandler(IMapper mapper,
                                       IPersonRepository personRepository,
                                       Serilog.ILogger logger)
        {
            _mapper = mapper;
            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task<List<PersonListDto>> Handle(GetClientPersonListRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var personList =
                    await _personRepository.GetClientPersonList();

                if (personList == null || !personList.Any())
                {
                    _logger.Warning("{methodName} No persons found.", nameof(GetClientPersonListRequestHandler));

                    return null;
                }

                return _mapper.Map<List<PersonListDto>>(personList);

            }

            catch (Exception ex)
            {
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(GetClientPersonListRequestHandler), ex.Message, ex);

                throw;
            }
        }
    }

}
