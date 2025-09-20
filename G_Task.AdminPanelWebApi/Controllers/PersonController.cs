using G_Task.Application.DTOs.Persons;
using G_Task.Application.Features.Persons.Requests.Commands;
using G_Task.Application.Features.Persons.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace G_Task.AdminPanelWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(Roles = "Admin")]
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

        // POST api/<FoodController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreatePersonDto CreatePerson)
        {
            return Ok(await _mediator.Send(new CreatePersonCommand { CreatePersonDto = CreatePerson }));
        }

        // PUT api/<FoodController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long personId, [FromBody] UpdatePersonDto UpdatePerson)
        {
            return Ok(await _mediator.Send(new UpdatePersonCommand { UpdatePersonDto = UpdatePerson, ID = personId }));
        }

        // DELETE api/<FoodController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long personId)
        {
            return Ok(await _mediator.Send(new DeletePersonCommand { ID = personId }));
        }

        [HttpPut("{personId:long}/ChangeStatus")]
        public async Task<ActionResult> ChangePersonStatus(long personId, bool status)
        {
            await _mediator.Send (new ChangeStatusPersonCommand { ID = personId ,  IsActive = status});

            return Ok();
        }
    }
}
