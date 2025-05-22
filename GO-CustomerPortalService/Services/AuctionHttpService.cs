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

    public async Task<List<Auction>> GetAllAuctionsAsync()
    {
        var response = await _http.GetFromJsonAsync<List<Auction>>($"{baseUrl}/auction");
        return response ?? new List<Auction>();
    }

    public async Task<Auction?> GetByIdAsync(Guid id)
    {
        return await _http.GetFromJsonAsync<Auction>($"{baseUrl}/auction/{id}");
    }
}
