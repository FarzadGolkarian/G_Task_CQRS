using G_Task.Application.DTOs.Common;
using G_Task.Common.Helpers;
using G_Task.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace G_Task.AdminPanelWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize(Roles = "Admin")]
public class EnumController : ControllerBase
{
   
    [HttpGet("AddressTypes")]
    public async Task<ActionResult<IList<EnumDto>>> GetAddressTypeEnum()
    {
        var enumDtos =
            Enum.GetValues(typeof(AddressTypeEnum))
                .Cast<AddressTypeEnum>()
                .Select(v => new EnumDto { Name = v.GetDescription(), Value = (int)v })
                .Where(s => s.Value > 0)
                .ToList();

        return Ok(enumDtos);
    }

}