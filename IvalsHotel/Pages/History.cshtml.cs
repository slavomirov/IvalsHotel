using IvalsHotel.Data.Models;
using IvalsHotel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IvalsHotel.Pages
{
    [Authorize]
    public class HistoryModel : PageModel
    {
        private readonly LogService _logService;

        public HistoryModel(LogService logService)
        {
            _logService = logService;
        }

        public List<Log> Logs { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public LogType? TypeFilter { get; set; }

        public async Task OnGetAsync()
        {
            Logs = await _logService.GetAllAsync();

            if (!string.IsNullOrEmpty(Search))
            {
                Logs = Logs
                    .Where(l => l.Description.Contains(Search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (TypeFilter.HasValue)
            {
                Logs = Logs
                    .Where(l => l.Type == TypeFilter.Value)
                    .ToList();
            }
        }

        public async Task<IActionResult> OnPostClearAsync() => RedirectToPage(new { Search = (string?)null, TypeFilter = (LogType?)null });
    }
}