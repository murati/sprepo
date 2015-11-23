using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Repository.Business
{
    public class DataResponseResult
    {
        public List<string> Headers = new List<string>();
        public dynamic Entity { get; set; }
        public string Type { get; set; }
    }
}
