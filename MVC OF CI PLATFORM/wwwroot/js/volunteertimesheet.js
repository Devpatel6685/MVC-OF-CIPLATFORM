getmissions();
function getmissions() {
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
}