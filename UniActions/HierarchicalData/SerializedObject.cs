using System;

namespace HierarchicalData
{
    [Serializable()]
    public class SerializedObject
    {
        public SerializedObject()
        {

        }

        public SerializedObject(object value)
        {
            Value = value;
        }

        private object _value;
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                HierarchicalObjectCrutch.Register(_value.GetType());
            }
        }
    }
}
