const dropZone = document.getElementById("drop-zone");
const fileInput = document.getElementById("file-input");
const imagePreview = document.getElementById("image-preview");
const uploadedFiles = new Set();

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