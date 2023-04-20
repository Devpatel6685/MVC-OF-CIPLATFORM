
$(document).ready(function () {
    $('#staticBackdrop').on('show.bs.modal', function (event) {
        alert("modal called");
        var button = $(event.relatedTarget); // Button that triggered the modal
        var missionId = button.data('missionid'); // Extract missionid from data attribute
        console.log("the mission id", missionId);
        var modal = $(this);
        modal.find('.modal-footer button.btn.apply').on('click', function () {
            DeleteMission(missionId); // Call DeleteMission function with missionid parameter
            //console.log("this is the id of mission", missionId);
        });
    });
});

function DeleteMission(missionId) {
    alert("delete misson called");
    $.ajax({
        url: '/admin/DeleteMission',
        type: 'GET',
        data: {
            missionid: missionId
        },
        success: function (result) {
            $('#loadPartialView').html($(result).find('#loadPartialView').html());
            toastr.success("mission is deleted ");

        }
    });
}




$(document).on('click', '.mis li', function (e) {
    e.preventDefault();
    $('.mis li').each(function () {
        $(this).removeClass('misactive');
    })
    $(this).addClass('misactive');
    filtermissions();
});
function filtermissions() {

    var pageIndex = $('.mis .misactive a').attr('id');

    $.ajax({
        url: "/Admin/Mission",
        type: "POST",
        data: {
            //  SearchInputdata: keyword,
            pageindex: pageIndex
        },
        success: function (response) {
            alert("hello");
            $('.table').html($(response).find('.table').html());
            $('.pagination').html($(response).find('.pagination').html());


        }
    })
}
$(document).ready(function () {

    $('#searchmission').keyup(function () {
        alert('hi');
        $('.pagination .misactive').removeClass('misactive');
        filterSearch();

    });
});

function filterSearch() {
    var keyword = $('#searchmission').val();

    $.ajax({
        url: "/Admin/Mission",
        type: "POST",
        data: {
            SearchInputdata: keyword,

        },
        success: function (response) {
            alert('called');

            $('.table').empty().html($(response).find('.table').html());
            $('.page').empty().html($(response).find('.page').html());

        }
    })
}

$(function () {
    $('#missionpage').on('click', function () {

        alert('success');
        $.ajax({
            url: '/admin/missionadd',
            type: 'GET',
            success: function (result) {
                $('#loadPartialView').html(result);
            }
        });
    });
});



/*
$(document).on('click', '.paginationmis li', function (e) {
    e.preventDefault();
    $('.paginationmis li').each(function () {
        $(this).removeClass('missionactive');
    })
    $(this).addClass('missionactive');
    console.log($(this).children().attr("id"))
    filtermission();
});
function filtermission() {
    var pageIndex = $('.paginationmis .missionactive a').attr('id');
    $.ajax({
        url: "/Admin/Mission",
        type: "POST",
        data: {
            //  SearchInputdata: keyword,
            pageindex: pageIndex
        },
        success: function (result) {

            $('.table').html($(result).find('.table').html());
            $('.paginationmis').html($(result).find('.paginationmis').html());

            // Change the URL in the browser to reflect the updated page state
            // history.pushState(null, null, "/Admin/UserpageInAdmin?pageindex=" + pageIndex);

        }
    })
}*/