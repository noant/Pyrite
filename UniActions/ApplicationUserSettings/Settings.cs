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

        public Settings() {
            FileName = Defaults.FileName;
        }
        public Settings(string fileName)
        {
            FileName = fileName;
        }

        public string FileName {get; private set;}

        private XmlParameters Parameters;

        private void ValidateFile()
        {
            if (!File.Exists(FileName))
            {
                File.AppendAllText(FileName, "<xml></xml>");
            }
        }
        
        private void LoadParameters()
        {
            ValidateFile();
            var serializer = new XmlSerializer(typeof(XmlParameters));
            FileStream fs = new FileStream(FileName, FileMode.Open);
            Parameters = (XmlParameters)serializer.Deserialize(fs);
            fs.Close();
        }

        private void SaveParameters()
        {
            FileStream fs = new FileStream(FileName, FileMode.Truncate );
            var serializer = new XmlSerializer(typeof(XmlParameters));
            serializer.Serialize(fs, Parameters);
            fs.Close();
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
