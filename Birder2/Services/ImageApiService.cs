using Birder2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface IImageApiHelperService
    {
        bool AreImagesAttachedAsync(int observationId);
        bool UpdateImagesAttachedValue(int observationId, bool newValue);
    }

    public class ImageApiHelperService : IImageApiHelperService
    {
        private readonly ApplicationDbContext _dbContext;

        public ImageApiHelperService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AreImagesAttachedAsync(int observationId)
        {
            bool value = _dbContext.Observations.Where(i => i.ObservationId == observationId)
                                                .Select(y => y.HasPhotos)
                                                .FirstOrDefault();
            return value;
        }

        public bool UpdateImagesAttachedValue(int observationId, bool newValue)
        {
            var observation = _dbContext.Observations.Where(i => i.ObservationId == observationId)
                                                     .FirstOrDefault();
            observation.HasPhotos = newValue;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
