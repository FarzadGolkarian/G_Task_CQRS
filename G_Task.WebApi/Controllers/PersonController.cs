using G_Task.Application.DTOs.Persons;
using G_Task.Application.Features.Persons.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace G_Task.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PersonController : Controller
    {

        private readonly IMediator _mediator;
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<FoodController>
        [HttpGet]
        public async Task<ActionResult<List<PersonListDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetPersonListRequest()));
        }

        // GET api/<FoodController>/5
        [HttpGet("{personId}")]
        public async Task<ActionResult<PersonDto>> Get(long personId)
        {
            return Ok(await _mediator.Send(new GetPersonRequest { ID = personId }));
        }

    }
}
