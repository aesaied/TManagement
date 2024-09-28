$(function () {


    var userDataTable = $('#usersTable').dataTable({

        "ajax": { 'url': jsonUrl, 'method': 'post' },
        "processing": true,
        "serverSide": true,

        "columnDefs": [{
            "targets": [0],
            "visible": true,
            "searchable": false
        }],
        lengthMenu: [2, 10, 20, 50]
        ,

        "columns": [

            { "data": "fullName", "name": "FullName", "autoWidth": true },
            { "data": "email", "name": "email", "autoWidth": true },
            { "data": "country", "name": "city.fatherLookup.name", "autoWidth": true },
            { "data": "city", "name": "city.name", "autoWidth": true },
            { "data": "educationLevel", "name": "educationLevel.name", "autoWidth": true },
            {
                "data": "currentStatus", "name": "currentStatus", "autoWidth": true, 'render': function (data) {


                    if (data == 0) {
                        return `<span class="badge bg-warning text-dark">Need approval</span>`;
                    }
                    else if (data == 1) {
                        return `<span class="badge bg-success">Active</span>`;
                    }
                    else if (data == 2) {
                        return `<span class="badge bg-danger">stopped</span>`;
                    }
                    else if (data == 3) {
                        return `<span class="badge bg-dark">Cancelled</span>`;
                    }

                }
            },

            {
                "data": "id", "name": "actions", "orderable": false, 'render': function (data, t, row) {

                    // if (row.currentStatus == 0) {
                    return `<button onclick="changeStatus(${data})">ChangeStatus</button>`;
                    // }
                    // else {
                    //     return "";
                    // }
                }
            }

        ]
    });


    changeStatus = function (id) {


        $('#dvModels').load('/users/changeStatus/' + id, (status) => {


            $('#user-changeStatus-modal').modal('show');


            $('#btnSave').click((e) => {
                e.preventDefault();
                var id = $('#hdnId').val();
                var newStatus = $('#cmbStatus').val();

                $.post('/users/changeStatus', { id: id, status: newStatus }, function (status) {
                    $('#user-changeStatus-modal').modal('hide');
                    userDataTable.api().ajax.reload();
                    console.log(status);

                });

            });


        });

    };
});