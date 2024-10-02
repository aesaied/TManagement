$(function () {


    var userDataTable = $('#tasksTable').dataTable({

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

            { "data": "title", "name": "title", "autoWidth": true },
            { "data": "taskDate", "name": "taskDate", "autoWidth": true, "render": DataTable.render.datetime('Do MMM YYYY') },
            { "data": "dueDate", "name": "dueDate", "autoWidth": true },
            {
                "data": "users", "name": "users.fullName", "autoWidth": true, "render": function (users) {


                    return users.map((user) => `<span class="badge bg-primary text-dark">${user}</span>`);
                }



            },

            {
                "data": "hasAttachments", "name": "HasAttachments", "autoWidth": true, "orderable": false,
                "render": function (hasAttachment) {

                    if (hasAttachment) {
                        return '<i class="fa fa-paperclip "/>';
                    }

                    return '';
                }
            },
           
            {
                "data": "currentStatus", "name": "currentStatus", "autoWidth": true, 'render': function (data) {


                    if (data == 0) {
                        return `<span class="badge bg-danger text-dark">New</span>`;
                    }
                    else if (data == 1) {
                        return `<span class="badge bg-warning">In progree</span>`;
                    }
                    else if (data == 2) {
                        return `<span class="badge bg-success">Complete</span>`;
                    }
                  

                }
            },

            {
                "data": "id", "name": "actions", "orderable": false, 'render': function (data, t, row) {

                    // if (row.currentStatus == 0) {
                    return `<div class="btn-group" role="group">
    <button id="btnGroupDrop1" type="button" class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
      Action
    </button>
    <ul class="dropdown-menu" aria-labelledby="btnGroupDrop1">
      <li><a class="dropdown-item" href="ETasks/Edit/${data}">Edit</a></li>
      <li><a class="dropdown-item" href="Etasks/View/${data}">View</a></li>
    </ul>
  </div>`;
                    // }
                    // else {
                    //     return "";
                    // }
                }
            }

        ]
    });


  
});