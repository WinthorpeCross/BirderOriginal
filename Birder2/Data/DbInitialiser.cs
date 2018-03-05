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
                new ConserverationStatus{ConservationStatus="Red",Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit.",CreationDate=DateTime.Now,LastUpdateDate=DateTime.Now},
                new ConserverationStatus{ConservationStatus="Amber",Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit.",CreationDate=DateTime.Now,LastUpdateDate=DateTime.Now},
                new ConserverationStatus{ConservationStatus="Green",Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit.",CreationDate=DateTime.Now,LastUpdateDate=DateTime.Now},
                new ConserverationStatus{ConservationStatus="Introduced",Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit.",CreationDate=DateTime.Now,LastUpdateDate=DateTime.Now}
            };
            foreach (ConserverationStatus cs in status)
            {
                context.ConservationStatuses.Add(cs);
            }
            context.SaveChanges();
            //
            var statuses = new BritishStatus[]
            {
                new BritishStatus{ BirderStatusInBritain="Common",CreationDate=DateTime.Now,LastUpdateDate=DateTime.Now },
                   new BritishStatus{ BirderStatusInBritain="Uncommon",CreationDate=DateTime.Now,LastUpdateDate=DateTime.Now }
            };
            foreach (BritishStatus bs in statuses)
            {
                context.BritishStatuses.Add(bs);
            }
            context.SaveChanges();
            //
            var birds = new Bird[]
            {
                new Bird { Class="Aves",Order="Accipitriformes",Family="Accipitridae",Genus="Accipiter",Species="Accipiter gentilis"
                    ,EnglishName="Goshawk",InternationalName="Northern Goshawk",Category="A",PopulationSize="100-1,000 pairs",BtoStatusInBritain="Winter Visitor"
                    ,ConserverationStatusId=1,BritishStatusId=1
                    ,ThumbnailUrl="http://farm3.staticflickr.com/2818/9312384651_76a7b6ff84_s.jpg"},

                new Bird { Class="Aves",Order="Passeriformes",Family="Fringillidae",Genus="Acanthis",Species="Acanthis hornemanni"
                    ,EnglishName="Artic Redpoll",InternationalName=null,Category="A",PopulationSize="100-1,000 records",BtoStatusInBritain="Winter Visitor"
                    ,ConserverationStatusId=1,BritishStatusId=1
                    ,ThumbnailUrl="http://farm8.staticflickr.com/7263/13667553233_5bd7837a4f_s.jpg"},

                new Bird { Class="Aves",Order="Charadriiformes",Family="Scolopacidae",Genus="Scolopax",Species="Scolopax rusticola"
                    ,EnglishName="Woodcock",InternationalName="Eurasian Woodcock",Category="A",PopulationSize="10-100,000 pairs",BtoStatusInBritain="Winter Visitor"
                    ,ConserverationStatusId=1,BritishStatusId=1
                    ,ThumbnailUrl="http://farm6.staticflickr.com/5288/5290609250_7ea5df20fc_s.jpg"}

            };
            foreach (Bird b in birds)
            {
                context.Birds.Add(b);
            }

            var orders = new SalesOrder[]
            {
                new SalesOrder
                {
                    CustomerName = "Adam",
                    PONumber = "9876",
                    SalesOrderItems =
                    {
                                    new SalesOrderItem{ProductCode = "ABC", Quantity = 10, UnitPrice = 1.23m },
                                    new SalesOrderItem{ProductCode = "XYZ", Quantity = 7, UnitPrice = 14.57m },
                                    new SalesOrderItem{ProductCode = "SAMPLE", Quantity = 3, UnitPrice = 15.00m }
                    }
                },
                new SalesOrder { CustomerName = "Michael" },
                new SalesOrder { CustomerName = "David", PONumber = "Acme 9" }
            };

            foreach (SalesOrder so in orders)
            {
                context.SalesOrders.Add(so);
            }

            context.SaveChanges();
        }
    }
}
