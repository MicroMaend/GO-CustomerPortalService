using GOCore;

public static class AuctionService
{
    public static List<Auction> Auctions { get; } = new()
    {
        new Auction
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Item = new Item
            {
                Name = "Guldarmbånd",
                Description = "Elegant 18k guldarmbånd med diamanter – Nord Auktion",
                Category = "Smykker",
                Value = 10000,
                Images = new List<string> { "/images/placeholder.jpg" },
                Seller = new User() // AuctionHouse ikke længere nødvendig
            },
            AuctionStart = DateTime.Now,
            AuctionEnd = DateTime.Now.AddHours(6),
            HighestBid = new Bidding { Amount = 7500 }
        },
        new Auction
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Item = new Item
            {
                Name = "Dansk Designstol",
                Description = "Original Arne Jacobsen stol i læder – Grøn & Olsen",
                Category = "Møbler",
                Value = 12000,
                Images = new List<string> { "/images/placeholder.jpg" },
                Seller = new User()
            },
            AuctionStart = DateTime.Now,
            AuctionEnd = DateTime.Now.AddDays(3),
            HighestBid = new Bidding { Amount = 5000 }
        },
        new Auction
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Item = new Item
            {
                Name = "Olie på lærred – Skagensmaler",
                Description = "Originalt maleri af Michael Ancher – AntikVest",
                Category = "Maleri",
                Value = 25000,
                Images = new List<string> { "/images/placeholder.jpg" },
                Seller = new User()
            },
            AuctionStart = DateTime.Now.AddHours(-1),
            AuctionEnd = DateTime.Now.AddDays(1),
            HighestBid = new Bidding { Amount = 18000 }
        },
        new Auction
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Item = new Item
            {
                Name = "Vintage Rolex Submariner",
                Description = "Sjælden urmodel fra 1978 – Nord Auktion",
                Category = "Smykker",
                Value = 65000,
                Images = new List<string> { "/images/placeholder.jpg" },
                Seller = new User()
            },
            AuctionStart = DateTime.Now,
            AuctionEnd = DateTime.Now.AddDays(5),
            HighestBid = new Bidding { Amount = 62000 }
        },
        new Auction
        {
            Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            Item = new Item
            {
                Name = "Empire kommode",
                Description = "Håndlavet kommode i mahogni fra 1820 – Grøn & Olsen",
                Category = "Møbler",
                Value = 8000,
                Images = new List<string> { "/images/placeholder.jpg" },
                Seller = new User()
            },
            AuctionStart = DateTime.Now,
            AuctionEnd = DateTime.Now.AddHours(10),
            HighestBid = new Bidding { Amount = 4500}
        }
    };

    public static Auction GetById(Guid id)
    {
        return Auctions.FirstOrDefault(a => a.Id == id);
    }
}
