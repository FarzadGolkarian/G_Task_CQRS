using G_Task.Application.DTOs.Addresses;
using G_Task.Application.Features.Addresses.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace G_Task.AdminPanelWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Admin")]
    public class AddressController : Controller
    {
        private readonly IMediator _mediator;
        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{personId}")]
        public async Task<ActionResult> Put(long personId, [FromBody] UpdateAddressDto updateAddress)
        {
            return Ok(await _mediator.Send(new UpdateAddressCommand { UpdateAddressDto = updateAddress, ID = personId }));
        }


        [HttpDelete("{addressId}")]
        public async Task<ActionResult> Delete(long addressId)
        {
            return Ok(await _mediator.Send(new DeleteAddressCommand { ID = addressId }));
        }


        [HttpPut("{addressId:long}/ChangeStatus")]
        public async Task<ActionResult> ChangePersonStatus(long addressId, bool status)
        {
            return Ok(await _mediator.Send(new ChangeStatusAddressCommand { ID = addressId, IsActive = status }));


        }
    }
}
