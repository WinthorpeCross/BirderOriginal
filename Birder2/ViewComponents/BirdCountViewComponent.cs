using Birder2.Data;
using Birder2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.ViewComponents
{
    public class BirdCountViewComponent : ViewComponent
    {
        private readonly ISideBarRepository _sideBarRepository;

        public BirdCountViewComponent(ISideBarRepository sideBarRepository)
        {
            _sideBarRepository = sideBarRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bir = await _sideBarRepository.BirdCount();
            return View("Default", bir.ToString());
        }

        //private Task<List<TodoItem>> GetItemsAsync(int maxPriority, bool isDone)
        //{
        //    return db.ToDo.Where(x => x.IsDone == isDone &&
        //                         x.Priority <= maxPriority).ToListAsync();
        //}
    }
}
