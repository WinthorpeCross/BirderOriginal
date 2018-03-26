using Birder2.Data;
using Birder2.Models;
using Birder2.Services;
using FlickrNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System;

namespace Birder2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IFlickrService _flickrService;
        private readonly IUserRepository _userRepository;
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public HomeController(IUserRepository userRepository,
                                UserManager<ApplicationUser> userManager,
                                    IFlickrService flickrService,
                                        IApplicationUserAccessor userAccessor,
                                            IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _flickrService = flickrService;
            _userAccessor = userAccessor;
            _emailSender = emailSender;
            //blobUtility = new HomeController.BlobUtility(_optionsAccessor.Value.StorageAccountNameOption, _optionsAccessor.Value.StorageAccountKeyOption);
        }

        //public BlobUtility(string accountName, string accountKey)
        //{
        //    CloudStorageAccount storageAccount = 
        //}

        public IActionResult Index() //async Task<>
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            PhotoCollection photos = _flickrService.GetFlickrPhotoCollection("Cyanistes caeruleus");
            return View(photos);
        }
 
        [AllowAnonymous]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Map example";
            return View();
        }

        [AllowAnonymous]
        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
