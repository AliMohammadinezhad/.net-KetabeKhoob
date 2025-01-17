using Common.Domain;

namespace Shop.Domain.OrderAgg;

public class OrderAddress : BaseEntity
{
    public long OrderId { get; internal set; }
    public string Province { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string PostalAddress { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string NationalCode { get; private set; }
    public Order Order { get; private set; }

    private OrderAddress()
    {
        
    }
    public OrderAddress(
        long orderId,
        string province,
        string city,
        string postalCode,
        string postalAddress,
        string phoneNumber,
        string name,
        string family,
        string nationalCode)
    {
        OrderId = orderId;
        Province = province;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
    }
}