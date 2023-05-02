const dropZone = document.getElementById("drop-zone");
const fileInput = document.getElementById("file-input");
const imagePreview = document.getElementById("image-preview");
const uploadedFiles = new Set();

if (listData != null) {
    var files = new Array();
    for (var img = 0; img < listData.length; img++) {
        var byteCharacters = atob(listData[img].toString().split(',')[1]);
        var byteNumbers = new Array(byteCharacters.length);
        for (var i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        var byteArray = new Uint8Array(byteNumbers);
        var blob = new Blob([byteArray], { type: "image/jpeg" });
        var file = new File([blob], "image" + i + ".jpg", { type: "image/jpeg" });
        files.push(file);
    }
    handleFiles(files);
}

dropZone.addEventListener("click", () => {
    fileInput.click();
});

dropZone.addEventListener("dragover", (event) => {
    event.preventDefault();
    dropZone.classList.add("dragover");
});

dropZone.addEventListener("dragleave", () => {
    dropZone.classList.remove("dragover");
});

dropZone.addEventListener("drop", (event) => {
    event.preventDefault();
    dropZone.classList.remove("dragover");
    const files = event.dataTransfer.files;
    handleFiles(files);
});

fileInput.addEventListener("change", () => {
    const files = fileInput.files;
    handleFiles(files);
});

function handleFiles(files) {
    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        if (!file.type.startsWith("image/") && !file.type.startsWith("video/")) continue;
        if (uploadedFiles.has(file.name)) {
            alert(`File "${file.name}" has already been uploaded.`);
            continue;
        }
        uploadedFiles.add(file.name);
        const image = document.createElement("img");
        image.classList.add("image-preview");
        const imageContainer = document.createElement("div");
        imageContainer.classList.add("image-container");
        const removeImage = document.createElement("div");
        removeImage.innerHTML = "&#10006;";
        removeImage.classList.add("remove-image");
        removeImage.addEventListener("click", () => {
            uploadedFiles.delete(file.name);
            imageContainer.remove();
        });
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            image.src = reader.result;
            imageContainer.appendChild(image);
            imageContainer.appendChild(removeImage);
            imagePreview.appendChild(imageContainer);
        };
    }
}

getmissions();
tinymce.init({
    selector: '#storytext',
    plugins: 'link image code',
    toolbar: 'undo redo | bold italic | fontsizeselect | alignleft aligncenter alignright alignjustify | superscript subscript ',
    height: 300
});

function getmissions() {
    var mission = $('#mission_id').val();
    $.ajax({
        url: "/volunterrstory/missions",
        type: "GET",
        success: function (result) {
            $(result).each(function (i, data) {
                if (data.missionId != mission) {
                    $('#mission_id').append('<option value="' + data.missionId + '" id="' + data.missionId + '">' + data.title + '</option>')
                }
            })
        }
    })
}



function saveDraft() {
    const missionId = $("#mission_id").val();
    const title = $("#title").val();
    const date = $("#date").val();
    const video = $("#videoURL").val();
    var textarea = tinymce.get("storytext").getContent();
    var description = textarea.substring(3, textarea.length - 4);
    const imagePaths = [];
    const images = $('#image-preview');
    const image_tag = images.find("img");
    $('#image-preview img').each(function () {
        console.log(typeof ($(this).attr('src')));
        imagePaths.push($(this).attr('src'));
    })
    if (isValidated()) {
        $.ajax({
            url: "/VolunterrStory/Shareyourstory",
            type: "POST",
            data: {
                missionId: missionId,
                title: title,
                date: date,
                videoURL: video,
                description: description,
                imagePaths: imagePaths,

            },
            success: function (result) {
                window.location = result.redirectUrl;
                //$('#submit').removeClass('bg-secondary');
                //$('#submit').removeAttr('disabled')
                console.log("data drafted")
            }
        })
    }
}
function handleMissionChange() {
    const missionid = document.getElementById("mission_id").value;
    $.ajax({
        url: '/VolunterrStory/loadaddstory',
        type: "GET",
        data: {
            missionid: missionid,
        },
        success: function (result) {
            window.location = result.redirectUrl;
        }
    })
}
function submit(storyid) {

    $.ajax({
        url: "/VolunterrStory/submit",
        type: "POST",
        data: {
            storyid: storyid,
        },
        success: function (result) {
            window.location = result.redirectUrl;

        }
    })
}

