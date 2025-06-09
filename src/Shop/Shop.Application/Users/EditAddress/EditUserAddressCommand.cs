using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Users.EditAddress;


public record EditUserAddressCommand(
    long Id,
    long UserId,
    string Shire,
    string City,
    string PostalCode,
    string PostalAddress,
    PhoneNumber PhoneNumber,
    string Name,
    string Family,
    string NationalCode
    ) : IBaseCommand;