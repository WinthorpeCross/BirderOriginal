using Birder2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface IApplicationUserAccessor
    {
        Task<ApplicationUser> GetUser();
    }
}
