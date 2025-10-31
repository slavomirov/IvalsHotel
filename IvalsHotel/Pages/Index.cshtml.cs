using IvalsHotel.Data.Models;
using IvalsHotel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IvalsHotel.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly TireService _tireService;

    public IndexModel(TireService tireService, LogService logService)
    {
        this._tireService = tireService;
    }

    [BindProperty(SupportsGet = true)]
    public int? FilterWidth { get; set; }
    [BindProperty(SupportsGet = true)]
    public int? FilterHeight { get; set; }
    [BindProperty(SupportsGet = true)]
    public int? FilterRimSize { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? FilterSeason { get; set; }
    
    public List<Tire> Tires { get; set; }

    public List<int> Widths { get; set; }
    public List<int> Heights { get; set; }
    public List<int> RimSizes { get; set; }
    public List<string> Seasons { get; set; }

    public async Task OnGetAsync()
    {
        var allTires = await _tireService.GetTiresAsync();

        var filteredTires = allTires.AsQueryable();

        if (FilterWidth.HasValue)
            filteredTires = filteredTires.Where(t => t.Width == FilterWidth.Value);

        if (FilterHeight.HasValue)
            filteredTires = filteredTires.Where(t => t.Height == FilterHeight.Value);

        if (FilterRimSize.HasValue)
            filteredTires = filteredTires.Where(t => t.RimSize == FilterRimSize.Value);

        if (!string.IsNullOrEmpty(FilterSeason))
            filteredTires = filteredTires.Where(t => t.Season.ToString() == FilterSeason);

        Tires = filteredTires.ToList();

        Widths = allTires
            .Where(t =>
                (!FilterHeight.HasValue || t.Height == FilterHeight) &&
                (!FilterRimSize.HasValue || t.RimSize == FilterRimSize) &&
                (string.IsNullOrEmpty(FilterSeason) || t.Season.ToString() == FilterSeason))
            .Select(t => t.Width)
            .Distinct()
            .OrderBy(w => w)
            .ToList();

        Heights = allTires
            .Where(t =>
                (!FilterWidth.HasValue || t.Width == FilterWidth) &&
                (!FilterRimSize.HasValue || t.RimSize == FilterRimSize) &&
                (string.IsNullOrEmpty(FilterSeason) || t.Season.ToString() == FilterSeason))
            .Select(t => t.Height)
            .Distinct()
            .OrderBy(h => h)
            .ToList();

        RimSizes = allTires
            .Where(t =>
                (!FilterWidth.HasValue || t.Width == FilterWidth) &&
                (!FilterHeight.HasValue || t.Height == FilterHeight) &&
                (string.IsNullOrEmpty(FilterSeason) || t.Season.ToString() == FilterSeason))
            .Select(t => t.RimSize)
            .Distinct()
            .OrderBy(r => r)
            .ToList();

        Seasons = allTires
            .Where(t =>
                (!FilterWidth.HasValue || t.Width == FilterWidth) &&
                (!FilterHeight.HasValue || t.Height == FilterHeight) &&
                (!FilterRimSize.HasValue || t.RimSize == FilterRimSize))
            .Select(t => t.Season.ToString())
            .Distinct()
            .OrderBy(s => s)
            .ToList();
    }

    public async Task<IActionResult> OnPostDecreaseAvailability(int id)
    {
        var tire = await _tireService.GetTireAsync(id);
        if (tire != null && tire.Available > 0)
        {
            tire.Available -= 1;
            await _tireService.UpdateTireAsync(tire);
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var tire = await _tireService.GetTireAsync(id);
        if (tire != null)
        {
            await _tireService.DeleteTireAsync(id);
        }

        return RedirectToPage();
    }

}
