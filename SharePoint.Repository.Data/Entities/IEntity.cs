using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Repository.Data.Entities
{
    public abstract class IEntity
    {
        /// <summary>
        /// GUID of the ID field. You can refer to InternalName of the desired field as well.
        /// </summary>
        [DisplayName("1d22ea11-1e32-424e-89ab-9fedbadb6ce1")]
        public int ID { get; set; }

        /// <summary>
        /// GUID of the Title field. You can refer to InternalName of the desired field as well.
        /// </summary>
        [DisplayName("fa564e0f-0c70-4ab9-b863-0177e6ddd247")]
        public string Title { get; set; }

    }
}
