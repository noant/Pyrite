using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HierarchicalData
{
    public class HierarchicalObject
    {
        public static class Defaults
        {
            public static readonly string FileName = "settings.xml";
        }

        public void SaveToFile()
        {
            try
            {
                File.WriteAllText(FileName, this.GetXml());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveToStream()
        {
            try
            {
                var xml = this.GetXml();
                Stream.Write(CurrentXmlEncoding.GetBytes(xml), 0, CurrentXmlEncoding.GetByteCount(xml));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string FileName { get; set; }

        public Stream Stream { get; set; }

        public HierarchicalObject(string fileName)
        {
            this.FileName = fileName;
        }

        public HierarchicalObject() : this(Defaults.FileName) { }

        internal static Encoding CurrentXmlEncoding = Encoding.Default;

        private Dictionary<dynamic, dynamic> _data = new Dictionary<dynamic, dynamic>();

        public bool ThrowsExceptionIfParameterNotExist { get; set; }

        public dynamic this[dynamic key]
        {
            get
            {
                if (ThrowsExceptionIfParameterNotExist && !_data.ContainsKey(key))
                    throw new ParameterNotExistException("Параметр '" + key.ToString() + "' не существует");

                if (!_data.ContainsKey(key) || _data[key] == null)
                    _data.Add(key, new HierarchicalObject());

                return _data[key];
            }
            set
            {
                if (!_data.ContainsKey(key))
                    _data.Add(key, null);

                _data[key] = value;
            }
        }

        public bool ContainsKey(dynamic key)
        {
            return _data.ContainsKey(key);
        }

        public dynamic[] Keys
        {
            get
            {
                return _data.Keys.ToArray();
            }
        }

        public dynamic[] Values
        {
            get
            {
                return _data.Values.ToArray();
            }
        }

        public List<T> ToList<T>()
        {
            return Values.Cast<T>().ToList();
        }

        private bool IsEmpty
        {
            get
            {
                if (this._data.Count == 0)
                    return true;
                foreach (var pair in this._data)
                {
                    if (!(pair.Value is HierarchicalObject) && pair.Value != null)
                        return false;
                    else
                        if (pair.Value is HierarchicalObject && !((HierarchicalObject)pair.Value).IsEmpty)
                        return false;
                }

                return true;
            }
        }

        public void Clear()
        {
            _data.Clear();
        }

        private void Optimize()
        {
            foreach (var key in this._data.Keys.ToArray())
            {
                if (this._data[key] is HierarchicalObject && ((HierarchicalObject)this._data[key]).IsEmpty)
                    this._data.Remove(key);
                else if (this._data[key] is HierarchicalObject)
                    ((HierarchicalObject)this._data[key]).Optimize();
            }
        }

        internal string GetXml()
        {
            this.Optimize();
            var memoryStream = new MemoryStream();
            var serializer = HierarchicalObjectCrutch.GetSerializer(typeof(XmlItems));
            serializer.Serialize(memoryStream, this.GetXmlItems());
            return CurrentXmlEncoding.GetString(memoryStream.GetBuffer());
        }

        public override string ToString()
        {
            return GetXml();
        }

        internal XmlItems GetXmlItems()
        {
            XmlItems items = new XmlItems();
            foreach (var item in this._data)
            {
                var xmlItem = new XmlItem(item.Key, item.Value is HierarchicalObject ? item.Value.GetXmlItems() : item.Value);
                items.Add(xmlItem);
            }
            return items;
        }

        internal static XmlItems ItemsFromXml(string xmlString)
        {
            var memoryStream = new MemoryStream(CurrentXmlEncoding.GetBytes(xmlString));
            var serializer = HierarchicalObjectCrutch.GetSerializer(typeof(XmlItems));
            return (XmlItems)serializer.Deserialize(memoryStream);
        }

        internal static HierarchicalObject FromXmlItems(XmlItems items)
        {
            var hobject = new HierarchicalObject();
            foreach (var item in items)
            {
                hobject[item.Key] = item.Value is XmlItems ? FromXmlItems((XmlItems)item.Value) : item.Value;
            }
            return hobject;
        }

        public static HierarchicalObject FromXml(string xml)
        {
            var items = ItemsFromXml(xml);
            return FromXmlItems(items);
        }

        public static HierarchicalObject FromFile(string fileName)
        {
            var hobj = FromXml(File.ReadAllText(fileName));
            hobj.FileName = fileName;
            return hobj;
        }

        public static HierarchicalObject FromStream(Stream stream)
        {
            var buff = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(buff, 0, buff.Length);
            var xml = CurrentXmlEncoding.GetString(buff, 0, buff.Length);
            var hobj = FromXml(xml);
            hobj.Stream = stream;
            stream.Position = 0;
            return hobj;
        }
    }

    public class ParameterNotExistException : Exception
    {
        public ParameterNotExistException(string mess) : base(mess)
        {
        }
    }
}
