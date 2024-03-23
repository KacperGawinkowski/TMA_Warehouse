using Microsoft.EntityFrameworkCore;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Server.Repositories
{
    public class ItemRepository
    {
        private readonly Context context;

        public ItemRepository(Context context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            return await context.Items.ToListAsync();
        }

        public async Task<Item> GetItem(int id)
        {
            return await context.Items.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
