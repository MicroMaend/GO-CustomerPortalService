using GOCore;

public interface IAuctionService
{
    Task<List<Auction>> GetAllAuctionsAsync();
    Task<Auction?> GetByIdAsync(Guid id);
    Task<List<Auction>> GetAuctionsByStartTimeAsync(DateTime startTime);
    Task<List<Auction>> GetAuctionsByEndTimeAsync(DateTime endTime);
    Task<List<Auction>> GetAuctionsByStatusAsync(string status);
    Task<string?> GetAuctionWinnerAsync(Guid id);
    Task CreateAuctionAsync(Auction auction);
    Task UpdateAuctionAsync(Guid id, Auction auction);
    Task DeleteAuctionAsync(Guid id);
    Task<AuctionHouse?> GetAuctionHouseById(Guid id);

}
