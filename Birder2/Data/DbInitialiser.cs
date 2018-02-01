using Birder2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

            byte[] birdIcon = File.ReadAllBytes(@"C:\Users\rcros\Desktop\if_099374-twitter-bird3_56010.png"); // artywear_yellow_1046.jpg");

            var status = new ConserverationStatus[]
            {
                new ConserverationStatus{ConservationStatus="Red"},
                new ConserverationStatus{ConservationStatus="Amber"},
                new ConserverationStatus{ConservationStatus="Green"},
                new ConserverationStatus{ConservationStatus="Introduced"}
            };
            foreach (ConserverationStatus cs in status)
            {
                context.ConservationStatuses.Add(cs);
            }
            context.SaveChanges();

            //var birds = new Bird[]
            //{
            //    new Bird{ Class="Aves",Order="Accipitriformes",Family="Accipitridae",Genus="Accipiter",Species="Accipiter gentilis",EnglishName="Goshawk",InternationalName="Northern Goshawk",Category="A",PopulationSize="100-1,000 pairs",Image=birdIcon,ConserverationStatusId=3}
            //};
            //foreach (Bird b in birds)
            //{
            //    context.Birds.Add(b);
            //}
            //context.SaveChanges();
        }
    }
}
