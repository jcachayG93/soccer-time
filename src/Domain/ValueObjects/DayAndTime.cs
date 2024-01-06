namespace Domain.ValueObjects;

public record DayAndTime
{
    public DayOfWeek Day { get; }
    
    public int Begins { get; }

    public int Ends { get; }

    public DayAndTime(
        DayOfWeek day, MinutesSinceMidnight begins, MinutesSinceMidnight ends)
    {
        
    }
}