function Edit() {
    const missionId = $("#mission_id").val();
    const title = $("#title").val();
    const date = $("#date").val();
    const video = $("#videoURL").val();
    var textarea = tinymce.get("storytext").getContent();
    var description = textarea.substring(3, textarea.length - 4);
    const imagePaths = [];
    const images = $('#image-preview');
    const image_tag = images.find("img");
    $('#image-preview img').each(function () {
        console.log(typeof ($(this).attr('src')));
        imagePaths.push($(this).attr('src'));
    })

    if (isValidated()) {
        console.log("hello");

        $.ajax({
            url: "/VolunterrStory/Edit",
            type: "POST",
            data: {
                missionId: missionId,
                title: title,
                date: date,
                videoURL: video,
                description: description,
                imagePaths: imagePaths,

            },
            success: function (result) {
                window.location = result.redirectUrl;
                //$('#submit').removeClass('bg-secondary');
                //$('#submit').removeAttr('disabled')
                console.log("data edited")
            }
        })
    }
}

function isValidated() {
    console.log("hello");
    const missionId = $("#mission").val();
    const title = $("#title").val();
    const date = $("#date").val();
    const video =$("#videoURL").val();
    var textarea = tinymce.get("storytext").getContent();
    const imagePaths = [];
    validate = true;
    const images = document.getElementById('image-preview');
    const image_tag = images.getElementsByTagName("img");
    for (let i = 0; i < image_tag.length; i++) {
        const image = image_tag[i].getAttribute("src");
        imagePaths.push(image);
    }
    if (missionId == "") {
        validate = false;
        $('#missionValidate').removeClass('d-none');
    } else {
        $('#missionValidate').addClass('d-none');
    }
    if (title == "") {
        validate = false;
        $('#titleValidate').removeClass('d-none');
    } else {
        $('#titleValidate').addClass('d-none');
    }
    if (date == "") {
        validate = false;
        $('#dateValidate').removeClass('d-none');
    } else {
        $('#dateValidate').addClass('d-none');
    }

    const youtubeUrlRegex = /^(https?:\/\/)?(www\.)?(youtube\.com|youtu\.be)\/(watch\?v=)?([a-zA-Z0-9_-]{11})$/;
    if (video.trim() === '') {
        $('#videoValidate').html("Enter Video URL")
        validate = false;
        $('#videoValidate').removeClass('d-none');
    }
    else if (!youtubeUrlRegex.test(video)) {
        $('#videoValidate').html("Enter Valid Video URL")
        validate = false;
        $('#videoValidate').removeClass('d-none');
    } else {
        $('#videoValidate').addClass('d-none');
    }



 
  /*  if (video == "") {
        validate = false;
        $('#videoValidate').removeClass('d-none');
    } else {
        $('#videoValidate').addClass('d-none');
    }*/
    if (textarea == "") {
        validate = false;
        $('#storytextValidate').removeClass('d-none');
    } else {
        $('#storytextValidate').addClass('d-none');
    }
    if (imagePaths.length == 0) {
        validate = false;
        $('#photoValidate').removeClass('d-none').text("Select Images");
    } else if (imagePaths.length > 20) {
        validate = false;
        $('#photoValidate').removeClass('d-none').text("Images must be less than 20");
    }
    else {
        $('#photoValidate').addClass('d-none');
    }
    return validate;
}

$('input').keyup(function () {
    isValidated();
})

$('input').click(function () {
    isValidated();
})

$('input').change(function () {
    isValidated();
})
$('select').change(function () {
    isValidated();
})

$('#storytext').keyup(function () {
    isValidated();
})

$('#storytext').click(function () {
    isValidated();
})
