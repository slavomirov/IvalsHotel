using IvalsHotel.Accessors;
using IvalsHotel.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace IvalsHotel.Services;

public class TireService
{
    private readonly TireAccessor _tireAccessor;
    private readonly LogService _logService;

    public TireService(TireAccessor tireAccessor, LogService logService)
    {
        _tireAccessor = tireAccessor;
        _logService = logService;
    }

    public async Task<List<Tire>> GetTiresAsync() =>
        await _tireAccessor.GetAllAsync();

    public async Task<Tire?> GetTireAsync(int id) =>
        await _tireAccessor.GetByIdAsync(id);

    public async Task AddTireAsync(Tire tire)
    {
        if (tire == null) throw new ArgumentNullException(nameof(tire));

        await _logService.LogCreateAsync(tire);
        await _tireAccessor.AddAsync(tire);
    }

    public async Task UpdateTireAsync(Tire tire)
    {
        var oldTire = await _tireAccessor.GetByIdDetachedAsync(tire.Id);

        if (oldTire != null) 
        {
            if (tire.Available <= 0)
            {
                await _tireAccessor.DeleteAsync(tire.Id);
                await _logService.LogDeleteAsync(oldTire);
            }
            else
            {
                await _tireAccessor.UpdateAsync(tire);
                await _logService.LogUpdateAsync(oldTire, tire);
            }
        }
    }

    public async Task DeleteTireAsync(int id)
    {
        var tire = await _tireAccessor.GetByIdAsync(id);

        if (tire != null)
        {
            await _logService.LogDeleteAsync(tire);
            await _tireAccessor.DeleteAsync(id);
        }
    }
}
