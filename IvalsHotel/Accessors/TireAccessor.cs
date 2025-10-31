using IvalsHotel.Data;
using IvalsHotel.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace IvalsHotel.Accessors;

public class TireAccessor
{
    private readonly AppDbContext _dbContext;

    public TireAccessor(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task AddAsync(Tire input)
    {
        await _dbContext.Tires.AddAsync(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int tireId)
    {
        var tire = await GetByIdAsync(tireId);

        _dbContext.Tires.Remove(tire!);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tire input)
    {
        var oldTire = await _dbContext.Tires.FirstAsync(x => x.Id == input.Id);

        _dbContext.Entry(oldTire).CurrentValues.SetValues(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Tire>> GetAllAsync() => await _dbContext.Tires.ToListAsync();

    public async Task<Tire?> GetByIdAsync(int id, bool detach = false) => await _dbContext.Tires.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Tire?> GetByIdDetachedAsync(int id) => await _dbContext.Tires.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
}
