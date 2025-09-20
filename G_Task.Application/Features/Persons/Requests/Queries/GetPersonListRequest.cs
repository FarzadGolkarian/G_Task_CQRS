using G_Task.Application.DTOs.Persons;
using MediatR;

namespace G_Task.Application.Features.Persons.Requests.Queries;

public class GetPersonListRequest : IRequest<List<PersonListDto>>;
