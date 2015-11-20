using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniActionsCore
{
    public static class StringExt
    {
        public static string Set(this string str, object val)
        {
            var maxPars = 20;
            var prefix = "#";
            for (int i = 0; i < maxPars; i++)
            {
                var currReplacer = prefix + i.ToString();
                if (str.Contains(currReplacer))
                    return str.Replace(currReplacer, val.ToString());
            }
            return str;
        }
    }
}
