using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix
{
    public static class Extensions
    {
        public static string Capitalize(this string value, char seperator = ' ')
        {
            var split = value.Split(seperator);
            for (int k = 0; k < split.Length; k++)
                split[k] = split[k].First().ToString().ToUpper() + String.Join("", split[k].Skip(1));
            return string.Concat(split);
        }
    }
}
