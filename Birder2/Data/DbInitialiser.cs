using Birder2.Models;
using System;
using System.IO;
using System.Linq;

namespace Birder2.Data
{
    public static class DbInitialiser
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Birds.Any())
            {
                return;
            }

            var status = new ConserverationStatus[]
            {
                new ConserverationStatus{ConservationStatus="Red list",Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit.",CreationDate=DateTime.Now,LastUpdateDate=DateTime.Now},
                new ConserverationStatus{ConservationStatus="Amber list",Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit.",CreationDate=DateTime.Now,LastUpdateDate=DateTime.Now},
                new ConserverationStatus{ConservationStatus="Green list",Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit.",CreationDate=DateTime.Now,LastUpdateDate=DateTime.Now},
                new ConserverationStatus{ConservationStatus="Former breeder",Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit.",CreationDate=DateTime.Now,LastUpdateDate=DateTime.Now},
                new ConserverationStatus{ConservationStatus="Not assessed",Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit.",CreationDate=DateTime.Now,LastUpdateDate=DateTime.Now}
            };
            foreach (ConserverationStatus cs in status)
            {
                context.ConservationStatuses.Add(cs);
            }
            context.SaveChanges();

            //var birds = new Bird[]
            //{
            //    new Bird { Class="Aves",Order="Accipitriformes",Family="Accipitridae",Genus="Accipiter",Species="Accipiter gentilis"
            //        ,EnglishName="Goshawk",InternationalName="Northern Goshawk",Category="A",PopulationSize="100-1,000 pairs",BtoStatusInBritain="Winter Visitor"
            //        ,ConserverationStatusId=1,BirderStatus=BirderStatus.Uncommon
            //        ,ThumbnailUrl="http://farm3.staticflickr.com/2818/9312384651_76a7b6ff84_s.jpg"
            //        ,SongUrl="http://www.xeno-canto.org/sounds/uploaded/RFGQDPLDEC/XC405653-H%C3%B8nsehauk%20XC%20Ringsaker%20-male-female%20Elias%20A.%20Ryberg20180312132725_026.mp3"},
            //};

            //foreach (Bird b in birds)
            //{
            //    context.Birds.Add(b);
            //}
            //context.SaveChanges();
        }
    }
}
