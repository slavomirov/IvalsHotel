namespace IvalsHotel.Data.Models;

public class Log
{
    public int Id { get; set; }
    public LogType Type { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}

public enum LogType
{
    Добавяне = 0, 
    Редактиране = 1,
    Изтриване = 2
}
