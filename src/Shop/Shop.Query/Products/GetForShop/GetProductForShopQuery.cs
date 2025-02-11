using Common.Query;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetForShop;

public class GetProductForShopQuery : QueryFilter<ProductShopResult, ProductShopFilterParam>
{
    public GetProductForShopQuery(ProductShopFilterParam filterParams) : base(filterParams)
    {
    }
}