using Enpal.AppointmentBooking.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Enpal.AppointmentBooking.API.Controllers
{
    [Route("calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {

        private readonly AppointmentSlotService _appointmentSlotService;
        private readonly ILogger<CalendarController> _logger;
        public CalendarController(AppointmentSlotService appointmentSlotService, ILogger<CalendarController> logger)
        {
            _appointmentSlotService = appointmentSlotService;
            _logger = logger;
        }

        [HttpPost("query")]
        public async Task<IActionResult> GetAvailableSlots([FromBody] SlotRequest request)
        {
            if (request == null || request.Products == null || request.Language == null || request.Rating == null
                || request.Date == null)
            {
                _logger.LogWarning("Invalid request parameters.");
                return BadRequest("Invalid request parameters.");
            }
            try
            {
                _logger.LogInformation($"Received request for available slots.Request details:{request}");
                var availableSlots = await _appointmentSlotService.GetAvailableSlotsAsync(request);

                return Ok(availableSlots);

                //if (availableSlots == null || !availableSlots.Any())
                //{
                //    return NotFound("No available slots found.");
                //}

                //return Ok(availableSlots);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while processing the request. Error details {ex}.");
                return StatusCode(500, $"An unexpected error occurred on the server. Please try again later. Error detals: {ex.Message}");
            }
        }
    }
}
