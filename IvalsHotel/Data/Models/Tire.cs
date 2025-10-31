using System.ComponentModel.DataAnnotations;

namespace IvalsHotel.Data.Models;

public class Tire
{
    public int Id { get; set; }
    public string? Made { get; set; }
    public string? Model { get; set; }
    [Required]
    public int Width { get; set; }
    [Required]
    public int Height { get; set; }
    [Required]
    public int RimSize { get; set; }
    [Required]
    public string Season { get; set; }
    public int? YearOfProduction { get; set; }
    [Required]
    public int Available { get; set; }
    public string? Description { get; set; }


    public override string ToString()
    {
        return $"Марка: {Made}, Модел: {Model}, Широчина: {Width}, Височина: {Height}, Цолаж: {RimSize}, Сезон: {Season}, ДОТ: {YearOfProduction}, Наличност: {Available}, Описание: {Description}";
    }
}