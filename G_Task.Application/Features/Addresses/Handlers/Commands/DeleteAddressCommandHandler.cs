using AutoMapper;
using G_Task.Application.Contracts.Persistence.Addresses;
using G_Task.Application.Features.Addresses.Requests.Commands;
using G_Task.Common.Exceptions;
using G_Task.Common.Responses;
using MediatR;
using Serilog;

namespace G_Task.Application.Features.Addresses.Handlers.Commands
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger _logger;

        public DeleteAddressCommandHandler(IMapper mapper, ILogger logger, IAddressRepository addressRepository)
        {
            _mapper = mapper;
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<BaseCommandResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new Common.Responses.BaseCommandResponse();

            try
            {

                var address = await _addressRepository.GetAsync(request.ID);

                if (address == null) throw new Common.Exceptions.NotFoundException(nameof(address), $"with ID {request.ID}");

                await _addressRepository.Delete(address);

                response.Success = true;
                response.Message = ErrorMessages.AddressDeleted;
                response.ID = request.ID;
                response.Status = 200;

                return response;
            }
            catch (Common.Exceptions.NotFoundException ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(DeleteAddressCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = ex.Message;
                response.ID = request.ID;
                response.Status = 404;
                return response;
            }
            catch (Exception ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(DeleteAddressCommandHandler), ex.Message, ex);

                response.Success = false;
                response.Message = ErrorMessages.PersonDeletedError;
                response.ID = request.ID;
                response.Status = 500;
                return response;
            }
        }

    }
}
