using GOCore;

public interface IAuctionService
{
    Task<List<Auction>> GetAllAuctionsAsync();
    Task<Auction?> GetByIdAsync(Guid id);
}
