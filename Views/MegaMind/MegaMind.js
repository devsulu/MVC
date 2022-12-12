
$(document).ready(function () {
    $('#frmEmployee').validate({
        rules: {
            Name: { required: true },
            Email: { required: true, email: true },
            PhoneNo: { required: true, minlength:10 },
            State: { required: true },
            City: { required: true },
            Agree:{required: true}
        },
        messages: {
            Name: { required: 'Name is required' },
            Email: { required: 'Email is required', email: 'valid email required' },
            PhoneNo: {
                required: 'PhoneNo is required', minlength: 'Require 10 digit mobile no.'
            },
            State: { required: 'Pls enter state' },
            City: { required: 'City is required' },
            Agree: {required:"Please Accept terms and conditions"}
        },
        submitHandler: function (form) {
            $('#btnSave').prop('disabled', true);
        }
    });
});

function AddEmployee() {
    
    window.location ="/MegaMind/Manage?Id=0";
}
function AddEdit(id) {
    
    window.location = "/MegaMind/Manage?id=" + id;
}


function SaveEmployee() {
    $("#LoaderImage").show();
    $.ajax({
        type:'post',
        url: "/MegaMind/Manage?id=",
        data: $("#frmEmployee").serialize(),
        
        success: function (response, textStatus, jqXHR) {
            $("#LoaderImage").hide();
            $("#myModal").modal("hide");
            if (response.result == 'OK') {
                window.location.href = "Index";
            }
            else if(response.result == 'ERROR') {
                alert(response.message);
            }
        }
    });
}
function DeleteEmployee(id) {
    
    swal({
        title: "Are you sure?",
        text: "You want to delete Employee of '" + id + "'?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel please!",
        closeOnConfirm: true,
        closeOnCancel: true
    },

        function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    type: 'post',
                    dataType: 'json',
                    cache: false,
                    url: '/MegaMind/Delete',
                    data: { id: id},
                    success: function (response, textStatus, jqXHR) {
                        var objResult = response.result;
                        var objMsg = response.msg;
                        if (objResult == "OK") {

                            window.location = "/MegaMind/Index";
                        }
                        else {
                            alert(response.message);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });

            } else {
                swal("Cancelled", "Your imaginary file is safe :)", "error");
            }
        });
}

function Cancel() {
    window.location.href = "Index";
}

