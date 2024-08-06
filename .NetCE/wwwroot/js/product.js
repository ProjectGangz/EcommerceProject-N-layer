var dataTable;
$(document).ready(function () {   
    loadDataTable();
});
function loadDataTable() {
   dataTable = $('#tblData').DataTable({      
       "ajax": { url: '/admin/product/getall' },
       
        "columns": [
            { data: 'title' },
            { data: 'description' },
            { data: 'listPrice' },
            { data: 'listPrice50' },
            { data: 'listPrice100' },
            { data: 'category.categoryName' },
            {
                data: 'id',
                "render": function (data) {
                    return ` <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i>Edit
                            </a>
                            <a onClick=Delete("/admin/Product/Delete/${data}") class="btn btn-danger mx-2">
                                <i class="bi bi-trash-fill"></i>Delete
                            </a>
                        </div>`
                }
            }
        ]
   });
}
function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            });
        }
    });
}
//var dataTable; // Declare a variable to hold the DataTable instance

//$(document).ready(function () {
//    initializeDataTable();
//    loadDataTable();
//});

//function initializeDataTable() {
//    if ($.fn.DataTable.isDataTable('#tblData')) {
//        dataTable.destroy(); // Destroy the existing DataTable instance
//    }

//    dataTable = $('#tblData').DataTable({
//        paging: true,
//        searching: true,
//        ordering: true,
//        info: true
//    });
//}

//function loadDataTable() {
//    if ($.fn.DataTable.isDataTable('#tblData')) {
//        dataTable.destroy(); // Destroy the existing DataTable instance
//    }

//    dataTable = $('#tblData').DataTable({
//        "ajax": { url: '/admin/product/getall' },
//        "columns": [
//            { data: 'title' },
//            { data: 'description' },
//            { data: 'listPrice' },
//            { data: 'listPrice50' },
//            { data: 'listPrice100' },
//            { data: 'category.categoryName' }
//        ]
//    });
//}