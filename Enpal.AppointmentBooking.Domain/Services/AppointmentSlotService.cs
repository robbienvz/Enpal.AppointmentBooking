using Enpal.AppointmentBooking.Common;
using Enpal.AppointmentBooking.Domain.Models;
using Enpal.AppointmentBooking.Domain.Repositories;

namespace Enpal.AppointmentBooking.Domain.Services
{
    public class AppointmentSlotService
    {
        public IRepository<SalesManager> _salesManagerRepository;
        public IRepository<Slot> _slotRepository;
        public AppointmentSlotService(IRepository<SalesManager> salesManagerRepository, IRepository<Slot> slotRepository)
        {
            _salesManagerRepository = salesManagerRepository;
            _slotRepository = slotRepository;
        }

        public async Task<List<AvailableSlotResponse>> GetAvailableSlotsAsync(SlotRequest request)
        {
            if (DateTime.TryParse(request.Date, out var requestedDate))
            {
                var availableSlots = new List<AvailableSlotResponse>();

                var allSalesManagers = await _salesManagerRepository.GetAllAsync();
                // Find sales managers that match the requested language, rating, and product criteria
                var matchingSalesManagers = await GetMatchingSalesManagersAsync(request);

                // Fetch available slots for each matching manager
                foreach (var manager in matchingSalesManagers)
                {
                    await GetAvailableSlotsForManagerAsync(manager, requestedDate, availableSlots);
                }

                return availableSlots;
            }

            throw new ArgumentException($"Invalid Date format.{request.Date}");
        }

        private async Task<IEnumerable<SalesManager>> GetMatchingSalesManagersAsync(SlotRequest request)
        {
            var allSalesManagers = await _salesManagerRepository.GetAllAsync();

            return allSalesManagers.Where(sm => sm.Languages.Contains(request.Language) &&
                sm.CustomerRatings.Contains(request.Rating) && request.Products.All(sm.Products.Contains));
        }

        private async Task GetAvailableSlotsForManagerAsync(SalesManager manager, DateTime requestedDate, List<AvailableSlotResponse> availableSlots)
        {
            // Fetch all slots for this manager on the requested date
            var managerSlots = (await _slotRepository.GetAllAsync()).Where(s => s.SalesManagerId == manager.Id &&
                               s.StartDate.Date == requestedDate.Date).ToList();
            // Loop through each slot and check if it can be booked
            foreach (var slot in managerSlots)
            {
                if (CheckSlotAvailability(managerSlots, slot))
                {
                    AddAvailableSlot(availableSlots, slot);
                }
            }
        }

        private bool CheckSlotAvailability(List<Slot> managerSlots, Slot slot)
        {
            if (slot.Booked)
            {
                return false;
            }
            // Check for overlapping booked slots for this manager
            return !managerSlots.Any(s => s != slot && s.Booked &&
                s.StartDate < slot.EndDate && s.EndDate > slot.StartDate);
        }

        private void AddAvailableSlot(List<AvailableSlotResponse> availableSlots, Slot slot)
        {
            var existingSlotEntry = availableSlots.FirstOrDefault(i => i.StartDate == slot.StartDate.ToUTC());
            if (existingSlotEntry != null)
            {
                // Increment the available count for this time
                existingSlotEntry.AvailableCount++;
            }
            else
            {
                availableSlots.Add(new AvailableSlotResponse
                {
                    AvailableCount = 1,
                    StartDate = slot.StartDate.ToUTC()
                });
            }
        }
    }
}
