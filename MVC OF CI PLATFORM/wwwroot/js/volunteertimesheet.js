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
                if (data.missionid != mission) {
                    $('#missionTime').append('<option value="' + data.missionId + '" id="' + data.missionId + '">' + data.title + '</option>')
                }
            })
            $('#missionGoal').empty();
            $('#missionGoal').append('<option value="">Select Mission</option>')
            $(result.goal).each(function (i, data) {
                if (data.missionId != mission) {
                    $('#missionGoal').append('<option value="' + data.missionId + '" id="' + data.missionId + '">' + data.title + '</option>')
                }
            })
        }
    })
}

$('#goalForm').submit(function (event) {
    debugger;
    $('#validaction').addClass('d-none');
    event.preventDefault();
    var validgoalvalue = parseInt($('#goalvalue1').val()) - parseInt($('#totalgoalachieve').val());
    console.log($('#action').val());
    console.log(validgoalvalue);
    if ($('#action').val() == "") {
        $('#validaction').removeClass('d-none');
    }
    else if (validgoalvalue == 0) {
        $('#validaction').removeClass('d-none').text("Your Goal is achieved you can't edit");
    }
    else if (parseInt($('#action').val()) >= validgoalvalue) {
        $('#validaction').removeClass('d-none').text('The value must be less than ' + validgoalvalue);
    }

    else if ($('#goalForm').valid()) {

        $('#validaction').addClass('d-none');

        $('#goalForm')[0].submit();
    }
});

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

$('#missionGoal').change(function () {
    $.ajax({
        url: "/Volunteer/getGoal",
        data: {
            id: parseInt($(this).val())
        },
        success: function (result) {
            var modal = $('#staticBackdrop2');
            modal.find('#goalvalue1').val(result.goal);
            modal.find('#totalgoalachieve').val(result.sum);
        }
    })
})

function edit(missionType, missionid, timesheetId) {
    debugger;
    $('.edit').html("Edit");
    $.ajax({
        url: "/Volunteer/getTimesheet",
        data: {
            timesheetid: timesheetId
        },
        type: "get",
        success: function (result) {
            debugger;
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
                var button = $('#timesheet-' + timesheetId); // Button that triggered the modal
                console.log(button.html())
                var goalvalue = button.data('goalvalue');
                var totalgoalachieve = button.data('totalachieved');
                var modal = $('#staticBackdrop2');
                modal.find('#goalvalue1').val(goalvalue);
                modal.find('#totalgoalachieve').val(totalgoalachieve);
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

