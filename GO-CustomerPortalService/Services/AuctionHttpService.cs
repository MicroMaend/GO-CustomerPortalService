using System.Net.Http.Json;
using GOCore;

public class AuctionHttpService : IAuctionService
{
    private readonly HttpClient _http;
    private const string baseUrl = "http://localhost:5003";

    public AuctionHttpService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<allAuctionsViewModel>> GetAllAuctionsAsync()
    {
        try
        {
            var response = await _http.GetFromJsonAsync<List<allAuctionsViewModel>>($"{baseUrl}/auction/overview");
            return response ?? new List<allAuctionsViewModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching auctions: {ex.Message}");
            throw;
        }
    }

    public async Task<Auction?> GetByIdAsync(Guid id)
    {
        return await _http.GetFromJsonAsync<Auction>($"{baseUrl}/auction/{id}");
    }
}
