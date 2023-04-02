getmissions();
/*function getmissions() {
    var mission = $('#mission').val();
    $.ajax({
        url: "/Volunteer/missions",
        type: "GET",
        success: function (result) {
            $(result.time).each(function (i, data) {
                if (data.missionId != mission) {
                    $('#missionTime').append('<option value="' + data.missionId + '" id="' + data.missionId + '">' + data.title + '</option>')
                }
            })
            $(result.goal).each(function (i, data) {
                if (data.missionId != mission) {
                    $('#missionGoal').append('<option value="' + data.missionId + '" id="' + data.missionId + '">' + data.title + '</option>')
                }
            })
        }
    })
}*/
function getmissions() {
    var mission = $('#mission').val();
    $.ajax({
        url: "/Volunteer/missions",
        type: "GET",
        success: function (result) {
            $('.edit').html("Submit");
            $(result.time).each(function (i, data) {
                $('#missionTime').empty();
                $('#missionTime').append('<option>Select Mission</option>')
                if (data.missionId != mission) {
                    $('#missionTime').append('<option value="' + data.missionId + '" id="' + data.missionId + '">' + data.title + '</option>')
                }
            })
            $(result.goal).each(function (i, data) {
                $('#missionGoal').empty();
                $('#missionGoal').append('<option>Select Mission</option>')
                if (data.missionId != mission) {
                    $('#missionGoal').append('<option value="' + data.missionId + '" id="' + data.missionId + '">' + data.title + '</option>')
                }
            })
        }
    })
}

function edit(missionType, missionid) {
    $('.edit').html("Edit");
    var newOption = $("<option selected>").val(missionid).text($('#mission-' + missionid).html());
    if (missionType == "time") {
        $('#missionTime').empty();
        $("#missionTime").append(newOption).val(missionid);
        $('#missionTime').val(missionid);

    } else {
        $('#missionGoal').empty();
        $("#missionGoal").append(newOption).val(missionid);
        $('#missionGoal').val(missionid);
    }


    console.log($('#mission-' + missionid).html())
}