using System.Net.Http.Json;
using TMA_Warehouse.Shared.Models;

namespace TMA_Warehouse.Client.Services
{
    public class UnitOfMeasurementService
    {
        private readonly HttpClient httpClient;

        public UnitOfMeasurementService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<UnitOfMeasurement>> GetUnitsOfMeasurements()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<UnitOfMeasurement>>($"api/UnitOfMeasurement/GetUnitsOfMeasurements");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UnitOfMeasurement> GetUnitOfMeasurement(int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<UnitOfMeasurement>($"api/UnitOfMeasurement/GetUnitOfMeasurement/{id}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
