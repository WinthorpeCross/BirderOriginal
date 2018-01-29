using Birder2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface IBirdRepository
    {
        Task<IEnumerable<Bird>> AllBirdsList();
        Task<IEnumerable<Bird>> FilteredBirdsList(string searchString);
        Task<Bird> GetBirdDetails(int? id);
    }
}
