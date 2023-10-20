using BusinessObjects;

namespace NguyenHongHiepSignalR.Hubs;

public interface ICustomerHub
{
    Task RegisterCustomer(Customer customer);
}
