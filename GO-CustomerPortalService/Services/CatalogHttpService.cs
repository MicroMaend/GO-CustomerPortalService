using System.Net.Http.Json;
using GOCore;

public class CatalogHttpService : ICatalogService
{
    private readonly HttpClient _http;
    private const string baseUrl = "http://localhost:5005";

    public CatalogHttpService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Item>> GetAllItemsAsync()
    {
        return await _http.GetFromJsonAsync<List<Item>>($"{baseUrl}/catalog/items");
    }

    public async Task<Item?> GetItemByIdAsync(string id)
    {
        return await _http.GetFromJsonAsync<Item>($"{baseUrl}/catalog/items/{id}");
    }

    public async Task<List<Item>> GetItemsByCategoryAsync(string category)
    {
        return await _http.GetFromJsonAsync<List<Item>>($"{baseUrl}/catalog/items/category/{category}");
    }

    public async Task AddItemAsync(Item item)
    {
        await _http.PostAsJsonAsync($"{baseUrl}/catalog/items", item);
    }

    public async Task UpdateItemAsync(string id, Item item)
    {
        await _http.PutAsJsonAsync($"{baseUrl}/catalog/items/{id}", item);
    }

    public async Task DeleteItemAsync(string id)
    {
        await _http.DeleteAsync($"{baseUrl}/catalog/items/{id}");
    }

}
