
namespace Enpal.AppointmentBooking.Domain.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
    }
}
