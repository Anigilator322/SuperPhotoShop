using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPhotoShop.Models
{
    public class ParametrsRequirments
    {
        public Dictionary<string, object> Parametrs = new Dictionary<string, object>();
        
        #region InputDescription
        public List<string> FieldsLabels { get; private set; } = new List<string>();
        #endregion

        public ParametrsRequirments(List<string> fieldsLabels)
        {
            
            FieldsLabels = fieldsLabels;
            foreach (var field in FieldsLabels)
            {
                Parametrs.Add(field, null);
            }
        }


        
    }
}
