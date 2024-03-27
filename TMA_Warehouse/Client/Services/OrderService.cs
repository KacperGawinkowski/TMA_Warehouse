using System.Net.Http.Json;
using TMA_Warehouse.Shared.DTOs;

namespace TMA_Warehouse.Client.Services
{
    public class OrderService
    {
        private readonly HttpClient httpClient;
        public OrderService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders()
        {
            try
            {
                var res = await httpClient.GetFromJsonAsync<IEnumerable<OrderDTO>>("api/Order/GetOrders");

                return res;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<OrderDTO> GetOrder(int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<OrderDTO>($"api/Order/GetOrder/{id}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> AddOrder(OrderDTO orderDTO)
        {
            orderDTO.EmployeeName = "sus";
            //Console.WriteLine($"{orderDTO.}")

            return await httpClient.PostAsJsonAsync<OrderDTO>($"api/Order/AddOrder", orderDTO);
        }

        public async Task<HttpResponseMessage> UpdateOrderStatus(int id, string status)
        {
            OrderDTO item = await GetOrder(id);

            return await httpClient.PutAsJsonAsync<string>($"api/Order/ChangeOrderStatus/{id}", status);
        }

        public async Task<HttpResponseMessage> RemoveOrder(OrderDTO orderDto)
        {
            return await httpClient.DeleteAsync($"api/Order/RemoveOrder/{orderDto.Id}");
        }

        public async Task<HttpResponseMessage> ConfirmOrder(int id, OrderDTO orderDto)
        {
            return await httpClient.PutAsJsonAsync<OrderDTO>($"api/Order/ConfirmOrder/{id}", orderDto);
        }
    }
}
