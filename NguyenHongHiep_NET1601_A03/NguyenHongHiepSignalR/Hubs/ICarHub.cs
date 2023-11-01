using BusinessObjects;

namespace NguyenHongHiepSignalR.Hubs;

public interface ICarHub
{
    Task ReceiveCreatedCar(CarInformation carInformation, string manufacturer, string supplier);
    Task ReceiveUpdatedCar(CarInformation carInformation, string manufacturer, string supplier);
    Task ReceiveDeletedCar(int carId, bool result);
}
