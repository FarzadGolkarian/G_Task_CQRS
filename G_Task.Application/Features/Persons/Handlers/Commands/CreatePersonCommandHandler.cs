using AutoMapper;
using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Application.DTOs.Persons.Validators;
using G_Task.Application.Features.Persons.Requests.Commands;
using G_Task.Common.Exceptions;
using G_Task.Common.Helpers;
using G_Task.Common.Responses;
using G_Task.Domain.Common;
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

                var result = await validator.ValidateAsync(request.CreatePersonDto ,cancellationToken);

                if (!result.IsValid) throw new Common.Exceptions.ValidationException(result);

                if (await _personRepository.GetNationalCode(request.CreatePersonDto.NationalCode))
                {
                    _logger.Error("{methodName} {errorMessage}", nameof(CreatePersonCommandHandler), ErrorMessages.NationalCodeInvalid);

                    response.Success = false;
                    response.Message = string.Format(ErrorMessages.NationalCodeInvalid, request.CreatePersonDto.NationalCode);
                    response.Status = 400;
                    return response;

                }

                request.CreatePersonDto.NationalCode.VerifyRealNationalNumber();

                if (request.CreatePersonDto.PersonAddress == null
                    || !request.CreatePersonDto.PersonAddress.Any()
                    || request.CreatePersonDto.PersonAddress.Any(a => a.AddressType == 0 || string.IsNullOrWhiteSpace(a.PersonAddress)))

                {

                    response.Success = false;
                    response.Message = ErrorMessages.PersonAddress;
                    response.Status = 400;
                    return response;
                }

                foreach (var item in request.CreatePersonDto.PersonAddress)
                
                    EnumHelper.ValidationEnumDefined(typeof(AddressTypeEnum), item.AddressType, " نوع آدرس");
                

                var person = _mapper.Map<Domain.Person>(request.CreatePersonDto);

                var personId = (await _personRepository.Add(person)).ID;

                response.Success = true;
                response.Message = ErrorMessages.PersonCreated;
                response.Status = 200;
                response.ID = personId;

                return response;
            }
            catch (Common.Exceptions.ValidationException ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(CreatePersonCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = ex.Message;
                response.Status = 400;

                return response;
            }
            catch (Exception ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(CreatePersonCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = ex.Message;
                response.Status = 500;
                return response;
            }
        }
    }
}
