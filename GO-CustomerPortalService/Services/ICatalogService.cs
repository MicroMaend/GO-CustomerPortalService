using GOCore;

public interface ICatalogService
{
    Task<List<Item>> GetAllItemsAsync();
    Task<Item?> GetItemByIdAsync(string id);
    Task<List<Item>> GetItemsByCategoryAsync(string category);
    Task AddItemAsync(Item item);
    Task UpdateItemAsync(string id, Item item);
    Task DeleteItemAsync(string id);
}
