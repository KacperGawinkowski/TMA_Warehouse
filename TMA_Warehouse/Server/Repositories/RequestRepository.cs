//using AntDesign;
//using Microsoft.EntityFrameworkCore;
//using TMA_Warehouse.Shared.Models;

//namespace TMA_Warehouse.Server.Repositories
//{
//    public class RequestRepository
//    {
//        private readonly Context context;

//        public RequestRepository(Context context)
//        {
//            this.context = context;
//        }

//        public async Task<IEnumerable<Request>> GetRequests()
//        {
//            return await context.Requests.ToListAsync();
//        }

//        public async Task<IEnumerable<Request>> GetRequestsWithRelatedData()
//        {
//            return await context.Requests
//                //.Include(request => request.RequestRow)
//                .Include(request => request.Item)
//                .Include(request => request.UnitOfMeasurement)
//                .ToListAsync();
//        }

//        public async Task<Request> GetRequest(int id)
//        {
//            return await context.Requests.FirstOrDefaultAsync(x => x.Id == id);
//        }

//        public async Task AddRequest(Request request)
//        {
//            if (request == null) throw new ArgumentNullException();

//            //request.Id = context.Requests.ToList().Count + 1;
//            context.Requests.Add(request);
//            context.SaveChanges();
//        }
//        public async Task UpdateRequest(int idRequest, Request request)
//        {
//            Request requestToChange = await GetRequest(idRequest);
//            if (requestToChange == null) throw new ArgumentNullException();

//            //requestToChange.RequestRow = request.RequestRow;
//            requestToChange.EmployeeName = request.EmployeeName;
//            requestToChange.Item = request.Item;
//            requestToChange.UnitOfMeasurement = request.UnitOfMeasurement;
//            requestToChange.Quantity = request.Quantity;
//            requestToChange.PriceWithoutVAT = request.PriceWithoutVAT;
//            requestToChange.Status = request.Status;

//            context.SaveChanges();
//        }

//        public async Task RemoveRequest(int id)
//        {
//            Request requestToBeRemoved = await GetRequest(id);
//            if (requestToBeRemoved == null) throw new ArgumentNullException();

//            context.Requests.Remove(requestToBeRemoved);
//            context.SaveChanges();
//        }
//    }
//}
