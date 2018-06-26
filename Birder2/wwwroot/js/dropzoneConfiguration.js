/* Dropzone */
Dropzone.options.imageUpload = { // "imageUpload" is the camelized version of the HTML element's ID
    paramName: ("files"), // The name that will be used to transfer the file.  Must be the same as the api parameter
    dictDefaultMessage: "Drop photographs here or Click here to upload...",
    addRemoveLinks: true, // Allows for cancellation of file upload and remove thumbnail
    init: function () {
        myDropzone = this;
        myDropzone.on("success", function (file, response) {
            myDropzone.removeFile(file); //presumably removes the file from the dropzone area
            fetchImageLinks(response);
        });
    }
};