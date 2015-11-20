using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApplicationUserSettings
{
    [Serializable()]
    [XmlRoot("xml")]
    public class XmlParameters
    {
        [XmlArray("Items")]
        [XmlArrayItem("Item", typeof(XmlItem))]
        public List<XmlItem> Items
        { get; set; }
    }
}
