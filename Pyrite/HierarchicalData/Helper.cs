using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace HierarchicalData
{
    public static class HierarchicalObjectCrutch
    {
        private static List<Type> _registered = new List<Type>();
        public static void Register(Type type)
        {
            if (_registered.Contains(type)) return;

            var types = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                         from aType in assembly.GetTypes()
                         where type.IsAssignableFrom(aType)
                         select aType).ToArray();

            if (!type.IsInterface && !type.IsAbstract)
                _registered.Add(type);

            foreach (var aType in types)
            {
                if (!_registered.Contains(aType))
                    _registered.Add(aType);
            }
        }

        public static void Register(IEnumerable<Type> types)
        {
            foreach (var type in types)
                Register(type);
        }

        public static object CloneObject(object obj)
        {
            var serializedObject = new SerializedObject(obj);
            var serializer = GetSerializer(typeof(SerializedObject));
            var memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, serializedObject);
            memoryStream.Position = 0;
            return (((SerializedObject)serializer.Deserialize(memoryStream))).Value;
        }

        internal static XmlSerializer GetSerializer(Type type)
        {
            return new XmlSerializer(type, _registered.ToArray());
        }
    }
}
