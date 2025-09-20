using AutoMapper;
using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Application.DTOs.Persons.Validators;
using G_Task.Application.Features.Persons.Requests.Commands;
using G_Task.Common.Responses;
using MediatR;
using Serilog;

namespace G_Task.Application.Features.Persons.Handlers.Commands
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, BaseCommandResponse>
    {

        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly ILogger _logger;

        public UpdatePersonCommandHandler(IMapper mapper,
                                          ILogger logger,
                                          IPersonRepository personRepository)
        {
            _mapper = mapper;
            _personRepository = personRepository;
            _logger = logger;
        }
        public async Task<BaseCommandResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                var Validatior = new UpdatePersonValidator();

                if (request.UpdatePersonDto != null)
                {

                    var R = await Validatior.ValidateAsync(request.UpdatePersonDto);



                    if (R.IsValid == false)
                    {
                        throw new Common.Exceptions.ValidationException(R);
                    }
                }

                var result = await _personRepository.GetAsync(request.ID);

                if (result == null) { throw new Common.Exceptions.NotFoundException(nameof(result), request.UpdatePersonDto); }

                _mapper.Map(request.UpdatePersonDto, result);

                await _personRepository.Update(result);

                response.Success = true;
                response.Message = "Person updated successfully.";

                return response;


            }
            catch (Common.Exceptions.NotFoundException ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(UpdatePersonCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
            catch (Exception ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(UpdatePersonCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = "An error occurred while updating the person.";

                return response;
            }

        }
    }
}
