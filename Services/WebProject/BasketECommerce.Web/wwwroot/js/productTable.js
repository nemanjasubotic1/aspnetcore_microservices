var dataTable;

$(function () {
    dataToTable();
})

function dataToTable() {

    dataTable = $('#catTable').DataTable({
        "ajax": {
            method: 'GET',
            url: '/Product/GetAllProducts',
            dataSrc: 'products'
        },
        "columns": [
            { data: "name", "width": "15%" },
            { data: "description", "width": "15%" },
            { data: "price", "width": "10%" },
            { data: "yearOfProduction", "width": "15%" },
            { data: "category", "width": "15%" },
            {

                data: "id",
                "className": "text-center",
                "render": function (data) {
                    return `<div class="w-75 btn-group text-center">
                            <a href="/Product/UpdateProduct?productId=${data}"" class="btn btn-outline-info mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a href="/Product/DeleteProduct?productId=${data}"" class="btn btn-outline-danger mx-2">
                            <i class="bi bi-trash3"></i> Delete
                            </a>
                            </div>
                    `;
                },
                "width": "35%"
            },
        ]
    });

}