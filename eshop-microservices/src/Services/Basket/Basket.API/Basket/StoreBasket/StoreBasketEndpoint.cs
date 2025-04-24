
using Basket.API.Basket.GetBasket;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string userName);
public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async(StoreBasketRequest request, ISender sender) => 
        {
            var command = request.Adapt<StoreBasketCommand>();
            var result = await sender.Send(command);
            var response = result.userName;
            return Results.Created($"/basket/{response}", response);            
        }).WithName("StoreBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Store Basket ")
            .WithDescription("Store Shoppping cart basket");
    }
}
