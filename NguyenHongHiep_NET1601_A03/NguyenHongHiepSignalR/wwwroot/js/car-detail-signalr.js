$(function () {
    const connection = new signalR.HubConnectionBuilder().withUrl("/car-hub").build();

    connection.on("ReceiveUpdatedCar", function (car, manufacturer, supplier) {
        if (car.carId == id) {
            let carData =
            `<dt class="col-sm-2">
                Name
            </dt>
            <dd class="col-sm-10">
                ${car.carName}
            </dd>
            <dt class="col-sm-2">
                Description
            </dt>
            <dd class="col-sm-10">
                ${car.carDescription}
            </dd>
            <dt class="col-sm-2">
                Number Of Doors
            </dt>
            <dd class="col-sm-10">
                ${car.numberOfDoors}
            </dd>
            <dt class="col-sm-2">
                Seating Capacity
            </dt>
            <dd class="col-sm-10">
                ${car.seatingCapacity}
            </dd>
            <dt class="col-sm-2">
                Fuel Type
            </dt>
            <dd class="col-sm-10">
                ${car.fuelType}
            </dd>
            <dt class="col-sm-2">
                Year
            </dt>
            <dd class="col-sm-10">
                ${car.year}
            </dd>
            <dt class="col-sm-2">
                Price Per Day
            </dt>
            <dd class="col-sm-10">
                ${parseInt(car.carRentingPricePerDay).toLocaleString('vi-VN')} &#x20AB;
            </dd>
            <dt class="col-sm-2">
                Manufacturer
            </dt>
            <dd class="col-sm-10">
                ${manufacturer}
            </dd>
            <dt class="col-sm-2">
                Supplier
            </dt>
            <dd class="col-sm-10">
                ${supplier}
            </dd>
            <dt class="col-sm-2">
                Status
            </dt>
            <dd class="col-sm-10">
                    <span>${car.carStatus == 1 ? 'Active' : 'Deleted'}</span>
            </dd>`;
            $('#car-info').html(carData);
        }
    });

    connection.on("ReceiveDeletedCar", function (carId, result) {
        if (carId == id) {
            alert('This car has been deleted!');
            window.location.href = '/Admin/CarManagement';
        }
    });

    connection.start()
        .catch(function (err) {
            return console.error(err.toString());
        });
});