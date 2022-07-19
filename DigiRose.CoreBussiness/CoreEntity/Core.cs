using System.Globalization;

namespace DigiRose.CoreBussiness.CoreEntity;

public class Core
{
    public Core()
    {
        CurrentTime = GetCurrentTime();
        CurrentDate = GetCurrentDate();
    }
    public int Id { get; set; }
    public string? CurrentDate { get; set; }
    public string? CurrentTime { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public string? ModificationTime { get; set; }
    public bool IsDeleted { get; set; }

    public string GetCurrentTime() => new PersianCalendar().GetHour(DateTime.Now) + ":" +
                                      new PersianCalendar().GetMinute(DateTime.Now) + ":" +
                                      new PersianCalendar().GetSecond(DateTime.Now);

    public string GetCurrentDate() => new PersianCalendar().GetYear(DateTime.Now) + "/" +
                                      new PersianCalendar().GetMonth(DateTime.Now) + "/" +
                                      new PersianCalendar().GetDayOfMonth(DateTime.Now);
}