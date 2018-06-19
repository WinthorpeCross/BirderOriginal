
var gallery;


document.onload = fetchImageLinks(model);

/* Dropzone */
Dropzone.options.imageUpload = { // "imageUpload" is the camelized version of the HTML element's ID
    paramName: ("files"), // The name that will be used to transfer the file.  Must be the same as the api parameter
    dictDefaultMessage: "Drop photographs here or Click to Upload",
    addRemoveLinks: true, // Allows for cancellation of file upload and remove thumbnail
    init: function () {
        myDropzone = this;
        myDropzone.on("success", function (file, response) {
            myDropzone.removeFile(file); //presumably removes the file from the dropzone area
            fetchImageLinks(response);
        });
    }
};

// Grab links for images from backend api
function fetchImageLinks(data) {
    // Fetch images
    //alert(data);
    //app.get('/myservice/:CustomerId', myservice.queryByCustomer);
    $.get("/api/Images/thumbnails?containerName=" + data, function (fetchedImageLinks) {
        console.log(fetchedImageLinks)

        // Check if anything is in there
        if (_.isEmpty(fetchedImageLinks)) {
            console.log('empty fetched')
            // do nothing
        } else {
            // Check if we have a gallery initialized
            if (_.isEmpty(gallery)) {
                // initialize gallery
                gallery = blueimp.Gallery(
                    fetchedImageLinks, // gallery links array
                    {
                        container: '#blueimp-gallery-carousel',
                        carousel: true
                    } // gallery options
                );
            } else {
                // check if images are equal to array
                console.log('currently in gallery:')
                console.log(gallery.list)
                var imageLinksEqual = _.isEqual(_.sortBy(gallery.list.map(s => s.split("?")[0])), _.sortBy(fetchedImageLinks.map(s => s.split("?")[0])))
                if (imageLinksEqual) {
                    console.log('images arr are equal')
                    // do nothing
                } else {
                    console.log('images arr are not equal')

                    // update gallery with new image urls. Only compare actual url without SAS token query string
                    var newImageLinks = _.difference(fetchedImageLinks.map(s => s.split("?")[0]), gallery.list.map(s => s.split("?")[0]))

                    console.log('differene is: ')
                    console.log(newImageLinks)
                    // Only add new images
                    gallery.add(newImageLinks);

                    // Force image load
                    gallery.next();
                }
            }
        }
    });
}

// Start first interval
//fetchImageLinks()

//setInterval(function () {
//    fetchImageLinks()
//}, 5000)
