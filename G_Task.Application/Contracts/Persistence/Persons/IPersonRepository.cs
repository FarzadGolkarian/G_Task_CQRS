using G_Task.Application.DTOs.Persons;
using G_Task.Domain;

namespace G_Task.Application.Contracts.Persistence.Persons
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<List<PersonListDto>> GetPersonList();
        Task<PersonDto?> GetPerson(long personId);
        Task<bool> ChangeStatus(long id, bool status);
    }
}
