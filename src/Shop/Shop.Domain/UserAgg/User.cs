using Common;
using Common.Exceptions;
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
        IDomainUserService domainService)
    {
        Guard(phoneNumber, email, domainService);
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
        IDomainUserService domainService)
    {
        Guard(phoneNumber, email, domainService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }

    public static User RegisterUser(string phoneNumber, string email, string password, IDomainUserService domainService)
    {
        return new User("", "", phoneNumber, email, password, Gender.None, domainService);
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

    public void EditAddress(UserAddress address)
    {
        var oldAddress = Addresses.FirstOrDefault(a => a.Id == address.Id);
        if(oldAddress is null)
            throw new NullOrEmptyDomainDataException("Address Not Found");

        Addresses.Remove(oldAddress);
        Addresses.Add(address);
    }

    public void ChargeWallet(Wallet wallet)
    {
        Wallets.Add(wallet);
    } // TODO : do this later

    public void SetRoles(List<UserRole> roles)
    {
        roles.ForEach(x => x.UserId = Id);
        UserRoles.Clear();
        UserRoles.AddRange(roles);
    }


    private void Guard(string phoneNumber, string email, IDomainUserService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
        NullOrEmptyDomainDataException.CheckString(email, nameof(email));
        
        if (phoneNumber.Length != 11)
            throw new InvalidDomainDataException("شماره موبایل نامعتبر است.");

        if(email.IsValidEmail() == false)
            throw new InvalidDomainDataException("ایمیل نامعتبر است.");

        if(phoneNumber != PhoneNumber)
            if (domainService.IsPhoneNumberExist(phoneNumber))
                throw new InvalidDomainDataException("شماره موبایل تکراری است.");

        if (email != Email)
            if (domainService.IsEmailExist(email))
                throw new InvalidDomainDataException("ایمیل تکراری است.");

    }
}