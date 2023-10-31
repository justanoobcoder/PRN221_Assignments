using BusinessObjects;
using Microsoft.AspNetCore.SignalR;

namespace NguyenHongHiepSignalR.Hubs;

public class CarHub : Hub<ICarHub>
{
    public async Task ReceiveCreatedCar(CarInformation carInformation, string manufacturer, string supplier)
    {
        await Clients.All.ReceiveCreatedCar(carInformation, manufacturer, supplier);
    }

    public async Task ReceiveUpdatedCar(CarInformation carInformation, string manufacturer, string supplier)
    {
        await Clients.All.ReceiveUpdatedCar(carInformation, manufacturer, supplier);
    }

    public async Task ReceiveDeletedCar(int carId, bool result)
    {
        await Clients.All.ReceiveDeletedCar(carId, result);
    }
}
