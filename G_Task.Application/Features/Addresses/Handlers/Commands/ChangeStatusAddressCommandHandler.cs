using G_Task.Application.Contracts.Persistence.Addresses;
using G_Task.Application.Features.Addresses.Requests.Commands;
using G_Task.Common.Exceptions;
using G_Task.Common.Responses;
using MediatR;
using Serilog;

namespace G_Task.Application.Features.Addresses.Handlers.Commands
{
    public class ChangeStatusAddressCommandHandler : IRequestHandler<ChangeStatusAddressCommand, BaseCommandResponse>
    {

        private readonly IAddressRepository _addressRepository;
        private readonly ILogger _logger;

        public ChangeStatusAddressCommandHandler(
                                          ILogger logger,
                                          IAddressRepository addressRepository)
        {

            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<BaseCommandResponse> Handle(ChangeStatusAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new Common.Responses.BaseCommandResponse();

            try
            {

                var Address = await _addressRepository.GetAsync(request.ID);

                if (Address == null) throw new NotFoundException(nameof(Address), request.ID);

                var success = await _addressRepository.ChangeStatus(request.ID, request.IsActive);

                if (!success) throw new Exception("Failed to change Address status.");

                response.Success = true;
                response.Message = ErrorMessages.ChangeAddressStatus;
                response.Status = 200;

                return response;
            }
            catch (Common.Exceptions.NotFoundException ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(ChangeStatusAddressCommand), ex.Message, ex);

                response.Success = false;
                response.Message = ex.Message;
                response.Status = 404;

                return response;
            }
            catch (Exception ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(ChangeStatusAddressCommand), ex.Message, ex);

                response.Success = false;
                response.Message = ErrorMessages.ChangeAddressStatusError;
                response.Status = 500;
                return response;
            }
        }
    }

}
