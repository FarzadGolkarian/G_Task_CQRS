using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Application.DTOs.Persons;
using G_Task.Domain;
using Microsoft.EntityFrameworkCore;

namespace G_Task.Persistence.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly G_TaskDbContext _dbContext;
        private readonly Serilog.ILogger _logger;
        public PersonRepository(G_TaskDbContext context, Serilog.ILogger logger) : base(context)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<bool> ChangeStatus(long id, bool status)
        {
            var person = await _dbContext.Set<Person>().FindAsync(id);

            if (person != null)
            {
                person.IsActive = status;

                _dbContext.Entry(person).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;

        }

        public async Task<PersonDto?> GetClientPerson(long personId)
        {

            try
            {
                return await _dbContext.Set<Person>()
                    .Where(p => p.ID == personId && p.IsActive)
                    .Select(p => new PersonDto
                    {
                        ID = p.ID,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        NationalCode = p.NationalCode,
                        PersonAddress = p.Addresses
                        .Where(w => w.IsActive)
                        .Select(s => new Application.DTOs.Addresses.AddressDto
                        {
                            AddressType = s.AddressType,
                            PersonAddress = s.PersonAddress
                        }).ToList()
                    }).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(GetPersonList), ex.Message, ex);

                return null;
            }
        }

        public async Task<List<PersonListDto>> GetClientPersonList()
        {

            try
            {
                return await _dbContext.Set<Person>()
                    .Where(p => p.IsActive)
                     .Select(p => new PersonListDto
                     {
                         ID = p.ID,
                         FirstName = p.FirstName,
                         LastName = p.LastName,
                         NationalCode = p.NationalCode,
                         PersonAddress = p.Addresses
                         .Where(w => w.IsActive)
                         .Select(s => new Application.DTOs.Addresses.AddressDto
                         {
                             AddressType = s.AddressType,
                             PersonAddress = s.PersonAddress
                         }).ToList()
                     }).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(GetPersonList), ex.Message, ex);

                return null;
            }
        }

        public async Task<bool> GetNationalCode(string nationalCode)
        {
            try
            {
                if (await _dbContext.Set<Person>()
                    .AnyAsync(b => b.NationalCode == nationalCode))
                    return true;
                else return false;
            }
            catch (Exception ex)
            {

                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(GetNationalCode), ex.Message, ex);

                return false;
            }
        }

        public async Task<PersonDto?> GetPerson(long personId)
        {

            try
            {
                return await _dbContext.Set<Person>()
                    .Where(p => p.ID == personId)
                    .Select(p => new PersonDto
                    {
                        ID = p.ID,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        NationalCode = p.NationalCode,
                        PersonAddress = p.Addresses.Select(s => new Application.DTOs.Addresses.AddressDto
                        {
                            AddressType = s.AddressType,
                            PersonAddress = s.PersonAddress
                        }).ToList()
                    }).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(GetPersonList), ex.Message, ex);

                return null;
            }
        }

        public async Task<List<PersonListDto>> GetPersonList()
        {
            try
            {
                return await _dbContext.Set<Person>()
                     .Select(p => new PersonListDto
                     {
                         ID = p.ID,
                         FirstName = p.FirstName,
                         LastName = p.LastName,
                         NationalCode = p.NationalCode,
                         PersonAddress = p.Addresses.Select(s => new Application.DTOs.Addresses.AddressDto
                         {
                             AddressType = s.AddressType,
                             PersonAddress = s.PersonAddress
                         }).ToList()
                     }).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.Error("{methodName} {errorMessage} {@ex}", nameof(GetPersonList), ex.Message, ex);

                return null;
            }

        }

    }
}

