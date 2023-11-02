$(function () {
    const connection = new signalR.HubConnectionBuilder().withUrl("/car-hub").build();
    connection.on("ReceiveCreatedCar", function (car, manufacturer, supplier) {
        console.log("ReceiveCreatedCar");
        let newCarRow =
            `<tr id="car-${car.carId}">
                <td>
                    ${car.carId}
                </td>
                <td>
                    ${car.carName}
                </td>
                <td>
                    ${car.carDescription}
                </td>
                <td>
                    ${car.numberOfDoors}
                </td>
                <td>
                    ${car.seatingCapacity}
                </td>
                <td>
                    ${car.fuelType}
                </td>
                <td>
                    ${car.year}
                </td>
                <td>
                    ${parseInt(car.carRentingPricePerDay).toLocaleString('vi-VN')} ₫
                </td>
                <td>
                    ${manufacturer}
                </td>
                <td>
                    ${supplier}
                </td>
                <td class="car-status">
                        <span style="color: green;">Active</span>
                </td>
                <td>
                        <span>
                            <a href="/Admin/CarManagement/Edit?id=${car.carId}"><i class="fa fa-edit" style="font-size:24px"></i></a> |
                            <a href="/Admin/CarManagement/Details?id=${car.carId}"><i class="fa fa-eye" style="font-size:24px"></i></a> |
                            <a href="/Admin/CarManagement/Delete?id=${car.carId}"><i class="fa fa-trash-o text-danger" style="font-size:24px"></i></a>
                        </span>
                </td>
            </tr>`;
        $('#table-body').append(newCarRow);
    });

    connection.on("ReceiveUpdatedCar", function (car, manufacturer, supplier) {
        console.log("ReceiveUpdatedCar");
        let updatedCarRow =
            `
                <td>
                    ${car.carId}
                </td>
                <td>
                    ${car.carName}
                </td>
                <td>
                    ${car.carDescription}
                </td>
                <td>
                    ${car.numberOfDoors}
                </td>
                <td>
                    ${car.seatingCapacity}
                </td>
                <td>
                    ${car.fuelType}
                </td>
                <td>
                    ${car.year}
                </td>
                <td>
                    ${parseInt(car.carRentingPricePerDay).toLocaleString('vi-VN')} ₫
                </td>
                <td>
                    ${manufacturer}
                </td>
                <td>
                    ${supplier}
                </td>
                <td class="car-status">
                        <span style="color: green;">Active</span>
                </td>
                <td>
                        <span>
                            <a href="/Admin/CarManagement/Edit?id=${car.carId}"><i class="fa fa-edit" style="font-size:24px"></i></a> |
                            <a href="/Admin/CarManagement/Details?id=${car.carId}"><i class="fa fa-eye" style="font-size:24px"></i></a> |
                            <a href="/Admin/CarManagement/Delete?id=${car.carId}"><i class="fa fa-trash-o text-danger" style="font-size:24px"></i></a>
                        </span>
                </td>`;
        $('#car-' + car.carId).html(updatedCarRow);
    });

    connection.on("ReceiveDeletedCar", function (carId, result) {
        console.log("ReceiveDeletedCar");
        if (result == true) {
            $('#car-' + carId).remove();
        } else {
            $('#car-' + carId + ' .car-status').html('<span style="color: red;">Deleted</span>');
            $('#car-' + carId + ' td:last-child').empty();
        }
    });

    connection.start()
        .catch(function (err) {
            return console.error(err.toString());
        });
});