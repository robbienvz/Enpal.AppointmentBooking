using System.Text.Json.Serialization;

namespace Enpal.AppointmentBooking.Domain.Models
{
    public class AvailableSlotResponse
    {
        [JsonPropertyName("available_count")]
        public int AvailableCount { get; set; }
        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }
    }
}
