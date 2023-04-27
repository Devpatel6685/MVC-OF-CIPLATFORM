function getmissions() {
    $('form')[0].reset();
    $('form')[1].reset();
    var mission = $('#mission').val();
    $.ajax({
        url: "/Volunteer/missions",
        type: "GET",
        success: function (result) {
            $('.edit').html("Submit");
            $('#missionTime').empty();
            $('#missionTime').append('<option value="">Select Mission</option>')
            $(result.time).each(function (i, data) {
                if (data.missionId != mission) {
                    $('#missionTime').append('<option value="' + data.missionId + '" id="' + data.missionId + '">' + data.title + '</option>')
                }
            })
            $('#missionGoal').empty();
            $('#missionGoal').append('<option>Select Mission</option>')
            $(result.goal).each(function (i, data) {
                if (data.missionId != mission) {
                    $('#missionGoal').append('<option value="' + data.missionId + '" id="' + data.missionId + '">' + data.title + '</option>')
                }
            })
        }
    })
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
                url: "/Volunteer/deleteTimesheet",
                data: {
                    id: id,
                },
                type: "POST",
                success: function (response) {
                    Swal.fire(
                        'Deleted!',
                        'Your timesheet has been deleted.',
                        'success'
                    )
                    $('#result').html($(response).find('#result').html());
                }
            })
        }
    })
}



function edit(missionType, missionid, timesheetId) {
    $('.edit').html("Edit");
    $.ajax({
        url: "/Volunteer/getTimesheet",
        data: {
            timesheetid: timesheetId
        },
        type: "get",
        success: function (result) {
            var date = new Date(Date.parse(result.date));
            date.setMinutes(date.getMinutes() - date.getTimezoneOffset());
            var isoDate = date.toISOString().slice(0, 10)
            $('span').each(function () {
                $(this).html("");
            })
            if (missionType == "time") {
                $('#timesheetId').val(timesheetId)
                $('#dateTime').val(isoDate);
                $('#hour').val(result.hour);
                $('#minute').val(result.minute);
                $('#messageTime').val(result.message);
            } else {
                $('form')[1].reset();
                $('#goalsheetId').val(timesheetId)
                $('#dateGoal').val(isoDate);
                $('#action').val(result.action);
                $('#messageGoal').val(result.message);
            }
        }
    })
    var newOption = $("<option selected>").val(missionid).text($('#mission-' + missionid).html());
    if (missionType == "time") {
        $('#missionTime').empty();
        $("#missionTime").append(newOption).val(missionid);
        $('#missionTime').val(missionid);
        $('#timesheetId').val(timesheetId);
    } else {
        $('#missionGoal').empty();
        $("#missionGoal").append(newOption).val(missionid);
        $('#missionGoal').val(missionid);
        $('#goalsheetId').val(timesheetId);
    }


    console.log($('#mission-' + missionid).html())
}
