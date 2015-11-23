using CamlBuilder;
using Microsoft.SharePoint;
using SharePoint.Repository.Data.Entities;
using SharePoint.Repository.Data.SharePointOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Repository.Data.Repositories
{
    public abstract class IRepository<T> : IEntity where T : new()
    {
        string WebUrl { get; set; }
        private SPList list;
        private SPWeb web;
        ListItemFieldMapper<T> mapper = new ListItemFieldMapper<T>();
        public IRepository(SPWeb web, string listName)
        {
            // TODO: Complete member initialization
            this.web = web;
            this.list = web.Lists.TryGetList(listName);
            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                mapper.AddMapping(GetFieldName(properties[i]), properties[i].Name);
            }
        }
        internal List<T> GetAllItems()
        {
            var caml = CamlLogicalJoin.And(CamlOperator.IsNotNull("ID"));
            SPListItemCollection items = list.GetItems(new SPQuery { Query = CamlQuery.BuildQuery(caml).GetWhereClause() });
            List<T> entityList = new List<T>();
            for (int i = 0; i < items.Count; i++)
            {
                T customer = mapper.CreateEntity(items[i]);
                entityList.Add(customer);
            }
            return entityList;
        }

        internal int Add(T entity, int id)
        {
            SPListItem item = null;
            if (id > 0)
                item = list.GetItemById(id);
            else
                item = list.Items.Add();
            mapper.FillSPListItemFromEntity(item, entity);
            item.Update();
            return item.ID;
        }

        private Guid GetFieldName(PropertyInfo prop)
        {
            IList<CustomAttributeData> attribs = prop.GetCustomAttributesData();
            for (int i = 0; i < attribs.Count; i++)
            {
                string typeName = attribs[i].Constructor.DeclaringType.Name;
                if (attribs[i].ConstructorArguments.Count >= 1 && (typeName == "DisplayName" || typeName == "DisplayNameAttribute"))
                {
                    return Guid.Parse(attribs[i].ConstructorArguments[0].Value.ToString());
                }
            }
            return Guid.Empty;
        }

        internal T GetItemById(int id)
        {
            SPListItem item = list.GetItemById(id);
            return mapper.CreateEntity(item);
        }
    }
}
