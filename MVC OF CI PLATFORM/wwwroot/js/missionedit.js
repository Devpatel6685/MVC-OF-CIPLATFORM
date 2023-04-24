getcountry();
getThemes();
$(document).ready(function () {
    $('#countryselect').on('change', function () {
        var countryId = $('#countryselect').val();
        console.log("country id", countryId);
        $.ajax({
            url: "/Admin/City",
            type: "GET",
            data: {
                countryid: countryId
            },
            success: function (result) {
                $('#cityselect').empty();
                if (result.length === 0) {
                    $('#cityselect').html('<option>No cities slected</option>');
                } else {
                    $('#cityselect').prepend('<option value="" selected>Select City</option>');
                    $.each(result, function (i, data) {
                        /* if (missionid != data.missionId) {*/
                        $('#cityselect').append('<option value="' + data.cityId + '" id="' + data.cityId + '">' + data.name + '</option>');
                        /*}*/

                    })
                }
            }
        })
    });
});
function getThemes() {
    $.ajax({
        url: "/Admin/GetThemes",
        type: "GET",

        success: function (result) {

            if (result.length === 0) {
                $('#themeselect').html('<option>No countries selected</option>');
            } else {
                $.each(result, function (i, data) {
                    /* if (missionid != data.missionId) {*/
                    $('#themeselect').append('<option value="' + data.missionThemeId + '" id="' + data.missionThemeId + '">' + data.title + '</option>');
                    /*}*/

                })
            }
        }
    })
}
function getcountry() {
    $.ajax({
        url: "/Admin/Country",
        type: "GET",

        success: function (result) {

            if (result.length === 0) {
                $('#mission').html('<option>No countries selected</option>');
            } else {
                $.each(result, function (i, data) {
                    /* if (missionid != data.missionId) {*/
                    $('#countryselect').append('<option value="' + data.countryId + '" id="' + data.countryId + '">' + data.name + '</option>');
                    /*}*/

                })
            }
        }
    })
}
function Mission() {
    alert('change call');
    var type = $('#avail12').val();
    console.log("type of mission", type);
    if (type == "time") {
        $('#timefields').removeClass('d-none');
        $('#goalfields').addClass('d-none');


    }
    else {
        $('#goalfields').removeClass('d-none');
        $('#timefields').addClass('d-none');
    }

}
/*function Mission() {
    alert('change')
    var type = $('#avail2').val();
    console.log("type", type);
    if (type == "time") {
        $('#totalseat').removeClass('d-none');
        $('#goalvalue').addClass('d-none');

    }
    else {
        $('#goalvalue').removeClass('d-none');
        $('#totalseat').addClass('d-none');
    }
}*/