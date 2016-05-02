using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveAction
{
    public class Node
    {
        private byte m_id = 0;

        public byte ID
        {
            get { return m_id; }
            internal set { m_id = value; }
        }

        private uint m_homeId = 0;

        public uint HomeID
        {
            get { return m_homeId; }
            internal set { m_homeId = value; }
        }

        private string m_name = "";

        public string Name
        {
            get { return m_name; }
            internal set { m_name = value; }
        }

        private string m_location = "";

        public string Location
        {
            get { return m_location; }
            internal set { m_location = value; }
        }

        private string m_label = "";

        public string Label
        {
            get { return m_label; }
            internal set { m_label = value; }
        }

        private string m_manufacturer = "";

        public string Manufacturer
        {
            get { return m_manufacturer; }
            internal set { m_manufacturer = value; }
        }

        private string m_product = "";

        public string Product
        {
            get { return m_product; }
            internal set { m_product = value; }
        }

        private List<ZWValueID> m_values = new List<ZWValueID>();

        public List<ZWValueID> Values
        {
            get { return m_values; }
        }

        public Node()
        {
        }

        public bool Loaded { get; internal set; }
        public bool Failed { get; internal set; }

        public void AddValue(ZWValueID valueID)
        {
            m_values.Add(valueID);
        }

        public void RemoveValue(ZWValueID valueID)
        {
            m_values.Remove(valueID);
        }

        internal bool RequestingValuesBegan { get; set; }
    }
}
