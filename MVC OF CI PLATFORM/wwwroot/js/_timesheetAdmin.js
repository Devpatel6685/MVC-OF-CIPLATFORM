$(document).on('click', '.timesheetPage .page-item', function (e) {
    $('.timesheetPage .page-item').each(function () {
        $(this).removeClass('active');
    })
    $(this).addClass('active');
    renderTimesheet()

})

$('#timesheetlink').addClass('bg-white')
$('#timesheetlink a').css("color", "orange");

function renderTimesheet() {
    var keyword = $('#searchTimesheet').val();
    var pageIndex = $('.timesheetPage .active a').attr('id');
    $.ajax({
        url: "/Admin/Timesheet",
        type: "get",
        data: {
            pageIndex: pageIndex,
            keyword: keyword,
        },
        success: function (result) {
            $('#loadPartialView #timesheetData').html($(result).find('#timesheetData').html());
            /*            $('#loadPartialView #missionApplicationData').html($($.parseHTML(result)).filter("#missionApplicationData").html());*/
        }
    })
}

$('#searchTimesheet').keyup(function () {
    renderTimesheet();
})

function updateTimesheet(id, status) {
    if (status == 1) {
        var text = "You want to accept this?"
        var button = "Yes, accept it!"
    } else {
        var text = "You want to reject this?"
        var button = "Yes, reject it!"
    }
    Swal.fire({
        title: 'Are you sure?',
        text: text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: button
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/admin/editTimesheet",
                type: "POST",
                data: {
                    id: id,
                    status: status
                },
                success: function (result) {
                    window.location = "/admin/Timesheet";
                }
            });
        }
    });
}

function deleteTimesheet(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You want to delete this?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/admin/deleteTimesheet",
                type: "POST",
                data: {
                    id: id
                },
                success: function (response) {
                    Swal.fire(
                        'Deleted!',
                        'Timesheet has been deleted.',
                        'success'
                    )
                    $('#loadPartialView').html($(response).find('#loadPartialView').html());
                }
            });
        }
    })
}

