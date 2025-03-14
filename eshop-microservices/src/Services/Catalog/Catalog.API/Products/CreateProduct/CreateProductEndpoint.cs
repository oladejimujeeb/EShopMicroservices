
namespace Catalog.API.Products.CreateProduct
{
    public record CreatProductRequest(string Name, string Description, string ImageFile,
                                                  decimal Price, List<string> Category);
    public record CreatProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreatProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreatProductResponse>();
                return Results.Created($"/products{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreatProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        
        }
    }
}
