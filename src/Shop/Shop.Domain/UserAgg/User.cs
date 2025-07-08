using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Services;

namespace Shop.Domain.UserAgg;

public class User : AggregateRoot
{
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string AvatarName { get; private set; }
    public bool IsActive { get; private set; }
    public Gender Gender { get; private set; }
    public List<UserRole> UserRoles { get; }
    public List<Wallet> Wallets { get; }
    public List<UserAddress> Addresses { get; }
    public List<UserToken> Tokens { get; }

    private User()
    {
    }

    public User(
        string name,
        string family,
        string phoneNumber,
        string email,
        string password,
        Gender gender,
        IUserDomainService userDomainService)
    {
        Guard(phoneNumber, email, userDomainService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        AvatarName = "avatar.png";
        Password = password;
        Gender = gender;
        IsActive = true;
        UserRoles = new List<UserRole>();
        Wallets = new List<Wallet>();
        Addresses = new List<UserAddress>();
        Tokens = new List<UserToken>();
    }


    public void Edit(
        string name,
        string family,
        string phoneNumber,
        string email,
        Gender gender,
        IUserDomainService userDomainService)
    {
        Guard(phoneNumber, email, userDomainService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }

    public static User RegisterUser(string phoneNumber, string password, IUserDomainService userDomainService)
    {
        return new User("", "", phoneNumber, "", password, Gender.None, userDomainService);
    }

    public void ChangePassword(string newPassword)
    {
        NullOrEmptyDomainDataException.CheckString(newPassword, nameof(newPassword));
        Password = newPassword;
    }

    public void RemoveToken(long tokenId)
    {
        var token = Tokens.FirstOrDefault(x => x.Id == tokenId);
        if (token is null)
            throw new InvalidDomainDataException("Invalid Token Id");

        Tokens.Remove(token);
    }

    public void SetAvatar(string imageName)
    {
        if (string.IsNullOrWhiteSpace(imageName))
            imageName = "avatar.png";
        AvatarName = imageName;
    }

    public void AddAddress(UserAddress address)
    {
        address.UserId = Id;
        Addresses.Add(address);
    }

    public void DeleteAddress(long addressId)
    {
        var currentAddress = Addresses.FirstOrDefault(a => a.Id == addressId);
        if (currentAddress is null)
            throw new NullOrEmptyDomainDataException("Address Not Found");

        Addresses.Remove(currentAddress);
    }

    public void EditAddress(UserAddress address, long addressId)
    {
        var oldAddress = Addresses.FirstOrDefault(a => a.Id == addressId);
        if (oldAddress is null)
            throw new NullOrEmptyDomainDataException("Address Not Found");

        oldAddress.Edit(address.Shire, address.City, address.PostalCode, address.PostalAddress,
            address.PhoneNumber, address.Name, address.Family, address.NationalCode);
    }

    public void SetActiveAddress(long addressId)
    {
        var currentAddress = Addresses.FirstOrDefault(x => x.Id == addressId);
        if (currentAddress is null)
            throw new NullOrEmptyDomainDataException("Address Not Found");

        foreach (var address in Addresses)
        {
            address.SetDeActive();
        }

        currentAddress.SetActive();
    }

    public void ChargeWallet(Wallet wallet)
    {
        wallet.UserId = Id;
        Wallets.Add(wallet);
    } // TODO : do this later

    public void SetRoles(List<UserRole> roles)
    {
        roles.ForEach(x => x.UserId = Id);
        UserRoles.Clear();
        UserRoles.AddRange(roles);
    }

    public void AddToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        var activeTokensCount = Tokens.Count(token => token.RefreshTokenExpireDate > DateTime.Now);
        if (activeTokensCount >= 3)
            throw new InvalidDomainDataException("امکان استفاده بیشتر از 3 دستگاه همزمان وجود ندارد.");

        var token = new UserToken(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device);
        token.UserId = Id;
        Tokens.Add(token);
    }


    private void Guard(string phoneNumber, string email, IUserDomainService userDomainService)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));

        if (phoneNumber.Length != 11 || string.IsNullOrWhiteSpace(phoneNumber))
            throw new InvalidDomainDataException("شماره موبایل نامعتبر است.");

        if (!string.IsNullOrWhiteSpace(email))
            if (email.IsValidEmail() == false)
                throw new InvalidDomainDataException("ایمیل نامعتبر است.");

        if (phoneNumber != PhoneNumber)
            if (userDomainService.IsPhoneNumberExist(phoneNumber))
                throw new InvalidDomainDataException("شماره موبایل تکراری است.");

        if (email != Email)
            if (userDomainService.IsEmailExist(email))
                throw new InvalidDomainDataException("ایمیل تکراری است.");

    }
}