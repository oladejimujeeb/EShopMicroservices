

using Catalog.API.Products.DeleteProduct;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(Guid Id, string Name, string Description, string ImageFile,
                                                  decimal Price, List<string> Category):ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image is Required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    internal class CreateProductCommandHandler (IDocumentSession session, ILogger<CreateProductCommandHandler> logger /*IValidator<CreateProductCommand> validator*/) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Business logic to create product
            logger.LogInformation("CreateProductCommandHandler. Handle call with {@command}", command);
            var product = new Product 
            {
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
                Id = command.Id,
                Name= command.Name,
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            //todo
            //save to database
            return new CreateProductResult(product.Id);
        }
    }
}
