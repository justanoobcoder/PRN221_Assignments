$(function () {
    console.log("register-connection.js");
    const connection = new signalR.HubConnectionBuilder().withUrl("/customer-hub").build();
    connection.on("RegisterCustomer", function (customer) {
        let newCustomerRow = 
        `<tr>
            <td>
                ${customer.customerId}
            </td>
            <td>
                ${customer.customerName}
            </td>
            <td>
                ${customer.telephone}
            </td>
            <td>
                ${customer.email}
            </td>
            <td>
                ${new Date(customer.customerBirthday).toLocaleDateString() }
            </td>
            <td>
                    <span style="color: green;">Active</span>
            </td>
            <td>
                    <span>
                        <a href="/Admin/CustomerManagement/Edit?id=${customer.customerId}"><i class="fa fa-edit" style="font-size:24px"></i></a> |
                        <a href="/Admin/CustomerManagement/Details?id=${customer.customerId}"><i class="fa fa-eye" style="font-size:24px"></i></a> |
                        <a href="/Admin/CustomerManagement/Delete?id=${customer.customerId}"><i class="fa fa-trash-o text-danger" style="font-size:24px"></i></a>
                    </span>
            </td>
        </tr>`;
        $('#table-body').append(newCustomerRow);
    });

    connection.start()
        .catch(function (err) {
            return console.error(err.toString());
        });
});