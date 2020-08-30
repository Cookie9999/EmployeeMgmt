$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: "/Position/GetPositions",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.PositionId + '</td>';
                html += '<td>' + item.PositionDescription + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.PositionId + ')">Edit</a> | <a href="#" onclick="Delete(' + item.PositionId + ')">Delete</a></td>';
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
        PositionId: $('#PositionId').val(),
        PositionDescription: $('#Description').val(),
    };
    $.ajax({
        url: "/Position/CreatePosition",
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

function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        PositionId: $('#PositionId').val(),
        PositionDescription: $('#Description').val(),
    };
    $.ajax({
        url: "/Position/UpdatePosition",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#PositionId').val("");
            $('#Description').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function getbyID(PosId) {
    $('#Description').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Position/GetPositionById?position_id=" + PosId,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#PositionId').val(result.PositionId);
            $('#Description').val(result.PositionDescription);
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
            url: "/Position/DeletePosition?position_id=" + ID,
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

function clearTextBox() {
    $('#PositionId').val("");
    $('#Description').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}

function validate() {
    var isValid = true;
    if ($('#Description').val().trim() == "") {
        $('#Description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Description').css('border-color', 'lightgrey');
    }
    return isValid;
}