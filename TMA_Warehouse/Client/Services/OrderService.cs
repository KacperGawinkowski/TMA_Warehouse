namespace TMA_Warehouse.Client.Services
{
    public class OrderService
    {
        private readonly HttpClient httpClient;
        public OrderService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
    }
}
