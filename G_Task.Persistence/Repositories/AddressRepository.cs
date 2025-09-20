using G_Task.Application.Contracts.Persistence.Addresses;
using G_Task.Domain;
using Microsoft.EntityFrameworkCore;

namespace G_Task.Persistence.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly G_TaskDbContext _dbContext;
        public AddressRepository(G_TaskDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<bool> ChangeStatus(long id, bool status)
        {
            var address = await _dbContext.Set<Address>().FindAsync(id);

            if (address != null)
            {
                address.IsActive = status;

                _dbContext.Entry(address).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
