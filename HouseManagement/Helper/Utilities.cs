namespace Helper;

public static class Utilities
{
    public static string ConvertMilisecondToHourMinSec(this long mSec)
    {
        var time = TimeSpan.FromMilliseconds(mSec);
        var lst = new List<string>();
        if (time.Hours > 0)
        {
            lst.Add($"{time.Hours:D2}h");
        }

        if (time.Minutes > 0)
        {
            lst.Add($"{time.Minutes:D2}m");
        }

        if (time.Seconds > 0)
        {
            lst.Add($"{time.Seconds:D2}s");
        }

        lst.Add($"{time.Milliseconds:D2}ms");
        var result = string.Join(":", lst);
        return result;
    }

    public static bool IsNotEmpty(this string? input)
    {
        return !string.IsNullOrEmpty(input);
    }
}