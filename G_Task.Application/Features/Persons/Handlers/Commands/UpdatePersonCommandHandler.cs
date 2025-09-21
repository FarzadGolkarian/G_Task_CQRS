using AutoMapper;
using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Application.DTOs.Persons.Validators;
using G_Task.Application.Features.Persons.Requests.Commands;
using G_Task.Common.Exceptions;
using G_Task.Common.Helpers;
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
                var person = await _personRepository.GetAsync(request.ID);

                if (person == null)
                    throw new Common.Exceptions.NotFoundException(nameof(person), $"with ID {request.ID}");

                var Validatior = new UpdatePersonValidator();

                var result = await Validatior.ValidateAsync(request.UpdatePersonDto, cancellationToken);

                if (!result.IsValid) throw new Common.Exceptions.ValidationException(result);

                if (!string.IsNullOrEmpty(request.UpdatePersonDto.NationalCode))
                {
                    var nCode = await _personRepository.GetNationalCode(request.UpdatePersonDto.NationalCode);

                    if (nCode)
                    {
                        _logger.Error("{methodName} {errorMessage}", nameof(UpdatePersonCommandHandler), ErrorMessages.NationalCodeInvalid);

                        response.Success = false;
                        response.Message = string.Format(ErrorMessages.NationalCodeInvalid, request.UpdatePersonDto.NationalCode);
                        return response;

                    }
                }

                request.UpdatePersonDto.NationalCode.VerifyRealNationalNumber();

                _mapper.Map(request.UpdatePersonDto, person);

                await _personRepository.Update(person);

                response.Success = true;

                response.Message = ErrorMessages.PersonUpdated;

                return response;


            }
            catch (Common.Exceptions.NotFoundException ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(UpdatePersonCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = ex.Message;
                response.Status = 404;

                return response;
            }
            catch (Common.Exceptions.ValidationException ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(UpdatePersonCommandHandler), ex.Errors.ToList(), ex);

                response.Success = false;
                response.Message = string.Join("; ", ex.Errors);
                response.Status = 400;

                return response;
            }
            catch (Exception ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(UpdatePersonCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = ex.Message;
                response.Status = 500;

                return response;
            }

        }
    }
}
