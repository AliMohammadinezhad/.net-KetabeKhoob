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
    public Gender Gender { get; private set; }
    public List<UserRole> UserRoles { get; private set; }
    public List<Wallet> Wallets { get; private set; }
    public List<UserAddress> Addresses { get; private set; }

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
        Password = password;
        Gender = gender;
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
        AvatarName = "avatar.png";
    }

    public static User RegisterUser(string phoneNumber, string password, IUserDomainService userDomainService)
    {
        return new User("", "", phoneNumber, null,password, Gender.None, userDomainService);
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
        if(oldAddress is null)
            throw new NullOrEmptyDomainDataException("Address Not Found");
        
        oldAddress.Edit(address.Shire, address.City, address.PostalCode, address.PostalAddress,
            address.PhoneNumber, address.Name, address.Family, address.NationalCode);
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


    private void Guard(string phoneNumber, string email, IUserDomainService userDomainService)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
        NullOrEmptyDomainDataException.CheckString(email, nameof(email));
        
        if (phoneNumber.Length != 11 || string.IsNullOrWhiteSpace(phoneNumber))
            throw new InvalidDomainDataException("شماره موبایل نامعتبر است.");

        if(email.IsValidEmail() == false)
            throw new InvalidDomainDataException("ایمیل نامعتبر است.");

        if(phoneNumber != PhoneNumber)
            if (userDomainService.IsPhoneNumberExist(phoneNumber))
                throw new InvalidDomainDataException("شماره موبایل تکراری است.");

        if (email != Email)
            if (userDomainService.IsEmailExist(email))
                throw new InvalidDomainDataException("ایمیل تکراری است.");

    }
}