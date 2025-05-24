using GOCore;

public interface IBiddingService
{
    Task PlaceBidAsync(Bidding bidding);
    Task DeleteBidAsync(Bidding bidding);
    Task<List<Bidding>> GetBidsByUserIdAsync(string userId);
    Task<List<Bidding>> GetBidsByAuctionIdAsync(string auctionId);
    Task<Bidding?> GetHighestBidAsync(string auctionId);
}
