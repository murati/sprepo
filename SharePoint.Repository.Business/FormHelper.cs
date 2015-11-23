using Microsoft.SharePoint;
using SharePoint.Repository.Data.Entities;
using SharePoint.Repository.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SharePoint.Repository.Business
{
    public class FormHelper : IDisposable
    {
        private bool disposed = false;
        private SPWeb web;
        public FormHelper(SPWeb web)
        {
            this.web = web;
        }

        public bool SaveLunch(ControlCollection controls, params Dictionary<int, string>[] lookups)
        {
            bool result = false;
            LunchRepository repo = new LunchRepository(web);
            LunchEntity entity = ParseForm<LunchEntity>(controls);
            //entity.Locations = lookups.FirstOrDefault().Count > 0 ? lookups.FirstOrDefault() : null;
            //entity.Users = null;
            repo.Add(entity);
            return result;
        }

        public void SaveLocations(ControlCollection controls, SPWeb web)
        {
            LocationRepository repo = new LocationRepository(web);
            LocationEntity entity = ParseForm<LocationEntity>(controls);
            repo.Add(entity);
        }

        public DataResponseResult GetLunches()
        {
            LunchRepository repo = new LunchRepository(web);
            IList<LunchEntity> entity = repo.GetItems();//.FirstOrDefault();
            DataResponseResult result = new DataResponseResult { Entity = entity.ToList(), Type = entity.GetType().GetGenericArguments().FirstOrDefault().FullName };
            PropertyInfo[] props = entity.GetType().GetGenericArguments().FirstOrDefault().GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                if (!props[i].PropertyType.IsGenericType)
                    result.Headers.Add(props[i].Name);
            }

            return result;

        }

        public object GetLunch()
        {
            LunchRepository repo = new LunchRepository(web);
            return repo.GetItems();//.FirstOrDefault();
        }

        public ControlCollection GetLunches(ControlCollection parent)
        {
            LunchRepository repo = new LunchRepository(web);
            LunchEntity entity = repo.GetItems().FirstOrDefault();
            TextBox tb;
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                if (!prop.GetType().IsGenericType)
                {
                    tb = new TextBox();
                    tb.Text = prop.GetValue(entity, null).ToString();
                    parent.Add(tb);
                }
            }
            return parent;
        }

        private TEntity ParseForm<TEntity>(ControlCollection controls) where TEntity : new()
        {
            TEntity entity = new TEntity();
            Guid result = Guid.Empty;
            Type entityType = typeof(TEntity);
            foreach (Control control in controls)
            {
                foreach (PropertyInfo prop in entityType.GetProperties())
                {
                    if (control.ID != null && control.ID.Equals(prop.Name))
                        prop.SetValue(entity, ((TextBox)control).Text);
                }
            }
            return entity;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (web != null)
                    web.Dispose();
            }
            disposed = true;
        }


    }
}
