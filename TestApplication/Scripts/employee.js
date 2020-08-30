$(document).ready(function () {
    loadData();
    $.get('/Position/GetPositions', function (data) {
        console.log(data)
        $.each(data, function (index, value) {
            console.log(index + value)
            $('<option >').val(value.PositionId).text(value.PositionDescription).appendTo("#Positions");
        })
    })
});

function loadData() {
    $.ajax({
        url: "/Employee/GetEmployees",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.EmployeeId + '</td>';
                html += '<td>' + item.EmployeeName + '</td>';
                html += '<td>' + item.DOB + '</td>';
                html += '<td>' + item.Position + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.EmployeeId + ')">Edit</a> | <a href="#" onclick="Delete(' + item.EmployeeId + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        EmployeeId: $('#EmployeeId').val(),
        EmployeeName: $('#Name').val(),
        DOB: $('#DOB').val(),
        Position: $('#Positions').val(),
    };
    $.ajax({
        url: "/Employee/CreateEmployee",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#DOB').val().trim() == "") {
        $('#DOB').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#DOB').css('border-color', 'lightgrey');
    }

    return isValid;
}

function clearTextBox() {
    $('#EmployeeId').val("");
    $('#Name').val("");
    $('#DOB').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}

function getbyID(EmpId) {
    $('#Name').css('border-color', 'lightgrey');
    $('#DOB').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Employee/GetEmployee?id=" + EmpId,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#EmployeeId').val(result.EmployeeId);
            $('#Name').val(result.EmployeeName);
            $('#DOB').val(result.DOB);
            $('#Positions').val(result.Position);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Employee/DeleteEmployee?id=" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        EmployeeId: $('#EmployeeId').val(),
        EmployeeName: $('#Name').val(),
        DOB: $('#DOB').val(),
        Position: $('#Positions').val(),
    };
    $.ajax({
        url: "/Employee/UpdateEmployee",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#EmployeeId').val("");
            $('#DOB').val("");
            $('#Name').val("");
            $('#Positions').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}