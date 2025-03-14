
namespace Catalog.API.Products.GetProductBy_Category
{
    public record GetProductByCategoryQuery(string category):IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> products);
    internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) : 
        IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryQueryHandler. Handle call with {@query}", query);
            var product = await session.Query<Product>().Where(p=>p.Category.Contains(query.category)).ToListAsync();
            return new GetProductByCategoryResult(product);
        }
    }
}
