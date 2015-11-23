using Microsoft.SharePoint;
using SharePoint.Repository.Data.Entities;
using SharePoint.Repository.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Repository.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SPSite site = new SPSite("your site url"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    RepoTestLoad(web);
                    RepoTestAdd(web);

                }
            }


            Console.ReadLine();
        }

        private static void RepoTestLoad(SPWeb web)
        {
            LunchRepository lunchRepo = new LunchRepository(web);
            IList<LunchEntity> lunches = lunchRepo.GetItems();
            foreach (var lunch in lunches)
            {
                Console.WriteLine(lunch.ID);
                Console.WriteLine(lunch.Title);
            }
        }
        private static void RepoTestAdd(SPWeb web)
        {
            LunchRepository lunch = new LunchRepository(web);
            List<LocationEntity> locs = new List<LocationEntity>();
            LocationEntity loce = new LocationEntity { ID = 0, Title = "London", Country = "United Kingdom" };
            locs.Add(loce);
            LunchEntity le = new LunchEntity
            {
                FirstDessert = "Mousse",
                SecondDessert = "Chocolate Fondue",
                Salad = "Caesar",
                Locations = locs

            };
            Console.WriteLine("Result is : " + lunch.Add(le));
        }
    }
}
