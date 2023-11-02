$(function () {
    const connection = new signalR.HubConnectionBuilder().withUrl("/car-hub").build();
    connection.on("ReceiveCreatedCar", function (car, manufacturer, supplier) {
        let newCarOption = `<option value="${car.carId}">${car.carName}</option>`;
        $('#CarId').append(newCarOption);
    });

    connection.on("ReceiveUpdatedCar", function (car, manufacturer, supplier) {
        let updatedCarOption = `<option value="${car.carId}">${car.carName}</option>`;
        $('#CarId option[value="' + car.carId + '"]').replaceWith(updatedCarOption);
    });

    connection.on("ReceiveDeletedCar", function (carId, result) {
        if (result == true) {
            $('#CarId option[value="' + carId + '"]').remove();
        }
    });

    connection.start()
        .catch(function (err) {
            return console.error(err.toString());
        });
});