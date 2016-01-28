using System;
using System.Linq;
using System.Reflection;

namespace HierarchicalData
{
    public static class Helper
    {
        public static HierarchicalObject CreateBySettingsAttribute(object obj)
        {
            var hobject = new HierarchicalObject();

            var type = obj.GetType();
            var settingAttrType = typeof(SettingsAttribute);

            var props = type.GetProperties().Where(x=>x.CustomAttributes.Any(z=>z.AttributeType==settingAttrType));
            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.CustomAttributes.Any(z => z.AttributeType == settingAttrType));
            foreach (var property in props)
            {
                hobject[property.Name] = property.GetValue(obj);
            }
            foreach (var field in fields)
                hobject[field.Name] = field.GetValue(obj);
            return hobject;
        }

        public static void SetToObject(object obj, HierarchicalObject hobject)
        {
            var type = obj.GetType();
            var settingAttrType = typeof(SettingsAttribute);

            var props = type.GetProperties().Where(x => x.CustomAttributes.Any(z => z.AttributeType == settingAttrType));
            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.CustomAttributes.Any(z => z.AttributeType == settingAttrType));
            foreach (var property in props)
            {
                if (hobject.ContainsKey(property.Name))
                    property.SetValue(obj, hobject[property.Name]);
            }
            foreach (var field in fields)
            {
                if (hobject.ContainsKey(field.Name))
                    field.SetValue(obj, hobject[field.Name]);
            }
        }

        public static object CloneHierarchicalObject(object obj)
        {
            var hobject = CreateBySettingsAttribute(obj);
            var objclone = obj.GetType().GetConstructor(new Type[0]).Invoke(new object[0]);
            SetToObject(objclone, hobject);
            return objclone;
        }
    }

    public class SettingsAttribute : Attribute { }
}
