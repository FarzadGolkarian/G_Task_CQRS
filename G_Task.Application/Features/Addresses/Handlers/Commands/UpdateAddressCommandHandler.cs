using AutoMapper;
using FluentValidation;
using G_Task.Application.Contracts.Persistence.Addresses;
using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Application.DTOs.Addresses.Validators;
using G_Task.Application.DTOs.Persons.Validators;
using G_Task.Application.Features.Addresses.Requests.Commands;
using G_Task.Common.Exceptions;
using G_Task.Common.Helpers;
using G_Task.Common.Responses;
using G_Task.Domain.Common;
using MediatR;
using Serilog;

namespace G_Task.Application.Features.Addresses.Handlers.Commands
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, BaseCommandResponse>
    {

        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger _logger;

        public UpdateAddressCommandHandler(IMapper mapper,
                                          ILogger logger,
                                           IAddressRepository addressRepository)
        {
            _mapper = mapper;
            _addressRepository = addressRepository;
            _logger = logger;
        }
        public async Task<BaseCommandResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                var address = await _addressRepository.GetAsync(request.ID);

                if (address == null) throw new Common.Exceptions.NotFoundException(nameof(address), $"with ID {request.ID}");

                var Validatior = new UpdateAddressValidator();

                var result = await Validatior.ValidateAsync(request.UpdateAddressDto, cancellationToken);

                if (!result.IsValid) throw new Common.Exceptions.ValidationException(result);

                if (request.UpdateAddressDto.AddressType == 0 || string.IsNullOrWhiteSpace(request.UpdateAddressDto.PersonAddress))
                {
                    response.Success = false;
                    response.Message = ErrorMessages.PersonAddress;
                    return response;
                }

                EnumHelper.ValidationEnumDefined(typeof(AddressTypeEnum), request.UpdateAddressDto.AddressType, " نوع آدرس");


                _mapper.Map(request.UpdateAddressDto, address);

                await _addressRepository.Update(address);

                response.Success = true;
                response.Message = ErrorMessages.PersonUpdated;

                return response;


            }
            catch (Common.Exceptions.NotFoundException ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(UpdateAddressCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = ex.Message;
                response.Status = 404;

                return response;
            }
            catch (Common.Exceptions.ValidationException ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(UpdateAddressCommandHandler), ex.Errors.ToList(), ex);

                response.Success = false;
                response.Message = string.Join("; ", ex.Errors);
                response.Status = 400;

                return response;
            }
            catch (Exception ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(UpdateAddressCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = ex.Message.Trim(); //ErrorMessages.PersonUpdatingError;
                response.Status = 500;

                return response;
            }

        }
    }
}
