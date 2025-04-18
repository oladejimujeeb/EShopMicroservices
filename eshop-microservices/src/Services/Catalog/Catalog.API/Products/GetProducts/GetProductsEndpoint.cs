
using Microsoft.AspNetCore.Http.HttpResults;
using OpenTelemetry.Trace;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductRequest(int? pageNumber, int? pagesize);
    public record GetProductResponse(IEnumerable<Product>Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products",async ([AsParameters] GetProductRequest request,ISender sender) =>
            {
                var query = new GetProductsQuery(request.pageNumber, request.pagesize); 
                var result = await sender.Send(query);
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
