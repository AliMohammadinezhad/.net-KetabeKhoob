using Common.Application;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Products.Delete;

public record DeleteProductCommand(long ProductId): IBaseCommand;


public class DeleteProductCommandHandler : IBaseCommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetTracking(request.ProductId);
        if(product == null)
            return OperationResult.NotFound();

        _productRepository.DeleteProduct(product);
        await _productRepository.Save();
        return OperationResult.Success();
    }
}