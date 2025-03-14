
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, decimal Price,
                        string ImageFile, string Description) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess=true);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p=>p.Id).NotEmpty().WithMessage("Product ID cannot be null");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Product Price must be greater than zero");
            RuleFor(p => p.Name).NotEmpty().Length(2, 150).WithMessage("Product name is required")
                .WithMessage("Product name must be between 2 and 150 characters");
        }
    }
    internal class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler. Handle call with {@command}", command);
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException();
            }
            product.Name = command.Name;
            product.Price = command.Price;
            product.ImageFile = command.ImageFile;
            product.Description = command.Description;
            product.Category = command.Category;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult();
        }
    }
}
