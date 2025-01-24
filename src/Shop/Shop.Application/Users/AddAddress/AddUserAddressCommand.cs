using Common.Application;
using Common.Domain.ValueObjects;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.AddAddress;

public record AddUserAddressCommand(
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