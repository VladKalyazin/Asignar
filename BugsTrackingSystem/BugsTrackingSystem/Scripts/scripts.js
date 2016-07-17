$(document).ready(function ($) {
    $(".clickable-row").click(function () {
        window.document.location = $(this).data("href");
    });    
});


$("input[type='checkbox']").change(function () {
    if ($(this).is(":checked")) {
        $(this).parent().addClass("background_change");
    } else {
        $(this).parent().removeClass("background_change");
    }
});

$(function () {
    $("#add_project, #add_task, #add_user").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });
   
    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-th-list close_popup\" aria-hidden=\"true\"> Create new</i>");
});


$(".open").click(function () {
    $("#add_task").css({ 'display': "inline" });
    $("#add_task").dialog("open");
    activatePlaceholders();
});

$("#opener").click(function () {
    $("#add_project").css({ 'display': "inline" });
    $("#add_project").dialog("open");
    activatePlaceholders();
});

$("#open_user").click(function () {
    $("#add_user").css({ 'display': "inline" });
    $("#add_user").dialog("open");
    activatePlaceholders();
});

$(function () {
    $("#add_user_to_project").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });

    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-users close_popup\" aria-hidden=\"true\"> Add user to project</i>");
});

$("#open_add_user").click(function () {
    $("#add_user_to_project").css({ 'display': "inline" });
    $("#add_user_to_project").dialog("open");
    activatePlaceholders();
});

$(function () {
    $("#add_filter").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });

    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-filter close_popup\" aria-hidden=\"true\"> Create new filter</i>");
});

$("#open_filter").click(function () {
    $("#add_filter").css({ 'display': "inline" });
    $("#add_filter").dialog("open");
    activatePlaceholders();
});

$(function () {
    $("#save_filter").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });

    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-filter close_popup\" aria-hidden=\"true\"> Save filter</i>");
});

$("#open_save_filter").click(function () {
    alert(11);
    $("#searchForm")
        .ajaxSubmit({ url: "/Manage/SaveFilterView", type: "post" })
        .success(function () {
            alert('success');
        });
    //$("#save_filter").css({ 'display': "inline" });
    //$("#save_filter").dialog("open");
    //activatePlaceholders();
});

$(document).ready(function() {
    function saveFilter() {
        
    }
});

$(function () {
    $("#change_password").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });
    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-key close_popup\" aria-hidden=\"true\"> Change password</i>");
});

$("#open_change").click(function () {
    $("#change_password").css({ 'display': "inline" });
    $("#change_password").dialog("open");
    activatePlaceholders();
});

$(function () {
    $("#edit_user").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });
    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-pencil-square-o close_popup\" aria-hidden=\"true\"> Edit user data</i>");
});

$("#open_edit_user").click(function () {
    $("#edit_user").css({ 'display': "inline" });
    $("#edit_user").dialog("open");
    activatePlaceholders();
});

$(function () {
    $("#edit_project").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });
    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-list close_popup\" aria-hidden=\"true\"> Edit project</i>");
});

$("#open_edit").click(function () {
    $("#edit_project").css({ 'display': "inline" });
    $("#edit_project").dialog("open");
    activatePlaceholders();
});

$(function () {
    $("#select_filter").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });
    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-filter close_popup\" aria-hidden=\"true\"> Choose filter</i>");
});

$("#open_filter").click(function () {
    $("#select_filter").css({ 'display': "inline" });
    $("#select_filter").dialog("open");
    activatePlaceholders();
});

$(function () {
    $("#edit_task").dialog
    ({
        modal: true,
        width: 600,
        height: 300,
        draggable: true,
        resizable: false,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        }
    });
    $(".ui-dialog-title").html("<i class=\"fa fa-pencil close_popup\" aria-hidden=\"true\"> Edit task</i>");
    $(".ui-dialog-titlebar-close").append("<i class=\"fa fa-times close_popup\" aria-hidden=\"true\"></i>");
});

$("#open_edit_task").click(function () {
    $("#edit_task").css({ 'display': "inline" });
    $("#edit_task").dialog("open");
    activatePlaceholders();
});

