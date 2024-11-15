var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:'/admin/user/getall'},
        "columns": [
            { "data": "userName", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "status.statusName", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                data: { id:"id", lockoutEnd:"lockoutEnd"},
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                     
                        return `
                        <div class="text-center">
                            <a href="/admin/user/LoginHistory?userId=${data.id}" class="btn btn-info text-white" style="cursor:pointer; width:150px;">
                                <i class="bi bi-pencil-square"></i> login logs
                            </a>
                        </div>
                    `
                   
                },
                "width": "25%"
            }
        ]
    });
}
