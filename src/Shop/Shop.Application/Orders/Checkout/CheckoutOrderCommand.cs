using Common.Application;

namespace Shop.Application.Orders.Checkout;

public record CheckoutOrderCommand(
    long UserId,
    string Province,
    string City,
    string PostalCode,
    string PostalAddress,
    string PhoneNumber,
    string Name,
    string Family,
    string NationalCode) : IBaseCommand;