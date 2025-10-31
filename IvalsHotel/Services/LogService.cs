using IvalsHotel.Accessors;
using IvalsHotel.Data.Models;
using System.Diagnostics;
using System.Reflection;

namespace IvalsHotel.Services;

public class LogService
{
    private readonly LogAccessor _accessor;

    public LogService(LogAccessor accessor)
    {
        this._accessor = accessor;
    }

    public async Task LogCreateAsync<T>(T input)
    {
        var stopwatch = Stopwatch.StartNew(); // стартирай таймера
        Log log = new() 
        { 
            Type = LogType.Добавяне, 
            Description = input!.ToString()! 
        };

        await this._accessor.CreateAsync(log); 
        
        
        stopwatch.Stop(); // спри таймера

        Console.WriteLine($"LogCreateAsync отне: {stopwatch.ElapsedMilliseconds} ms");
    }

    public async Task LogDeleteAsync<T>(T input)
    {
        var stopwatch = Stopwatch.StartNew(); // стартирай таймера


        Log log = new() 
        { 
            Type = LogType.Изтриване,
            Description = input!.ToString()!
        };

        await this._accessor.CreateAsync(log);

        stopwatch.Stop(); // спри таймера

        Console.WriteLine($"LogCreateAsync отне: {stopwatch.ElapsedMilliseconds} ms");
    }

    public async Task LogUpdateAsync<T>(T oldObject, T newObject)
    {

        var stopwatch = Stopwatch.StartNew(); // стартирай таймера

        var difference = GetDifference(oldObject, newObject);

        Log log = new() 
        { 
            Type = LogType.Редактиране, 
            Description = difference
        };

        await this._accessor.CreateAsync(log);

        stopwatch.Stop(); // спри таймера

        Console.WriteLine($"LogCreateAsync отне: {stopwatch.ElapsedMilliseconds} ms");
    }

    public async Task<List<Log>> GetAllAsync() => await _accessor.GetAllAsync();

    private static string GetDifference<T>(T obj1, T obj2)
    {
        string differences = "";

        if (obj1 == null || obj2 == null)
            throw new ArgumentNullException();

        var properties = typeof(T).GetProperties();

        foreach (var prop in properties)
        {
            var value1 = prop.GetValue(obj1);
            var value2 = prop.GetValue(obj2);

            if (!Equals(value1, value2))
            {
                var propName = GetPropertyName(prop.Name);

                differences += propName + " - ПРЕДИ: " + value1?.ToString() + " СЛЕД: " + value2?.ToString() + "<br />";
            }
        }

        return differences;
    }

    private static string GetPropertyName(string propertyName)
        => propertyName switch
        {
            "Made" => "Марка",
            "Model" => "Модел",
            "Width" => "Широчина",
            "Height" => "Височина",
            "RimSize" => "Цолаж",
            "Season" => "Сезон",
            "YearOfProduction" => "ДОТ",
            "Available" => "Наличност",
            "Description" => "Описание",
            _ => "",
        };
}


//public string? Made { get; set; }
//public string? Model { get; set; }
//[Required]
//public int Width { get; set; }
//[Required]
//public int Height { get; set; }
//[Required]
//public int RimSize { get; set; }
//[Required]
//public string Season { get; set; }
//public int? YearOfProduction { get; set; }
//[Required]
//public int Available { get; set; }
//public string? Description { get; set; }