using Birder2.Data;
using Birder2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.ViewComponents
{
    [Authorize]
    public class BirdCountViewComponent : ViewComponent
    {
        private readonly IAnalysisRepository _analysisRepository;

        public BirdCountViewComponent(IAnalysisRepository analysisRepository)
        {
            _analysisRepository = analysisRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bir = await _analysisRepository.BirdCount();
            return View("Default", bir.ToString());
        }

        //private Task<List<TodoItem>> GetItemsAsync(int maxPriority, bool isDone)
        //{
        //    return db.ToDo.Where(x => x.IsDone == isDone &&
        //                         x.Priority <= maxPriority).ToListAsync();
        //}
    }
}
