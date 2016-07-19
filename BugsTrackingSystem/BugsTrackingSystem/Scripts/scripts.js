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
});

$("#opener").click(function () {
    $("#add_project").css({ 'display': "inline" });
    $("#add_project").dialog("open");
});

$("#open_user").click(function () {
    $("#add_user").css({ 'display': "inline" });
    $("#add_user").dialog("open");
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

function open_save_filter_popup() {
    var search = document.getElementById("save_search").value;
    var priorities = $("#save_priority").find("option:selected");
    var statuses = $("#save_status").find("option:selected");
    var projects = $("#save_project").find("option:selected");
    var assignees = $("#save_assignee").find("option:selected");
    
    $("#save_filter").css({ 'display': "inline" });
    $("#save_filter").dialog("open");

    var elemForm = $('#save_filter');
    var searchInput = elemForm.find('[name="Search"]')[0];
    var prioritiesDropDown = elemForm.find('[name="Priorities"]');
    var statusesDropDown = elemForm.find('[name="Statuses"]');
    var projectsDropDown = elemForm.find('[name="Projects"]');
    var assigneesDropDown = elemForm.find('[name="Assignees"]');

    var prioritiesArray = [];
    var statusesArray = [];
    var projectsArray = [];
    var assigneesArray = [];
    for (var i = 0; i < priorities.length; ++i) {
        prioritiesArray.push(priorities[i].value);
    }
    for (var i = 0; i < statuses.length; ++i) {
        statusesArray.push(statuses[i].value);
    }
    for (var i = 0; i < projects.length; ++i) {
        projectsArray.push(projects[i].value);
    }
    for (var i = 0; i < assignees.length; ++i) {
        assigneesArray.push(assignees[i].value);
    }

    searchInput.value = search;

    prioritiesDropDown.val(prioritiesArray);
    prioritiesDropDown.selectpicker('refresh');

    statusesDropDown.val(statusesArray);
    statusesDropDown.selectpicker('refresh');

    projectsDropDown.val(projectsArray);
    projectsDropDown.selectpicker('refresh');

    assigneesDropDown.val(assigneesArray);
    assigneesDropDown.selectpicker('refresh');
};

$("#save_filter_search").click(function () {
    var title = document.getElementById("search_title").value;
    var search = document.getElementById("Search").value;
    var priorities = $("#save_priority").find("option:selected");
    var statuses = $("#save_status").find("option:selected");
    var projects = $("#save_project").find("option:selected");
    var assignees = $("#save_assignee").find("option:selected");

    var prioritiesArray = [];
    var statusesArray = [];
    var projectsArray = [];
    var assigneesArray = [];
    for (var i = 0; i < priorities.length; ++i) {
        prioritiesArray.push(priorities[i].value);
    }
    for (var i = 0; i < statuses.length; ++i) {
        statusesArray.push(statuses[i].value);
    }
    for (var i = 0; i < projects.length; ++i) {
        projectsArray.push(projects[i].value);
    }
    for (var i = 0; i < assignees.length; ++i) {
        assigneesArray.push(assignees[i].value);
    }

    var submitFlag = true;
    if (title.length > 30) {
        submitFlag = false;
        document.getElementById("validation_search_filter_new").innerHTML = "Title of filter has to be less than 30 characters!";
    }
    if (search.length > 50) {
        submitFlag = false;
        document.getElementById("validation_search_filter_new").innerHTML = "Search has to be less than 50 characters!";
    }
    if (title.length == 0 || search.length == 0) {
        submitFlag = false;
        document.getElementById("validation_search_filter_new").innerHTML = "Fill all inputs";
    }

    if (submitFlag) {
        $.ajax({
            url: "/Manage/AddNewFilterFromSearch",
            method: "POST",
            data: {
                title: title,
                search: search,
                priorities: prioritiesArray,
                statuses: statusesArray,
                projects: projectsArray,
                assignees: assigneesArray
            }
        })
       .success(function () {
           alert('Filter was successfully added');
           $("#save_filter").css({ 'display': "none" });
           $("#save_filter").dialog("close");
           resetAll();
       })
       .error(function (mess) {
           //alert(mess);
       });
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
});

$(function () {
    $("#edit_photo").dialog
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

    $(".ui-dialog-title").replaceWith("<i class=\"fa fa-pencil close_popup\" aria-hidden=\"true\"> Edit photo</i>");
});

$("#open_edit_user_photo").click(function () {
    $("#edit_photo").css({ 'display': "inline" });
    $("#edit_photo").dialog("open");
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
});

$("#projectEditSelect").change(function () {
    var selectedId = $('#userEditSelect option:selected').val();
    $.ajax({
        dataType: "json",
        url: "/Manage/GetUsersFromProject",
        data: {
            projectId: this.value,
            selectedUserId: selectedId,
        },
    }).success(function (result) {
        var ddl = $("#userEditSelect");
        ddl.find("option").remove();
        ddl.selectpicker("refresh");
        $(result).each(function () {
            $(document.createElement("option"))
                .attr("value", this.Value)
                .text(this.Text)
                .prop("selected", this.Selected)
                .appendTo(ddl);
        });
        ddl.selectpicker("refresh");
    });
});

$("#projectSelect").change(function () {
    var selectedId = $('#userSelect option:selected').val();
    $.ajax({
        dataType: "json",
        url: "/Manage/GetUsersFromProject",
        data: {
            projectId: this.value,
            selectedUserId: selectedId,
        },
    }).success(function (result) {
        var ddl = $("#userSelect");
        ddl.find("option").remove();
        ddl.selectpicker("refresh");
        $(result).each(function () {
            $(document.createElement("option"))
                .attr("value", this.Value)
                .prop("selected", this.Selected)
                .text(this.Text)
                .appendTo(ddl);
        });
        ddl.selectpicker("refresh");
    });
});

$("#open_add_attachments").click(function () {
    $("#attachmens").css({ 'display': "inline" });
});


$("#open_new_attachments").click(function () {
    $("#attachmens_new").css({ 'display': "inline" });
});

$("#userSelect").change(function () {
    var selectedId = $('#projectSelect option:selected').val();
    $.ajax({
        dataType: "json",
        url: "/Manage/GetProjectsFromUser",
        data: {
            userId: this.value,
            selectedProjectId: selectedId
        },
    }).success(function (result) {
        var ddl = $("#projectSelect");
        ddl.find("option").remove();
        ddl.selectpicker("refresh");
        $(result).each(function () {
            $(document.createElement("option"))
                .attr("value", this.Value)
                .prop("selected", this.Selected)
                .text(this.Text)
                .appendTo(ddl);
        });
        ddl.selectpicker("refresh");
    });
});

$("#userEditSelect").change(function () {
    var selectedId = $('#projectSelect option:selected').val();
    $.ajax({
        dataType: "json",
        url: "/Manage/GetProjectsFromUser",
        data: {
            userId: this.value,
            selectedProjectId: selectedId
        },
    }).success(function (result) {
        var ddl = $("#projectEditSelect");
        ddl.find("option").remove();
        ddl.selectpicker("refresh");
        $(result).each(function () {
            $(document.createElement("option"))
                .attr("value", this.Value)
                .prop("selected", this.Selected)
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


function resetAll() {
    $.ajax({
        type: "POST",
        url: "/Manage/Search"
    })
        .done(function () {
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

function hideFilter(el, filterId) {
    var checkstr = confirm('Are you sure you want to delete this filter?');
    if (checkstr == true) {
        window.location.href = "/Manage/DeleteFilter?filterId=" + filterId
    }
    else {
        return false;
    }
}

function goto_filter() {
    var param = $('.MyFilters').find("#close_filter_hover:hover").length;
    if (param == 0 || param == null) {
        window.location.href = "/Manage/Search?filterId=" + filterId
    };
};


function goto_user() {
    var param = $('.MyUsers').find("#close_user_hover:hover").length;
    if(param == 0 || param == null) {
        window.location.href = "/Manage/Profile?userId=" + userId
    };
};

function goto_project() {
    var param = $('.MyProjects').find("#close_project_hover:hover").length;
    if (param == 0 || param == null) {
        window.location.href = "/Manage/Project?id=" + projId
    };
};

function goto_project_home() {
    var param = $('.MyProjectsHome').find("#close_project_home_hover:hover").length;
    if (param == 0 || param == null) {
        window.location.href = "/Manage/Project?id=" + projId
    };
}

function hideUser(el, userId) {
    var checkstr = confirm('Are you sure you want to delete this user');
    if (checkstr == true) {
        window.location.href = "/Manage/DeleteUser?userId=" + userId
    }
    else {
        return false;
    }
}

function hideProject(el, projectId) {
    var checkstr = confirm('Are you sure you want to delete this project?');
    if (checkstr == true) {
        window.location.href = "/Manage/DeleteProject?projectId=" + projectId
    }
    else {
        return false;
    }
}

function deleteDefect() {
    var checkstr = confirm('Are you sure you want to delete this defect?');
    if (checkstr == true) {
        window.location.href = "/Manage/DeleteDefect?defId=" + defId + "&projId=" + projId
    }
    else {
        return false;
    }
}

function delete_user_from_project(el, projId, userId) {
    var checkstr = confirm('Are you sure you want to delete this user from project?');
    if (checkstr == true) {
        window.location.href = "/Manage/DeleteUserFromProject?projId=" + projId + "&userId=" + userId
    }
    else {
        return false;
    }
}

function delete_user_from_project_profile(el, projId, userId) {
    var checkstr = confirm('Are you sure you want to delete this user from project?');
    if (checkstr == true) {
        window.location.href = "/Manage/DeleteUserFromProjectProfile?projId=" + projId + "&userId=" + userId
    }
    else {
        return false;
    }
}

function delete_attachment_task(el, id, taskId) {
    var checkstr = confirm('Are you sure you want to delete this attachment?');
    if (checkstr == true) {
        window.location.href = "/Manage/DeleteAttachment?attachId=" + id + "&defectId=" + taskId
    }
    else {
        return false;
    }
}

function validate() {
    var submitFlag = true;
    var x = document.forms["form2"]["text"].value;
    if (x.length >= 200) {
        submitFlag = false;
        document.getElementById("validation_comment").innerHTML = "Ivalid length - no more than 200 characters!";
    }
    if (x.length == 0)
    {
        submitFlag = false;
        document.getElementById("validation_comment").innerHTML = "Fill the input first";
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
    if (x.length == 0 || y.length == 0) {
        submitFlag = false;
        document.getElementById("validation").innerHTML = "You have to fill all fields";
    }
    return submitFlag;
}

function validate_new_task() {
    var submitFlag = true;
    var x = document.forms["form_new_task"]["Name"].value;
    var y = document.forms["form_new_task"]["Description"].value;
    if (x.length > 200) {
        submitFlag = false;
        document.getElementById("validation_new_task").innerHTML = "The name of task has to be less than 200 characters!";
    }
    if (y.length > 500) {
        submitFlag = false;
        document.getElementById("validation_new_task").innerHTML = "The description has to be less than 500 characters!";
    }
    if (x.length == 0 || y.length == 0) {
        submitFlag = false;
        document.getElementById("validation").innerHTML = "You have to fill all fields";
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
    if (x.length == 0 || y.length == 0) {
        submitFlag = false;
        document.getElementById("validation").innerHTML = "You have to fill all fields";
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
        document.getElementById("validation_edit_profile").innerHTML = "Email has to contain less than 35 characters";
    }
    if (x.length == 0 || y.length == 0 || z.length == 0) {
        submitFlag = false;
        document.getElementById("validation").innerHTML = "You have to fill all fields";
    }
    return submitFlag;
}

function validate_password() {
    var submitFlag = true;
    var password = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
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
    if (x.length == 0 || y.length == 0) {
        submitFlag = false;
        document.getElementById("validation").innerHTML = "You have to fill all fields";
    }
    return submitFlag;
}

function validate_new_filter() {
    var submitFlag = true;
    var x = document.forms["form_add_filter"]["Name"].value;
    var y = document.forms["form_add_filter"]["Search"].value;
    if (x.length > 30) {
        submitFlag = false;
        document.getElementById("validation_filter").innerHTML = "The Name should be shorter than 30 symbols";
    }
    if (y.length > 50) {
        submitFlag = false;
        document.getElementById("validation_filter").innerHTML = "The Search should be shorter than 50 symbols";
    }
    if (x.length == 0 || y.length == 0) {
        submitFlag = false;
        document.getElementById("validation").innerHTML = "You have to fill all fields";
    }
    return submitFlag;
}

function validate_new_user() {
    var submitFlag = true;
    var letters = /^[A-Za-z]+$/;
    var email = /^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$/;
    var password = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
    var x = document.forms["form_new_user"]["FirstName"].value;
    var y = document.forms["form_new_user"]["Surname"].value;
    var z = document.forms["form_new_user"]["Email"].value;
    var p = document.forms["form_new_user"]["Password"].value;
    var q = document.forms["form_new_user"]["ConfirmPassword"].value;
    if (x.length > 30) {
        document.getElementById("validation_adding_user").innerHTML = "Name has to contain less than 30 letters";
        submitFlag = false;
    }
    if (x.match(letters) == null) {
        document.getElementById("validation_adding_user").innerHTML = "Name has to contain letters";
        submitFlag = false;
    }
    if (y.length > 30) {
        document.getElementById("validation_adding_user").innerHTML = "Surname has to contain less than 30 letters";
        submitFlag = false;
    }
    if (y.match(letters) == null) {
        document.getElementById("validation_adding_user").innerHTML = "Surname has to contain letters";
        submitFlag = false;
    }
    if (z.match(email) == null) {
        submitFlag = false;
        document.getElementById("validation_adding_user").innerHTML = "Email is not valid";
    }
    if (z.length > 35) {
        submitFlag = false;
        document.getElementById("validation_adding_user").innerHTML = "Email has t contain less than 35 characters";
    }
    if (p.match(password) == null) {
        submitFlag = false;
        document.getElementById("validation_adding_user").innerHTML = "The password is invalid!";
    }
    if (p !== q) {
        submitFlag = false;
        document.getElementById("validation_adding_user").innerHTML = "The passwords doesn't match!";
    }
    if (x.length == 0 || y.length == 0 || z.length == 0 || p.length == 0 || q.length == 0) {
        submitFlag = false;
        document.getElementById("validation").innerHTML = "You have to fill all fields";
    }
    return submitFlag;
}

function validate_new_projectname() {
    var submitFlag = true;
    var x = document.forms["new_project_name"]["NewName"].value;
    if (x.length > 30) {
        submitFlag = false;
        document.getElementById("validation_new_projectname").innerHTML = "The name of project has to be less than 30 characters!";
    }
    if (x.length == 0) {
        submitFlag = false;
        document.getElementById("validation").innerHTML = "You have to fill the field";
    }
    return submitFlag;
}