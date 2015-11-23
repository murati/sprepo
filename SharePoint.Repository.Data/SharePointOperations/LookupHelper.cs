using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Repository.Data.SharePointOperations
{
    internal class LookupHelper
    {
        private static Dictionary<int, string> GetLookupValues(string listItem)
        {
            Dictionary<int, string> lookupValues = new Dictionary<int, string>();
            if (!string.IsNullOrEmpty(listItem))
            {
                List<string> values = listItem.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                for (int i = 0; i < values.Count; i += 2)
                {
                    lookupValues.Add(int.Parse(values[i]), values[i + 1]);
                }
            }
            return lookupValues;
        }

        private static List<int> GetLookupIDs(string listItem)
        {
            List<int> lookupIds = new List<int>();
            if (!string.IsNullOrEmpty(listItem))
            {
                List<string> values = listItem.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                for (int i = 0; i < values.Count; i += 2)
                {
                    lookupIds.Add(int.Parse(values[i]));
                }
            }
            return lookupIds;
        }

        private static string GetLookupListName(PropertyInfo prop)
        {
            string typeName = string.Empty;
            IList<CustomAttributeData> attribs = prop.GetCustomAttributesData();
            for (int i = 0; i < attribs.Count; i++)
            {
                typeName = attribs[i].Constructor.DeclaringType.Name;
                if (attribs[i].ConstructorArguments.Count >= 1 && (typeName == "Description" || typeName == "DescriptionAttribute"))
                {
                    return attribs[i].ConstructorArguments[0].Value.ToString();
                }
            }
            return null;
        }

        private static Guid GetFieldID(PropertyInfo prop)
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

        public static List<T> CreateEntityListFromLookup<T>(SPList spList, string listItem) where T : new()
        {
            ListItemFieldMapper<T> mapper = new ListItemFieldMapper<T>();
            List<int> lookupIds = GetLookupIDs(listItem);
            List<T> entityList = new List<T>();
            if (lookupIds.Count > 0)
            {
                SPListItem lkpItem = null;
                PropertyInfo[] properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                    mapper.AddMapping(GetFieldID(properties[i]), properties[i].Name);
                for (int i = 0; i < lookupIds.Count; i++)
                {
                    lkpItem = spList.GetItemById(lookupIds[i]);
                    T entity = mapper.CreateEntity(lkpItem);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public static T CreateEntityFromLookup<T>(SPList spList, string listItem) where T : new()
        {
            int lookupId = GetLookupIDs(listItem).FirstOrDefault();
            T entity = new T();
            if (lookupId != null)
            {
                SPListItem item = null;
                ListItemFieldMapper<T> mapper = new ListItemFieldMapper<T>();
                PropertyInfo[] properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                    mapper.AddMapping(GetFieldID(properties[i]), properties[i].Name);
                item = spList.GetItemById(lookupId);
                entity = mapper.CreateEntity(item);
            }
            return entity;
        }

        public static object FillLookupList<T>(SPList spList, object values) where T : new()
        {
            List<T> entities = values as List<T>;
            SPFieldLookupValueCollection lookups = new SPFieldLookupValueCollection();
            for (int i = 0; i < entities.Count; i++)
            {
                lookups.Add(new SPFieldLookupValue(FillLookup<T>(spList, entities[i])));
            }
            return lookups;
        }

        public static string FillLookup<T>(SPList spList, object value) where T : new()
        {
            ListItemFieldMapper<T> mapper = new ListItemFieldMapper<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length; i++)
                mapper.AddMapping(GetFieldID(properties[i]), properties[i].Name);
            T entity = (T)value;
            int lookupId = 0;
            PropertyInfo propertyInfo = null;
            SPListItem newItem = null;
            propertyInfo = typeof(T).GetProperty("ID");
            lookupId = Convert.ToInt32(propertyInfo.GetValue(entity, null));
            if (lookupId > 0)
                newItem = spList.Items.GetItemById(lookupId);
            else
                newItem = spList.Items.Add();
            mapper.FillSPListItemFromEntity(newItem, entity);
            newItem.Update();
            propertyInfo = typeof(T).GetProperty("Title");
            string title = propertyInfo.GetValue(entity, null).ToString();
            return string.Format("{0};#{1}", newItem.ID, title);
        }

        public static object GetLookupData(SPListItem item, PropertyInfo propertyInfo, string fieldValue, string methodName)
        {
            Type lookupType = propertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();
            MethodInfo genericMethod = typeof(LookupHelper).GetMethod(methodName);
            object list = null;
            if (genericMethod != null)
            {
                string listName = LookupHelper.GetLookupListName(propertyInfo);
                SPList spList = item.Web.Lists.TryGetList(listName);
                if (spList != null)
                {
                    genericMethod = genericMethod.MakeGenericMethod(lookupType);
                    list = genericMethod.Invoke(lookupType, new object[] { spList, fieldValue });
                }
                return list;

            }
            else
                return null;
        }

        public static object SetLookupData(SPListItem item, PropertyInfo propertyInfo, object fieldValue, string methodName)
        {
            Type lookupType = propertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();
            MethodInfo genericMethod = typeof(LookupHelper).GetMethod(methodName);
            object list = null;
            if (genericMethod != null)
            {
                string listName = LookupHelper.GetLookupListName(propertyInfo);
                SPList spList = item.Web.Lists.TryGetList(listName);
                if (spList != null)
                {
                    genericMethod = genericMethod.MakeGenericMethod(lookupType);
                    list = genericMethod.Invoke(lookupType, new object[] { spList, fieldValue });
                }
            }
            return list;
        }

    }
}
