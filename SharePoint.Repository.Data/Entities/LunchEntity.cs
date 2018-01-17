using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Repository.Data.Entities
{
    public class LunchEntity : EntityBase
    {
        /// <summary>
        /// GUID of the fields given below. You can refer to InternalName of the desired field as well. 
        /// If you won't publish field definitions using feature, DO NOT use guid.
        /// </summary>
        [DisplayName("6316AAFD-9ECA-4643-9F81-D68DAE9AD6B4")]
        public string FirstLunch { get; set; }
        [DisplayName("210B170A-8BC3-47BF-84EE-9CE7017B7694")]
        public string SecondLunch { get; set; }
        [DisplayName("358D1F53-BFF4-4892-9C1B-61F34C4BBB11")]
        public string FirstDessert { get; set; }
        [DisplayName("3E9E5118-3341-48A1-91DF-E99CB21021BA")]
        public string SecondDessert { get; set; }
        [DisplayName("3D353816-4BA5-4CEB-AF37-7D9CF76A1AC8")]
        public string Salad { get; set; }

        [DisplayName("ffa59ac6-b4c1-4309-9d0b-77af240cee8d"), Description("LocationList")]
        public List<LocationEntity> Locations { get; set; }


    }
}
