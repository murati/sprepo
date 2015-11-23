using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using SharePoint.Repository.Business;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace SharePoint.Repository.Web.Layouts.SharePoint.Repository.Web
{
    public partial class RepoTest : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (FormHelper helper = new FormHelper(SPContext.Current.Web))
            {
                lv.DataSource = helper.GetLunch();
                lv.DataBind();
                DataResponseResult result = helper.GetLunches();
                rp.HeaderTemplate = new ListTemplate(result.Headers);
                rp.ItemTemplate = new ListTemplate(System.Web.UI.WebControls.ListItemType.Item);
                rp.FooterTemplate = new ListTemplate(System.Web.UI.WebControls.ListItemType.Footer);
                rp.DataSource = result.Entity;
                rp.DataBind();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> locationList = new JavaScriptSerializer().Deserialize<List<string>>(hdnLocations.Value).FindAll(p => !string.IsNullOrEmpty(p));
            Dictionary<int, string> locations = new Dictionary<int, string>();
            for (int i = 0; i < locationList.Count; i++)
                locations.Add(0, locationList[i]);
            using (FormHelper helper = new FormHelper(SPContext.Current.Web))
            {
                helper.SaveLunch(panel.Controls, locations, null);
            }
        }
    }
}
