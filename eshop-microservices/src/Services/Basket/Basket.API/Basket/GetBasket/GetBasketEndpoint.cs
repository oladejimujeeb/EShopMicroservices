

namespace Basket.API.Basket.GetBasket;
    //public record GetBasketRequest(string userName);
    public record GetBasketResponse(ShoppingCart ShoppingCart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("basket/{userName}", async (string userName, ISender sender) => 
            {
                var result = await sender.Send(new GetBasketQuery(userName));
                var response = result.Adapt<GetBasketResponse>();
                return response;
            }).WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Basket ")
            .WithDescription("Get Basket using username"); 
        }
    }
