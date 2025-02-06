namespace Shop.Api.ViewModels.Users;

public class EditUserAddressViewModel
{
    public EditUserAddressViewModel(string shire, string city, string postalCode, string postalAddress,
        string phoneNumber, string name, string family, string nationalCode, bool activeAddress)
    {
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
        ActiveAddress = activeAddress;
    }
    public string Shire { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string PostalAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string NationalCode { get; set; }
    public bool ActiveAddress { get; set; }
}