using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Spottify2.Models.Request
{
    public class BaseRequestModel
    {
        public Dictionary<string, string> ToDictionary()
        {
            var dictionary = new Dictionary<string, string>();
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                var value = property.GetValue(this);
                if (value != null)
                {
                    dictionary[property.Name] = value.ToString();
                }
            }
            return dictionary;
        }
    }
}
