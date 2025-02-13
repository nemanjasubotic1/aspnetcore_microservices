﻿var dataTable;

$(function () {
    dataToTable();
})

function dataToTable() {

    dataTable = $('#catTable').DataTable({
        "ajax": {
            method: 'GET',
            url: '/Category/GetAllCategories',
            dataSrc: 'categories'
        },
        "columns": [
            { data: "name", "width": "20%" },
            { data: "description", "width": "45%" },
            {

                data: "id",
                "className": "text-center",
                "render": function (data) {
                    return `<div class="w-75 btn-group text-center">
                            <a href="/Category/UpdateCategory?categoryId=${data}"" class="btn btn-outline-info mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a href="/Category/DeleteCategory?categoryId=${data}"" class="btn btn-outline-danger mx-2">
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