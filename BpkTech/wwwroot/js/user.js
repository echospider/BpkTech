$(document).ready(function () {
    $('#txtFirstName').focus();
    GetUsers();
    $('#btnSave').on('click', function (e) {
        if ($('#txtFirstName').val().trim() === '') {
            toastr.warning('Please enter First Name');
            $('#txtFirstName').focus();
            return;
        }
        else if ($('#txtLastName').val().trim() === '') {
            toastr.warning('Please enter Last Name');
            $('#txtLastName').focus();
            return;
        }
        else if ($('#txtCity').val().trim() === '') {
            toastr.warning('Please enter City');
            $('#txtCity').focus();
            return;
        }
        else if ($('#txtPhoneNumber').val().trim() === '') {
            toastr.warning('Please enter Phone Number');
            $('#txtPhoneNumber').focus();
            return;
        }
        SaveUser();
    });
});
function GetUsers() {
    var table = $('#users').DataTable();
    table.destroy();
    $('#users').empty();
    var host = $(location).attr('protocol') + '//' + $(location).attr('host');
    $.ajax({
        type: "GET",
        url: host + "/api/Users",
        async: true,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            console.log(data);
            table = $('#users').dataTable({
                data: data.result,
                columns: [
                    {
                        title: 'First Name',
                        data: 'firstName'
                    },
                    {
                        title: 'Last Name',
                        'data': 'lastName'
                    },
                    {
                        title: 'City',
                        'data': 'city'
                    },
                    {
                        title: 'Phone Number',
                        'data': 'phoneNumber'
                    },
                ],
                "columnDefs": [
                    { "width": "100px", "targets": [0, 1,2, 3] }
                ],
                "order": [[1, 'asc']] //Order by Last Name
            });
        },
        error: function (jqXHR, status) {
            toastr.error(status + ' ' + jqXHR.responseText);
        }
    });
};
function SaveUser() {
    var firstName = $('#txtFirstName').val();
    var lastName = $('#txtLastName').val();
    var city = $('#txtCity').val();
    var phoneNumber = $('#txtPhoneNumber').val();
    var _data = {
        firstName: firstName,
        lastName: lastName,
        city: city,
        phoneNumber: phoneNumber
    };
    $.ajax({
        type: "POST",
        url: "/api/UpdateUser",
        async: true,
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(_data),
        success: function (data) {
            var result = data.result;
            console.log(result);

            if (data.statusCode !== 200) {
                if (data.errors.length > 0) {
                    toastr.error(data.errors[0].errorMessage);
                    return;
                }
            }
            else {
                toastr.success('Successfully saved user.');
                $('#txtFirstName').val('');
                $('#txtLastName').val('');
                $('#txtCity').val('');
                $('#txtPhoneNumber').val('');
                GetUsers();
                $('#txtFirstName').focus();
            }
        },
        error: function (jqXHR, status) {
            toastr.error(status + ' ' + jqXHR.responseText);
        }
    });
};
