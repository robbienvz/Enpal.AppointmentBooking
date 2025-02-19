namespace Enpal.AppointmentBooking.Common
{
    public static class DateTimeExtentions
    {
        public static string ToUTC(this DateTime dateTime)
        {
            DateTime utcDateTime = dateTime.ToUniversalTime();
            return utcDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }
    }
}
