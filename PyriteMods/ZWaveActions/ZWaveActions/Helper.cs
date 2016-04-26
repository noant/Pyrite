using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveActions
{
    public static class Helper
    {
        private static object GetValue(ZWManager manager, ZWValueID v)
        {
            switch (v.GetType())
            {
                case ZWValueID.ValueType.Bool:
                    bool r1;
                    manager.GetValueAsBool(v, out r1);
                    return r1;
                case ZWValueID.ValueType.Byte:
                    byte r2;
                    manager.GetValueAsByte(v, out r2);
                    return r2;
                case ZWValueID.ValueType.Decimal:
                    decimal r3;
                    manager.GetValueAsDecimal(v, out r3);
                    return r3;
                case ZWValueID.ValueType.Int:
                    Int32 r4;
                    manager.GetValueAsInt(v, out r4);
                    return r4;
                case ZWValueID.ValueType.List:
                    string[] r5;
                    int selected;
                    manager.GetValueListItems(v, out r5);
                    manager.GetValueListSelection(v, out selected);
                    return new ItemsSelection(r5, selected);
                case ZWValueID.ValueType.Raw:
                    int[] raw;
                    manager.GetValueListValues(v, out raw);
                    return raw;
                case ZWValueID.ValueType.Schedule:
                    return "Schedule";
                case ZWValueID.ValueType.Short:
                    short r7;
                    manager.GetValueAsShort(v, out r7);
                    return r7;
                case ZWValueID.ValueType.String:
                    string r8;
                    manager.GetValueAsString(v, out r8);
                    return r8;
                default:
                    throw new Exception("Cannot find type");
            }
        }

        public static bool SetValue(ZWManager manager, ZWValueID v, object obj)
        {
            switch (v.GetType())
            {
                case ZWValueID.ValueType.Bool:
                    return manager.SetValue(v, (bool)obj);
                case ZWValueID.ValueType.Byte:
                    return manager.SetValue(v, (byte)obj);
                case ZWValueID.ValueType.Decimal:
                    return manager.SetValue(v, (float)(decimal)obj);
                case ZWValueID.ValueType.Int:
                    return manager.SetValue(v, (int)obj);
                case ZWValueID.ValueType.List:
                    return manager.SetValueListSelection(v, ((ItemsSelection)obj).Values[((ItemsSelection)obj).SelectedItem]);
                case ZWValueID.ValueType.Schedule:
                    return false;
                case ZWValueID.ValueType.Short:
                    return manager.SetValue(v, (ushort)obj);
                case ZWValueID.ValueType.String:
                    return manager.SetValue(v, (string)obj);
                default:
                    throw new Exception("Cannot find type");
            }
        }

        public static T GetValue<T>(this ZWValueID v, ZWManager manager)
        {
            return (T)GetValue(manager, v);
        }

        public static object GetValue(this ZWValueID v, ZWManager manager)
        {
            return GetValue(manager, v);
        }

        public static bool SetValue<T>(this ZWValueID v, ZWManager manager, object value)
        {
            return SetValue(manager, v, value);
        }

        public class ItemsSelection
        {
            public ItemsSelection(string[] values, int selectedItem)
            {
                Values = values;
                SelectedItem = selectedItem;
            }
            public string[] Values { get; private set; }
            public int SelectedItem { get; private set; }
        }
    }
}
