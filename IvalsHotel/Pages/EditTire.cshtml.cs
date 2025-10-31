using IvalsHotel.Data.Models;
using IvalsHotel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IvalsHotel.Pages;

[Authorize]
public class EditTireModel : PageModel
{
    private readonly TireService _tireService;

    public EditTireModel(TireService tireService)
    {
        _tireService = tireService;
    }

    [BindProperty]
    public Tire Input { get; set; }

    public List<SelectListItem> SeasonOptions { get; set; } =
    [
        new SelectListItem { Text = "Зимни", Value = "Зимни" },
        new SelectListItem { Text = "Летни", Value = "Летни" },
        new SelectListItem { Text = "Всесезонни", Value = "Всесезонни" },
    ];


    public async Task<IActionResult> OnGetAsync(int id)
    {
        Input = await _tireService.GetTireAsync(id);
        if (Input == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var dbEntity = await _tireService.GetTireAsync(Input.Id);
        if (dbEntity == null)
            return NotFound();

        // Map през позволените полета
        dbEntity.Made = Input.Made;
        dbEntity.Model = Input.Model;
        dbEntity.Width = Input.Width;
        dbEntity.Height = Input.Height;
        dbEntity.RimSize = Input.RimSize;
        dbEntity.Season = Input.Season;
        dbEntity.YearOfProduction = Input.YearOfProduction;
        dbEntity.Available = Input.Available;
        dbEntity.Description = Input.Description;

        await _tireService.UpdateTireAsync(dbEntity);
        return RedirectToPage("/Index");
    }
}
