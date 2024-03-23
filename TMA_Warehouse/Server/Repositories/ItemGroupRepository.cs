using Microsoft.EntityFrameworkCore;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Server.Repositories
{
    public class ItemGroupRepository
    {
        private readonly Context context;

        public ItemGroupRepository(Context context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ItemGroup>> GetItemGroups()
        {
            return await context.ItemGroups.ToListAsync();
        }

        public async Task<ItemGroup> GetItemGroup(int id)
        {
            return await context.ItemGroups.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