$("#projectEditSelect").change(function () {
    $.ajax({
        dataType: "json",
        url: "/Manage/GetUsersFromProject",
        data: { projectId: this.value },
    }).success(function (result) {
        var ddl = $("#userEditSelect");
        ddl.find("option").remove();
        ddl.selectpicker("refresh");
        $(result).each(function () {
            $(document.createElement("option"))
                .attr("value", this.Value)
                .text(this.Text)
                .appendTo(ddl);
        });
        ddl.selectpicker("refresh");
    });
});

$("#projectSelect").change(function () {
    $.ajax({
        dataType: "json",
        url: "/Manage/GetUsersFromProject",
        data: { projectId: this.value },
    }).success(function (result) {
        var ddl = $("#userSelect");
        ddl.find("option").remove();
        ddl.selectpicker("refresh");
        $(result).each(function () {
            $(document.createElement("option"))
                .attr("value", this.Value)
                .text(this.Text)
                .appendTo(ddl);
        });
        ddl.selectpicker("refresh");
    });
});

$("#userSelect").change(function () {
    $.ajax({
        dataType: "json",
        url: "/Manage/GetProjectsFromUser",
        data: { userId: this.value },
    }).success(function (result) {
        var ddl = $("#projectSelect");
        ddl.find("option").remove();
        ddl.selectpicker("refresh");
        $(result).each(function () {
            $(document.createElement("option"))
                .attr("value", this.Value)
                .text(this.Text)
                .appendTo(ddl);
        });
        ddl.selectpicker("refresh");
    });
});

$("#userEditSelect").change(function () {
    $.ajax({
        dataType: "json",
        url: "/Manage/GetProjectsFromUser",
        data: { userId: this.value },
    }).success(function (result) {
        var ddl = $("#projectEditSelect");
        ddl.find("option").remove();
        ddl.selectpicker("refresh");
        $(result).each(function () {
            $(document.createElement("option"))
                .attr("value", this.Value)
                .text(this.Text)
                .appendTo(ddl);
        });
        ddl.selectpicker("refresh");
    });
});

$('#searchForm').find('.btn-pagin').each(function () {
    var link = $(this);
    link.click(function (event) {
        event.preventDefault();
        var pageNumber = link.text();
        var input = $("<input>")
                    .attr("type", "hidden")
                    .attr("name", "page").val(pageNumber);
        $('#searchForm').append($(input));
        $('#searchForm').submit();
    });
});

function activatePlaceholders() {
    $("[placeholder]").parents("form").submit(function () {
        $(this).find("[placeholder]").each(function () {
            var input = $(this);
            if (input.val() == input.attr("placeholder")) {
                input.val("");
            }
        });
    });

    $("[placeholder]").focus(function () {
        var input = $(this);
        if (input.val() == input.attr("placeholder")) {
            input.val("");
            input.removeClass("placeholder");
            input.css({ 'color': "black", 'font-size': "100%" });
        }
    }).blur(function () {
        var input = $(this);
        if (input.val() == "" || input.val() == input.attr("placeholder")) {
            input.addClass("placeholder");
            input.val(input.attr("placeholder"));
            input.css({ 'color': "rgba(0, 0, 0, 0.5)", 'font-size': "80%" });
        }
    }).blur();
}




$("body").on("click", ".hasicon > i", function () {
    $.ajax({
        url: "/Manage/Home",
        data: { filterId: filterId },
        method: "POST"
    })
   .success(function () {
       $(el).parent().hide();
   })
   .error(function (mess) {
       //alert(mess);
        });
});

function hideFilter(el, filterId) {
    $.ajax({
        url: "/Manage/DeleteFilter/",
        data: { filterId: filterId },
        method: "POST"
    })
    .success(function () {
        $(el).parent().hide();
    })
    .error(function (mess) {
        //alert(mess);
        });
}

function hideUser(el, userId) {
    $.ajax({
        url: "/Manage/DeleteUser/",
        data: { userId: userId },
        method: "POST"
    })
    .success(function () {
        $(el).parent().hide();
    })
    .error(function (mess) {
        alert(mess);
    });
}

