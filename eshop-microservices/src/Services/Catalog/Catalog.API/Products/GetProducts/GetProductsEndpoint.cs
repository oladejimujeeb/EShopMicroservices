
using Microsoft.AspNetCore.Http.HttpResults;
using OpenTelemetry.Trace;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductResponse(IEnumerable<Product>Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                var resonpse = result.Adapt<GetProductResponse>();
                return Results.Ok(resonpse);
            }).
            WithName("GetProducts")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Product");
            
        }
    }
}
