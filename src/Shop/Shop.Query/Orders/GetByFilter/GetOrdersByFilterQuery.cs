using Common.Query;
using Shop.Query.Comments.DTOs;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetByFilter;

public class GetOrdersByFilterQuery(OrderFilterParams filterParams)
    : QueryFilter<OrderFilterResult, OrderFilterParams>(filterParams);