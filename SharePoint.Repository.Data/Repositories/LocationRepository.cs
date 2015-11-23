using Microsoft.SharePoint;
using SharePoint.Repository.Data.Entities;
using SharePoint.Repository.Data.SharePointOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Repository.Data.Repositories
{
    public class LocationRepository : IRepository<LocationEntity>
    {
        ListItemFieldMapper<LocationEntity> mapper = new ListItemFieldMapper<LocationEntity>();
        public const string ListName = "LocationList";

        public string ContentTypeName
        {
            get { return "Location"; }
        }
        public int Add(LocationEntity le)
        {
            return base.Add(le, le.ID);
        }
        public LocationEntity Get(int id)
        {
            LocationEntity customer = base.GetItemById(id);
            return customer;
        }
        public IList<LocationEntity> GetAll()
        {
            return base.GetAllItems();
        }

        public LocationRepository(SPWeb web)
            : base(web, ListName)
        {
        }

        public string WebUrl { get; set; }
    }
}
