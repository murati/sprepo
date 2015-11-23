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
    public class ListTemplate : ITemplate
    {
        static int itemcount = 0;
        List<string> headers = new List<string>();
        ListItemType templateType;
        public ListTemplate(ListItemType type)
        {
            templateType = type;
        }

        public ListTemplate(List<string> headers)
        {
            templateType = ListItemType.Header;
            this.headers = headers;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            Literal lc = new Literal();
            switch (templateType)
            {
                case ListItemType.Header:
                    lc.Text = "<TABLE border=1><tr>";
                    for (int i = 0; i < headers.Count; i++)
                        lc.Text = string.Format("{0}<th>{1}</th>", lc.Text, headers[i]);
                    lc.Text = string.Format("{0}</tr>", lc.Text);
                    break;
                case ListItemType.Item:
                    lc.Text = "<TR>";
                    lc.DataBinding += new EventHandler(TemplateControl_DataBinding);
                    break;
                case ListItemType.AlternatingItem:
                    lc.Text = "<TR><TD bgcolor=lightblue>Item number: " +
                       itemcount.ToString() + "</TD></TR>";
                    break;
                case ListItemType.Footer:
                    lc.Text = "</TABLE>";
                    break;
            }
            container.Controls.Add(lc);
            itemcount += 1;
        }

        private void TemplateControl_DataBinding(object sender, EventArgs e)
        {
            Literal lc;
            lc = (Literal)sender;
            RepeaterItem container = (RepeaterItem)lc.NamingContainer;
            PropertyInfo[] properties = container.DataItem.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (!properties[i].PropertyType.IsGenericType)
                {
                    lc.Text += "<td>";
                    lc.Text += DataBinder.Eval(container.DataItem, properties[i].Name);
                    lc.Text += "</td>";
                }
            }
            lc.Text += "</tr>";

        }
    }
}
