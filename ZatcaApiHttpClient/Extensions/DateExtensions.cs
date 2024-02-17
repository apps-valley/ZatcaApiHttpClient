namespace ZatcaApiHttpClient.Extensions;

public static class DateExtensions
{
    public static DateTime ToKsaDateTime(this DateTime date)
    {
        TimeZoneInfo ksaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time");
        if (date.Kind == DateTimeKind.Local)
        {
            return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.Local, ksaTimeZone);
        }
        else
        {
            return TimeZoneInfo.ConvertTimeFromUtc(date, ksaTimeZone);
        }
    }
}
