using System;
using System.Collections.Generic;
using System.Linq;

namespace HierarchicalData
{
    [Serializable()]
    public class XmlItem //public for xml serializator
    {
        public XmlItem(object key, object value)
        {
            Key = key;
            Value = value;
        }

        public XmlItem()
        {
            Key = "";
            Value = "";
        }

        public object Key { get; set; }
        public object Value { get; set; }
    }

    [Serializable()]
    public class XmlItems : List<XmlItem> {
        public List<T> ConvertToList<T>()
        {
            return this.Select(x => (T)x.Value).ToList();
        }
    }

    public static class Extensions
    {
        public static HierarchicalObject ToHierarchicalObject<T>(this IEnumerable<T> baseEnumerable)
        {
            var hobject = new HierarchicalObject();
            for (var i = 0; i < baseEnumerable.Count(); i++)
                hobject[i] = baseEnumerable.ElementAt(i);
            return hobject;
        }
    }
}
