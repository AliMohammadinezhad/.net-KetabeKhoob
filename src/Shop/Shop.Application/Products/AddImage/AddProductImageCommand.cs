using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Products.RemoveImage.AddImage;

public record AddProductImageCommand(IFormFile ImageFile, long ProductId, int Order) : IBaseCommand;