function hideProject(el, projectId) {
    $.ajax({
        url: "/Manage/DeleteProject/",
        data: { projectId: projectId },
        method: "POST"
    })
    .success(function () {
        $(el).parent().hide();
    })
    .error(function (mess) {
        //alert(mess);
    });
}

function resetAll() {
    $.ajax({
            type: "POST",
            url: "/Manage/Search"
        })
        .done(function() {
            // Clear the form
            $('input[type=text], textarea').val('');
            $('select').find('option').prop("selected", false);
            $('input[type=radio]').prop("checked", false);
            $('.dropdown-toggle').val('');
            $('li').siblings().removeClass("selected");
            $('.filter-option').html("Nothing selected");
            $('#searchForm').submit();
        });
}

function validate() {
    var submitFlag = true;
    var x = document.forms["form2"]["text"].value;
    if (x.length >= 200) {
        submitFlag = false;
        document.getElementById("validation_comment").innerHTML = "Ivalid length - no more than 200 characters!";
    }
    return submitFlag;
}

function validate_task() {
    var submitFlag = true;
    var x = document.forms["form_edit_task"]["Name"].value;
    var y = document.forms["form_edit_task"]["Description"].value;
    if (x.length > 200) {
        submitFlag = false;
        document.getElementById("validation").innerHTML = "The name of task has to be less than 200 characters!";
    }
    if (y.length > 500) {
        submitFlag = false;
        document.getElementById("validation").innerHTML = "The description has to be less than 500 characters!";
    }
    return submitFlag;
}

function validate_new_project() {
    var submitFlag = true;
    var letters = /^[A-Z]+$/;
    var x = document.forms["form_new_project"]["Key"].value;
    var y = document.forms["form_new_project"]["Name"].value;
    if (y.length > 30) {
        document.getElementById("validation_new_project").innerHTML = "Name has to be less than 30 characters";
        submitFlag = false;
    }
    if (x.length !== 3 || x.match(letters) == null) {
        submitFlag = false;
        document.getElementById("validation_new_project").innerHTML = "Key has to be uppercase and contain only 3 letters";
    }
    return submitFlag;
}

function validate_profile() {
    var submitFlag = true;
    var letters = /^[A-Za-z]+$/;
    var email = /^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$/;
    var x = document.forms["form_edit_profile"]["Name"].value;
    var y = document.forms["form_edit_profile"]["Surname"].value;
    var z = document.forms["form_edit_profile"]["Email"].value;
    if (x.length > 30) {
        document.getElementById("validation_edit_profile").innerHTML = "Name has to contain less than 30 letters";
        submitFlag = false;
    }
    if (x.match(letters) == null) {
        document.getElementById("validation_edit_profile").innerHTML = "Name has to contain letters";
        submitFlag = false;
    }
    if (y.length > 30) {
        document.getElementById("validation_edit_profile").innerHTML = "Surname has to contain less than 30 letters";
        submitFlag = false;
    }
    if (y.match(letters) == null) {
        document.getElementById("validation_edit_profile").innerHTML = "Surname has to contain letters";
        submitFlag = false;
    }
    if (z.match(email) == null) {
        submitFlag = false;
        document.getElementById("validation_edit_profile").innerHTML = "Email is not valid";
    }
    if (z.length > 35) {
        submitFlag = false;
        document.getElementById("validation_edit_profile").innerHTML = "Email has t contain less than 35 characters";
    }
    return submitFlag;
}

function validate_password() {
    var submitFlag = true;
    var password = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$/;
    var x = document.forms["form_change_password"]["Password"].value;
    var y = document.forms["form_change_password"]["RepeatPassword"].value;
    if (x.match(password) == null) {
        submitFlag = false;
        document.getElementById("validation_pasword").innerHTML = "The password is invalid!";
    }
    if (y !== x) {
        submitFlag = false;
        document.getElementById("validation_pasword").innerHTML = "The passwords doesn't match!";
    }
    return submitFlag;
}