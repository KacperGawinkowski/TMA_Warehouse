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

        public async Task AddItem(Item item)
        {
            if (item == null) throw new ArgumentNullException();

            //item.Id = context.Items.ToList().Count + 1;
            context.Items.Add(item);
            context.SaveChanges();
        }
        public async Task UpdateItem(int idItem, Item item)
        {
            Item itemToChange = await GetItem(idItem);
            if (itemToChange == null) throw new ArgumentNullException();

            itemToChange.Name = item.Name;
            itemToChange.ItemGroup = item.ItemGroup;
            itemToChange.UnitOfMeasurement = item.UnitOfMeasurement;
            itemToChange.Quantity = item.Quantity;
            itemToChange.PriceWithoutVAT = item.PriceWithoutVAT;
            itemToChange.Status = item.Status;
            itemToChange.StorageLocation = item.StorageLocation;
            itemToChange.ContantPerson = item.ContantPerson;
            itemToChange.PhotoURL = item.PhotoURL;

            context.SaveChanges();
        }

        public async Task RemoveItem(int id)
        {
            Item itemToBeRemoved = await GetItem(id);
            if (itemToBeRemoved == null) throw new ArgumentNullException();

            context.Items.Remove(itemToBeRemoved);
            context.SaveChanges();
        }

    }
}
