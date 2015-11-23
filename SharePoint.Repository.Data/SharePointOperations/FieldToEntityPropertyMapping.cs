using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePoint.Repository.Data.SharePointOperations
{
    public class FieldToEntityPropertyMapping
    {
        /// <summary>
        /// The guid that corresponds to the Id of the field.
        /// </summary>
        public Guid ListFieldId { get; set; }

        /// <summary>
        /// The name of the property on the entity. 
        /// </summary>
        public string EntityPropertyName { get; set; }
    }
}
