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

            var status = new BirdConserverationStatus[]
            {
                new BirdConserverationStatus{ConservationStatus="Red", Note=null},
                new BirdConserverationStatus{ConservationStatus="Amber", Note=null},
                new BirdConserverationStatus{ConservationStatus="Green", Note=null},
                new BirdConserverationStatus{ConservationStatus="Introduced", Note=null}
            };
            foreach (BirdConserverationStatus bcs in status)
            {
                context.BirdConservationStatuses.Add(bcs);
            }
            context.SaveChanges();

            var birds = new Bird[]
            {
            new Bird{EnglishName="Dipper",InternationalName="White-throated Dipper",ScientificName="Cinclus cinclus",Image=birdIcon,BirdConserverationStatusId=3},
            new Bird{EnglishName="Kestrel",InternationalName="Common Kestrel",ScientificName="Falco tinnunculus",Image=birdIcon,BirdConserverationStatusId=2},
            new Bird{EnglishName="Merlin",InternationalName=null,ScientificName="Falco columbarius",Image=birdIcon,BirdConserverationStatusId=2},
            new Bird{EnglishName="Hoopoe",InternationalName="Eurasian Hoopoe",ScientificName="Upupa epops",Image=birdIcon,BirdConserverationStatusId=3},
            new Bird{EnglishName="Lesser Black-backed Gull",InternationalName=null,ScientificName="Larus fuscus",Image=birdIcon,BirdConserverationStatusId=3}
            };
            foreach (Bird b in birds)
            {
                context.Birds.Add(b);
            }
            context.SaveChanges();
        }
    }
}
