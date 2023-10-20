using BusinessObjects;
using Microsoft.AspNetCore.SignalR;

namespace NguyenHongHiepSignalR.Hubs;

public class CustomerHub : Hub<ICustomerHub>
{
    public async Task RegisterCustomer(Customer customer)
    {
        await Clients.All.RegisterCustomer(customer);
    }
}
