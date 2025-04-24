
using Basket.API.Basket.GetBasket;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketRequest(string userName);
    public record DeleteBasketResponse(bool IsSucsess);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async(string userName, ISender sender) => 
            {
                var result = await sender.Send(new DeleteBasketCommand(userName));
                var response = result.IsSuccess;
                return Results.Ok(response);
            }).WithName("DeleteBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Basket ")
            .WithDescription("Delete Shoppping cart basket");
        }
    }
}
