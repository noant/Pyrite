using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUserSettings
{
    [Serializable()]
    public class XmlItem
    {
        public XmlItem(string parName, string value)
        {
            ParameterName = parName;
            Value = value;
        }
        public XmlItem()
        {
            ParameterName = "";
            Value = "";
        }
        public string ParameterName { get; set; }
        public string Value { get; set; }
    }
}
