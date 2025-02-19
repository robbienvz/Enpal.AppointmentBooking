using Enpal.AppointmentBooking.DB;
using Enpal.AppointmentBooking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Enpal.AppointmentBooking.Infrastructure
{
    public class SlotRepository : IRepository<Slot>
    {
        private readonly EnpalDbContext _enpalDbContext;
        private readonly ILogger<SlotRepository> _logger;
        public SlotRepository(EnpalDbContext enpalDbContext, ILogger<SlotRepository> logger)
        {
            _enpalDbContext = enpalDbContext;
            _logger = logger;
        }
        public async Task<List<Slot>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all slots from the database.");
                return await _enpalDbContext.Slots.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching slots.Error details {ex}.");
                throw new Exception("An error occurred while fetching slots.", ex);
            }
        }
    }
}
