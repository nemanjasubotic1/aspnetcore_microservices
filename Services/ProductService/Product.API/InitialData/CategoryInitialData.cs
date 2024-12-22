using Marten;
using Marten.Schema;
using Services.ProductService.ProductCategory.API.Models;

namespace Services.ProductService.ProductCategory.API.InitialData;

public class CategoryInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Category>().AnyAsync())
            return;


        session.Store<Category>(GetInitialCategories());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Category> GetInitialCategories() => new List<Category>()
    {
        new Category
        {
            Id = new Guid("4968b544-a938-4fbc-8735-c8e6fbfae449"),
            Name = "Action",
            Description = "Great action movies"
        },
        new Category
        {
            Id = new Guid("c071cebf-b162-4644-9b20-84b58fb0e75c"),
            Name = "SciFi",
            Description = "Thrilling movies of future"
        }
    };
}
