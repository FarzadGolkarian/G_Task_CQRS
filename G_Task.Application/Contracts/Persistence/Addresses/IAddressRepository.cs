using G_Task.Domain;

namespace G_Task.Application.Contracts.Persistence.Addresses
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<bool> ChangeStatus(long id, bool status);
    }

}
