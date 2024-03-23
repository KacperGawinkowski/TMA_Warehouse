using Microsoft.EntityFrameworkCore;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Server.Repositories
{
    public class UnitOfMeasureRepository
    {
        private readonly Context context;

        public UnitOfMeasureRepository(Context context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<UnitOfMeasurement>> GetUnitsOfMeasurements()
        {
            return await context.UnitsOfMeasurements.ToListAsync();
        }

        public async Task<UnitOfMeasurement> GetUnitOfMeasurement(int id)
        {
            return await context.UnitsOfMeasurements.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
