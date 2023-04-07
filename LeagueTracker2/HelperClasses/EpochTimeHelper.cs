namespace LeagueTracker2.HelperClasses
{
    public static class EpochTimeHelper
    {
        public static long GetStartTimeSevenDaysAgo()
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime now = DateTime.UtcNow;
            DateTime startTime = now.AddDays(-7);

            return (long)(startTime - epochStart).TotalSeconds;
        }

        public static long GetCurrentEpochTime()
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime now = DateTime.UtcNow;

            return (long)(now - epochStart).TotalSeconds;
        }
    }
}
