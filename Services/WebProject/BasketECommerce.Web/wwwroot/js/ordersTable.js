var dataTable;

$(function () {
    dataToTable();
})

function dataToTable() {

    dataTable = $('#ordersTable').DataTable({
        "ajax": {
            method: 'GET',
            url: '/Orders/GetAllOrders',
            dataSrc: 'orders'
        },
        "columns": [
            { data: "emailAddress", "width": "10%" },
            { data: { firstName: "firstName", lastName: "lastName" },
                "render": function (data) {
                    return data.firstName + " " + data.lastName
                }
            , "width": "10%" },
            { data: "addressLine", "width": "10" },
            { data: "createdAt", "width": "10" },
            { data: "status", "width": "10" },
            { data: "totalPrice", "width": "10" },
            {

                data: "id",
                "className": "text-center",
                "render": function (data) {
                    return `<div class="w-75 btn-group text-center">
                            <a href="/Orders/OrderDetails?orderId=${data}" class="btn btn-outline-info mx-2">
                            <i class="bi bi-pencil-square"></i> Details
                            </a>
                            </div>
                    `;
                },
                "width": "35%"
            },
        ]
    });

}