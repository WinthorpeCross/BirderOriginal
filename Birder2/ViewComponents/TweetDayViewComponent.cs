using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.ViewComponents
{
    public class TweetDayViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var bir = await _analysisRepository.BirdCount();
            return View();
        }
    }
}
