using Birder2.Services;
using ImageResizeWebApp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Birder2.Controllers
{
    [Route("api/[controller]")]
    //[ApiController] 
    public class ImagesApiController : ControllerBase
    {
        private readonly IImageApiHelperService _imageApiHelperService;
        private readonly IConfiguration _config;

        public ImagesApiController(IImageApiHelperService imageApiHelperService,
                                    IConfiguration config)
        {
            _imageApiHelperService = imageApiHelperService;
            _config = config;
        }

        // POST /api/images/upload
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromForm]ICollection<IFormFile> files, int observationId)
        {
            bool isUploaded = false;

            try
            {
                if (files.Count == 0)

                    return BadRequest("No files received from the upload");

                if (_config["BlobStorageKey"] == string.Empty || _config["BlobStorage:Account"] == string.Empty)

                    return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                if (observationId == 0)

                    return BadRequest("No observationId is supplied");

                foreach (var formFile in files)
                {
                    if (StorageHelper.IsImage(formFile))
                    {
                        if (formFile.Length > 0)
                        {
                            using (Stream stream = formFile.OpenReadStream())
                            {
                                isUploaded = await StorageHelper.UploadFileToStorage(stream, observationId.ToString(), formFile.FileName, _config["BlobStorageKey"], _config["BlobStorage:Account"]);
                            }
                        }
                    }
                    else
                    {
                        return new UnsupportedMediaTypeResult();
                    }
                }

                if (isUploaded)
                {
                    _imageApiHelperService.UpdateImagesAttachedValue(observationId, true);
                    //if ("test" != string.Empty)

                    return new AcceptedAtActionResult("GetThumbNails", "Images", null , observationId.ToString());

                    //else
                    
                    //    return new AcceptedResult();
                }
                else

                    return BadRequest("Look like the image couldnt upload to the storage");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET /api/images/thumbnails
        [HttpGet("thumbnails")]
        public async Task<IActionResult> GetThumbNails(int observationId)
        {
            var areImagesAvailable = _imageApiHelperService.AreImagesAttachedAsync(observationId);
            if (areImagesAvailable != true)
            {
                return Accepted("Observation status indicates images not available");
            }

            try
            {
                //if (_config["BlobStorageKey"] || _config["BlobStorage:Account"] == string.Empty)

                //    return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                if (observationId == 0)

                    return BadRequest("No observationId is supplied");

                List<string> thumbnailUrls = await StorageHelper.GetThumbNailUrls(observationId.ToString(), _config["BlobStorageKey"], _config["BlobStorage:Account"]);

                if(thumbnailUrls.Count == 0)
                {
                    _imageApiHelperService.UpdateImagesAttachedValue(observationId, false);
                }

                return new ObjectResult(thumbnailUrls);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

/*
        // GET: api/Images
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Images/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Images
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Images/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
*/