using Common.Query;

namespace Shop.Query.Orders.DTOs;

public class OrderAddressDto : BaseDto
{
    public long OrderId { get; set; }
    public string Province { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string PostalAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string NationalCode { get; set; }
}