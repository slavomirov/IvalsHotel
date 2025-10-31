using IvalsHotel.Data.Models;
using IvalsHotel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace IvalsHotel.Pages;

[Authorize]
public class AddTireModel : PageModel
{
    private readonly TireService _tireService;

    public AddTireModel(TireService tireService)
    {
        _tireService = tireService;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        public string? Made { get; set; }

        public string? Model { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Width { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Height { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int RimSize { get; set; }

        public string Season { get; set; }
        
        public int? YearOfProduction { get; set; }

        [Range(0, int.MaxValue)]
        public int Available { get; set; }

        public string? Description { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var tire = new Tire
        {
            Made = Input.Made,
            Model = Input.Model,
            Width = Input.Width,
            Height = Input.Height,
            RimSize = Input.RimSize,
            Season = Input.Season,
            YearOfProduction = Input.YearOfProduction ?? 0,
            Available = Input.Available,
            Description = Input.Description
        };

        await _tireService.AddTireAsync(tire);

        return RedirectToPage("/Index");
    }
}
