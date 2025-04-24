

using Basket.API.Data;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string userName):ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);
public class DeleteBasketCommandValidator:AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x=>x.userName).NotEmpty().WithMessage("Username cannot be empty");
    }
}
public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        //TODO: delete basket from database and cache
        try
        {
            await repository.DeleteBasket(command.userName, cancellationToken);
        }
        catch (System.Exception)
        {

            return new DeleteBasketResult(false);
        }
      
        return new DeleteBasketResult(true);
    }
}
