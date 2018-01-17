using Microsoft.SharePoint;
using SharePoint.Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Repository.Data.Repositories
{
    public class LunchRepository : RepositoryBase<LunchEntity>
    {
        public const string ListName = "LunchList";

        public string ContentTypeName
        {
            get { return "LunchCT"; }
        }
        public int Add(LunchEntity le)
        {
            return base.Add(le, le.ID);
        }
        public LunchEntity Get(int id)
        {
            return base.GetItemById(id);
        }
        public IList<LunchEntity> GetItems()
        {
            return base.GetAllItems();
        }

        public LunchRepository(SPWeb web)
            : base(web, ListName)
        {

        }

        public string WebUrl { get; set; }
    }
}
