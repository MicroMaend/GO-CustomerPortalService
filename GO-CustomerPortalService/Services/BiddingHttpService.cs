using System.Net.Http.Json;
using System.Security.Cryptography;
using GOCore;

public class BiddingHttpService : IBiddingService
{
    private readonly HttpClient _http;
    private const string baseUrl = "http://localhost:5002";

    public BiddingHttpService(HttpClient http)
    {
        _http = http;
    }

    public async Task PlaceBidAsync(Bidding bid)
    {
        await _http.PostAsJsonAsync($"{baseUrl}/bidding/bid", bid);
    }

    public async Task DeleteBidAsync(Bidding bid)
    {
        await _http.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"{baseUrl}/bidding/bid")
        {
            Content = JsonContent.Create(bid)
        });
    }

    public async Task<List<Bidding>> GetBidsByUserIdAsync(string userId)
    {
        return await _http.GetFromJsonAsync<List<Bidding>>($"{baseUrl}/bidding/bids/user/{userId}");
    }

    public async Task<List<Bidding>> GetBidsByAuctionIdAsync(string auctionId)
    {
        return await _http.GetFromJsonAsync<List<Bidding>>($"{baseUrl}/bidding/bids/auction/{auctionId}");
    }

    public async Task<Bidding?> GetHighestBidAsync(string auctionId)
    {
        var response = await _http.GetAsync($"{baseUrl}/bidding/bid/highest/{auctionId}");

        if (!response.IsSuccessStatusCode)
            return null;

        if (response.Content.Headers.ContentLength == 0)
            return null;

        return await response.Content.ReadFromJsonAsync<Bidding>();
    }

}
