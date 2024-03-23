using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Server
{
    public class Context : DbContext
    {
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<UnitOfMeasurement> UnitsOfMeasurements { get; set; }
        public DbSet<Item> Items { get; set; }

        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnCreateItemGroups(modelBuilder);
            OnCreateUnitsOfMeasurements(modelBuilder);
            OnCreateItems(modelBuilder);
        }

        private void OnCreateItemGroups(ModelBuilder modelBuilder)
        {
            var itemGroups = new List<ItemGroup>
            {
                new ItemGroup
                {
                    Id = 1,
                    Name="Group_1"
                },
                new ItemGroup
                {
                    Id = 2,
                    Name="Group_2"
                },
                new ItemGroup
                {
                    Id = 3,
                    Name="Group_3"
                }
            };

            modelBuilder.Entity<ItemGroup>().HasData(itemGroups);
        }

        private void OnCreateUnitsOfMeasurements(ModelBuilder modelBuilder)
        {
            var unitsOfMeasurements = new List<UnitOfMeasurement>
            {
                new UnitOfMeasurement
                {
                    Id = 1,
                    Name="Unit"
                },
                new UnitOfMeasurement
                {
                    Id = 2,
                    Name="Kilograms"
                }
            };

            modelBuilder.Entity<UnitOfMeasurement>().HasData(unitsOfMeasurements);
        }

        private void OnCreateItems(ModelBuilder modelBuilder)
        {
            var items = new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Name = "Bricks",
                    ItemGroupId = 1,
                    UnitOfMeasurementId = 1,
                    Quantity = 1000,
                    PriceWithoutVAT = 1m,
                    Status = "In Stock",
                    StorageLocation = "Warehouse A, Shelf 1",
                    ContantPerson = "John Smith",
                    PhotoURL = "brick_image.jpg"
                },
                new Item
                {
                    Id = 2,
                    Name = "Cement bags",
                    ItemGroupId = 1,
                    UnitOfMeasurementId = 2,
                    Quantity = 500,
                    PriceWithoutVAT = 25m,
                    Status = "In Stock",
                    StorageLocation = "Warehouse A, Shelf 2",
                    ContantPerson = "John Smith",
                    PhotoURL = "cement_image.jpg"
                },
                new Item
                {
                    Id = 3,
                    Name = "Concrete blocks",
                    ItemGroupId = 1,
                    UnitOfMeasurementId = 1,
                    Quantity = 800,
                    PriceWithoutVAT = 5m,
                    Status = "In Stock",
                    StorageLocation = "Warehouse A, Shelf 3",
                    ContantPerson = "John Smith",
                    PhotoURL = "concrete_blocks_image.jpg"
                },
                new Item
                {
                    Id = 4,
                    Name = "Roofing materials",
                    ItemGroupId = 2,
                    UnitOfMeasurementId = 1,
                    Quantity = 200,
                    PriceWithoutVAT = 15m,
                    Status = "In Stock",
                    StorageLocation = "Warehouse B, Shelf 4",
                    ContantPerson = "Michael Brown",
                    PhotoURL = "roofing_materials_image.jpg"
                },
                new Item
                {
                    Id = 5,
                    Name = "Lumber",
                    ItemGroupId = 1,
                    UnitOfMeasurementId = 1,
                    Quantity = 600,
                    PriceWithoutVAT = 2m,
                    Status = "In Stock",
                },
                new Item
                {
                    Id = 6,
                    Name = "Nails",
                    ItemGroupId = 2,
                    UnitOfMeasurementId = 1,
                    Quantity = 5000,
                    PriceWithoutVAT = 0.15m,
                    Status = "In Stock",
                    PhotoURL = "nails_image.jpg"
                },
                new Item
                {
                    Id = 7,
                    Name = "Paint",
                    ItemGroupId = 2,
                    UnitOfMeasurementId = 1,
                    Quantity = 100,
                    PriceWithoutVAT = 20m,
                    Status = "In Stock",
                    StorageLocation = "Warehouse B, Shelf 2",
                    ContantPerson = "Michael Brown",
                    PhotoURL = "paint_image.jpg"
                },
                new Item
                {
                    Id = 8,
                    Name = "Drywall sheets",
                    ItemGroupId = 2,
                    UnitOfMeasurementId = 1,
                    Quantity = 300,
                    PriceWithoutVAT = 17.5m,
                    Status = "In Stock",
                    StorageLocation = "Warehouse B, Shelf 3",
                    ContantPerson = "Michael Brown",
                },
                new Item
                {
                    Id = 9,
                    Name = "Insulation",
                    ItemGroupId = 1,
                    UnitOfMeasurementId = 1,
                    Quantity = 400,
                    PriceWithoutVAT = 35m,
                    Status = "In Stock",
                    StorageLocation = "Warehouse A, Shelf 4",
                    ContantPerson = "Olivia Anderson",
                },
                new Item
                {
                    Id = 10,
                    Name = "Plumbing fixtures",
                    ItemGroupId = 2,
                    UnitOfMeasurementId = 1,
                    Quantity = 50,
                    PriceWithoutVAT = 150m,
                    Status = "In Stock",
                    StorageLocation = "Warehouse B, Shelf 5",
                    PhotoURL = "plumbing_fixtures_image.jpg"
                },
                new Item
                {
                    Id = 11,
                    Name = "Electrical wiring",
                    ItemGroupId = 2,
                    UnitOfMeasurementId = 1,
                    Quantity = 2000,
                    PriceWithoutVAT = 7.49m,
                    Status = "In Stock",
                    ContantPerson = "Olivia Anderson",
                    PhotoURL = "electrical_wiring_image.jpg"
                },
            };

            modelBuilder.Entity<Item>().HasData(items);
        }
    }
}
