using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync())
                return;
            session.Store(GetPreConfiguredProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>
        {
            new Product
            {
                Category =new List<string>{"Smart Phone", "Java Phone"},
                Description ="Naija Phone",
                Id = new Guid(),
                ImageFile="Image 101",
                Name= "Nokia",
                Price =400000,
            },
             new Product
            {
                Category =new List<string>{ "Java Phone"},
                Description ="Ibadan Phone",
                Id = new Guid(),
                ImageFile="Image 102",
                Name= "Itel",
                Price =4000,
            },
              new Product
            {
                Category =new List<string>{ "Touch Light Phone"},
                Description ="Ibadan Phone",
                Id = new Guid(),
                ImageFile="Image 103",
                Name= "Tecno",
                Price =15000,
            }

        };
    }
}
