using Enpal.AppointmentBooking.DB;
using Enpal.AppointmentBooking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Enpal.AppointmentBooking.Infrastructure
{
    public class SalesManagerRepository : IRepository<SalesManager>
    {
        private readonly EnpalDbContext _enpalDbContext;
        private readonly ILogger<SalesManagerRepository> _logger;

        public SalesManagerRepository(EnpalDbContext enpalDbContext, ILogger<SalesManagerRepository> logger)
        {
            _enpalDbContext = enpalDbContext;
            _logger = logger;
        }
        public async Task<List<SalesManager>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all sales managers from the database.");
                return await _enpalDbContext.SalesManagers.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching sales managers.Error details {ex}.");
                throw new Exception("An error occurred while fetching sales managers.", ex);
            }
        }
    }
}
