using FeesTrackingApplication.Data;
using FeesTrackingApplication.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FeesTrackingApplication.Services
{
    public class BatchService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;

        public BatchService(HttpClient httpClient, AppDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task FetchAndSaveBatchesAsync()
        {
            try
            {
                var apiURL = "https://feestracking.freeprojectapi.com/api/Batches";
                var apiResponse = await _httpClient.GetFromJsonAsync<BatchApiResponse>(apiURL);

                if (apiResponse == null || !apiResponse.Result || apiResponse.Data == null)
                {
                    Console.WriteLine("Failed to fetch batches or data is empty.");
                    return;
                }

                var apiData = apiResponse.Data;

                Console.WriteLine($"Fetched {apiData.Count} batches from external API.");


                int addedCount = 0;

                foreach (var item in apiData)
                {
                    // Add only if not exists
                    if (!_context.Batches.Any(x => x.BatchId == item.BatchId))
                    {
                        _context.Batches.Add(item);
                        addedCount++;
                    }
                }

                await _context.SaveChangesAsync();
                Console.WriteLine($"Saved {addedCount} new batches to local DB.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching batches: " + ex.Message);
                throw; // This will propagate and show in your Developer Exception Page
            }

        }
    }
}
