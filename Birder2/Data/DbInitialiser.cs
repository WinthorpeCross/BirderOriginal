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

            //byte[] birdIcon = File.ReadAllBytes(@"C:\Users\rcros\Desktop\if_099374-twitter-bird3_56010.png"); // artywear_yellow_1046.jpg");

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

            //    new Bird { Class="Aves",Order="Passeriformes",Family="Fringillidae",Genus="Acanthis",Species="Acanthis hornemanni"
            //        ,EnglishName="Arctic Redpoll",InternationalName=null,Category="A",PopulationSize="100-1,000 records",BtoStatusInBritain="Winter Visitor"
            //        ,ConserverationStatusId=2,BirderStatus=BirderStatus.Common
            //        ,ThumbnailUrl="http://farm8.staticflickr.com/7263/13667553233_5bd7837a4f_s.jpg"
            //        ,SongUrl="http://www.xeno-canto.org/sounds/uploaded/OOECIWCSWV/XC322840-LS_52078%20Hoary%20Redpoll%20call%20A.mp3"},

            //    new Bird { Class="Aves",Order="Charadriiformes",Family="Scolopacidae",Genus="Scolopax",Species="Scolopax rusticola"
            //        ,EnglishName="Woodcock",InternationalName="Eurasian Woodcock",Category="A",PopulationSize="10-100,000 pairs",BtoStatusInBritain="Winter Visitor"
            //        ,ConserverationStatusId=3,BirderStatus=BirderStatus.Uncommon
            //        ,ThumbnailUrl="http://farm6.staticflickr.com/5288/5290609250_7ea5df20fc_s.jpg"
            //        ,SongUrl="http://www.xeno-canto.org/sounds/uploaded/WIPMNWUHXI/XC394860-Hs_LumandaEst_170526_2304.mp3"},
            //};
            //foreach (Bird b in birds)
            //{
            //    context.Birds.Add(b);
            //}
            //context.SaveChanges();
        }
    }
}
