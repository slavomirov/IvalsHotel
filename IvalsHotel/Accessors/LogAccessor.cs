using IvalsHotel.Data;
using IvalsHotel.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace IvalsHotel.Accessors;

public class LogAccessor
{
    private readonly AppDbContext _dbContext;

    public LogAccessor(AppDbContext dbContext)
	{
        this._dbContext = dbContext;
	}

    public async Task CreateAsync(Log input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        await _dbContext.Logs.AddAsync(input);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Log>> GetAllAsync() => await _dbContext.Logs.OrderByDescending(x => x.Date).ToListAsync();
}
