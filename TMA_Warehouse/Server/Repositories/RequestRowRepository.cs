//using Microsoft.EntityFrameworkCore;
//using TMA_Warehouse.Shared.Models;

//namespace TMA_Warehouse.Server.Repositories
//{
//    public class RequestRowRepository
//    {
//        private readonly Context context;

//        public RequestRowRepository(Context context)
//        {
//            this.context = context;
//        }

//        public async Task<IEnumerable<RequestRow>> GetRequestRows()
//        {
//            return await context.RequestRows.ToListAsync();
//        }

//        public async Task<RequestRow> GetRequestRow(int id)
//        {
//            return await context.RequestRows.FirstOrDefaultAsync(x => x.Id == id);
//        }

//        public async Task AddRequestRow(RequestRow requestRow)
//        {
//            if (requestRow == null) throw new ArgumentNullException();

//            //requestRow.Id = context.RequestRows.ToList().Count + 1;
//            context.RequestRows.Add(requestRow);
//            context.SaveChanges();
//        }
//        public async Task UpdateRequestRow(int idRequestRow, RequestRow requestRow)
//        {
//            RequestRow requestRowToChange = await GetRequestRow(idRequestRow);
//            if (requestRowToChange == null) throw new ArgumentNullException();

//            requestRowToChange.Request = requestRow.Request;
//            requestRowToChange.Item = requestRow.Item;
//            requestRowToChange.UnitOfMeasurement = requestRow.UnitOfMeasurement;
//            requestRowToChange.Quantity = requestRow.Quantity;
//            requestRowToChange.PriceWithoutVAT = requestRow.PriceWithoutVAT;
//            requestRowToChange.Comment = requestRow.Comment;

//            context.SaveChanges();
//        }

//        public async Task RemoveRequestRow(int id)
//        {
//            RequestRow requestRowToBeRemoved = await GetRequestRow(id);
//            if (requestRowToBeRemoved == null) throw new ArgumentNullException();

//            context.RequestRows.Remove(requestRowToBeRemoved);
//            context.SaveChanges();
//        }
//    }
//}
