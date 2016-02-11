using System;
using UniActionsClientIntefaces;

namespace UniActionsCore.ScenarioCreating
{
    internal static class Helper
    {
        public static string CreateParamsViewString(object obj)
        {
            var template = new Func<object, string, string>((val, name) =>
            {
                if (val == null)
                    val = "[пусто]";
                return ", " + name + "=" + val;
            });

            string result = "";
            var type = obj.GetType();
            var hNameType = typeof(HumanFriendlyNameAttribute);
            foreach (var field in type.GetFields())
            {
                var name = field.Name;
                var hNameAttr = Attribute.GetCustomAttribute(field, hNameType);
                if (hNameAttr != null)
                    name = ((HumanFriendlyNameAttribute)hNameAttr).Name;
                var value = field.GetValue(obj);
                result += template(value, name);
            }
            foreach (var property in type.GetProperties())
            {
                var name = property.Name;
                var hNameAttr = Attribute.GetCustomAttribute(property, hNameType);
                if (hNameAttr != null)
                    name = ((HumanFriendlyNameAttribute)hNameAttr).Name;
                var value = property.GetValue(obj);
                result += template(value, name);
            }

            if (string.IsNullOrWhiteSpace(result))
                return result.Substring(2);
            return "";
        }
    }
}
