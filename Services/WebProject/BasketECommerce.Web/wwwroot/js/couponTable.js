var dataTable;

$(function () {
    dataToTable();
})

function dataToTable() {

    dataTable = $('#couponTable').DataTable({
        "ajax": {
            method: 'GET',
            url: '/Coupon/GetAllCoupons',
            dataSrc: 'coupons'
        },
        "columns": [
            { data: "couponName", "width": "20%" },
            { data: "amount", "width": "10%" },
            { data: "expiryDate", "width": "15%" },
            { data: "isExpired", "width": "15%" },
            { data: "isEnabled", "width": "15%" },
            {

                data: null,
                "className": "text-center",
                "render": function (data, type, row) {
                    return `<div class="w-75 btn-group text-center">
                            <a href="/Coupon/EnableDisable?couponName=${row.couponName}" class="${row.isEnabled ? "btn btn-outline-danger mx-2" : "btn btn-outline-success mx-2"}">
                            <i class="bi bi-trash3"></i> ${row.isEnabled ? "Disable" : "Enable" }
                            </a>
                            </div>
                    `;
                },
                "width": "35%"
            },
        ]
    });

}