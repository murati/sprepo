using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Repository.Data.Entities
{
    public class LocationEntity : EntityBase
    {
        /// <summary>
        /// GUID of the fields given below. You can refer to InternalName of the desired field as well. 
        /// If you won't publish field definitions using feature, DO NOT use guid.
        /// </summary>
        [DisplayName("1ff0256f-0fd2-4e19-ac67-6ea009625e93")]
        public string Country { get; set; }
    }
}
