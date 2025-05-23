using GOCore;

public interface IAuctionService
{
    Task<List<allAuctionsViewModel>> GetAllAuctionsAsync();
    Task<Auction?> GetByIdAsync(Guid id);
}
