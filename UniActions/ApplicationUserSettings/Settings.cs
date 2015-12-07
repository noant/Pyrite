using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApplicationUserSettings
{
    public class Settings
    {
        public static class Defaults
        {
            public static readonly string FileName ="settings.xml";
        }
        
        public class ParameterNotExistException : Exception {
            public ParameterNotExistException(string message) : base(message)
            {}
        }

        public Settings(string fileName) {
            this.NeedSettingsSourceToWrite += (settings) => new FileStream(fileName, FileMode.Truncate);
            this.NeedSettingsSourceToRead += (settings) =>
                {
                    if (!File.Exists(fileName))
                        SaveStruct(new FileStream(fileName, FileMode.OpenOrCreate));
                    return new FileStream(fileName, FileMode.OpenOrCreate);
                };
        }

        public Settings() : this(Defaults.FileName) { }

        public Settings(Func<Settings, Stream> needSourceToRead, Func<Settings, Stream> needSourceToWrite, Action<Settings, Stream> afterCommit)
        {
            NeedSettingsSourceToWrite += needSourceToWrite;
            NeedSettingsSourceToRead += needSourceToRead;
            WhenChangesCommit += afterCommit;
        }

        private event Func<Settings, Stream> NeedSettingsSourceToWrite;
        private event Func<Settings, Stream> NeedSettingsSourceToRead;
        private event Action<Settings, Stream> WhenChangesCommit;

        private XmlParameters Parameters;
        
        private void LoadParameters()
        {
            var stream = NeedSettingsSourceToRead(this);
            var serializer = new XmlSerializer(typeof(XmlParameters));
            Parameters = (XmlParameters)serializer.Deserialize(stream);
            stream.Close();
        }
        
        private void SaveParameters()
        {
            var stream = NeedSettingsSourceToWrite(this);
            var serializer = new XmlSerializer(typeof(XmlParameters));
            serializer.Serialize(stream, Parameters);
            stream.Close();
            if (WhenChangesCommit != null)
                WhenChangesCommit(this, stream);
        }

        public void SaveStruct(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(XmlParameters));
            serializer.Serialize(stream, new XmlParameters() { 
                Items = new List<XmlItem>()
            });
            stream.Close();
            if (WhenChangesCommit != null)
                WhenChangesCommit(this, stream);
        }

        public bool Contains(string parName)
        {
            if (Parameters == null) LoadParameters();
            return Parameters.Items.Where(x => x.ParameterName == parName).Count() > 0;
        }

        public string GetValue(string parName)
        {
            if (Parameters == null) LoadParameters();
            if (Parameters.Items.Where(x => x.ParameterName == parName).Count() == 0) throw new ParameterNotExistException("Parameter not exist: "+parName);
            return Parameters.Items.Where(x => x.ParameterName == parName).First().Value;
        }

        public void SetValue(string parName, string value)
        {
            if (Parameters == null) LoadParameters();
            if (Parameters.Items.Where(x => x.ParameterName == parName).Count() == 0)
            {
                Parameters.Items.Add(new XmlItem(parName, value));
            }
            else
            {
                Parameters.Items.Where(x => x.ParameterName == parName).First().Value = value;
            }
            SaveParameters();
        }

        public void Clear()
        {
            Parameters.Items.Clear();
        }
    }
}
