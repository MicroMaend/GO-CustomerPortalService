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
        return await _http.GetFromJsonAsync<List<Auction>>($"{baseUrl}/auction") ?? new List<Auction>();
    }

    public async Task<Auction?> GetByIdAsync(Guid id)
    {
        return await _http.GetFromJsonAsync<Auction>($"{baseUrl}/auction/{id}");
    }

    public async Task<List<Auction>> GetAuctionsByStartTimeAsync(DateTime startTime)
    {
        return await _http.GetFromJsonAsync<List<Auction>>($"{baseUrl}/auction/start?dateTime={startTime:o}") ?? new List<Auction>();
    }

    public async Task<List<Auction>> GetAuctionsByEndTimeAsync(DateTime endTime)
    {
        return await _http.GetFromJsonAsync<List<Auction>>($"{baseUrl}/auction/end?dateTime={endTime:o}") ?? new List<Auction>();
    }

    public async Task<List<Auction>> GetAuctionsByStatusAsync(string status)
    {
        return await _http.GetFromJsonAsync<List<Auction>>($"{baseUrl}/auction/status?status={status}") ?? new List<Auction>();
    }

    public async Task<string?> GetAuctionWinnerAsync(Guid id)
    {
        return await _http.GetFromJsonAsync<string>($"{baseUrl}/auction/{id}/winner");
    }

    public async Task CreateAuctionAsync(Auction auction)
    {
        await _http.PostAsJsonAsync($"{baseUrl}/auction", auction);
    }

    public async Task UpdateAuctionAsync(Guid id, Auction auction)
    {
        await _http.PutAsJsonAsync($"{baseUrl}/auction/{id}", auction);
    }

    public async Task DeleteAuctionAsync(Guid id)
    {
        await _http.DeleteAsync($"{baseUrl}/auction/{id}");
    }

    public async Task<AuctionHouse?> GetAuctionHouseById(Guid id)
    {
        return await _http.GetFromJsonAsync<AuctionHouse>($"auctionhouse/{id}");
    }
}
