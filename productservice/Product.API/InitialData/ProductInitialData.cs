using Marten;
using Marten.Schema;
using ProductCategory.API.Models;

namespace ProductCategory.API.InitialData;

public class ProductInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        session.Store<Product>(GetInitialProducts());
        await session.SaveChangesAsync();
    }


    private static IEnumerable<Product> GetInitialProducts() => new List<Product>()
    {
        new Product
        {
            //Id = Guid.NewGuid(),
            Name = "Die Hard 1",
            Description = "Epic action",
            Price = 150,
            ImageUrl = "https://placehold.co/600x400",
            YearOfProduction =new DateOnly(1988, 1, 1),
            CategoryId = new Guid("4968b544-a938-4fbc-8735-c8e6fbfae449")
        },
        new Product
        {
            //Id = Guid.NewGuid(),
            Name = "Die Hard 2",
            Description = "Epic action",
            Price = 120,
            ImageUrl = "https://placehold.co/600x400",
            YearOfProduction =new DateOnly(1990, 1, 1),
            CategoryId = new Guid("4968b544-a938-4fbc-8735-c8e6fbfae449")
        },
        new Product
        {
            //Id = Guid.NewGuid(),
            Name = "Lord of the Rings 1",
            Description = "Epic adventure movie",
            Price = 120,
            ImageUrl = "https://placehold.co/600x400",
            YearOfProduction =new DateOnly(2000, 1, 1),
            CategoryId = new Guid("c071cebf-b162-4644-9b20-84b58fb0e75c")
        },
    };
}
