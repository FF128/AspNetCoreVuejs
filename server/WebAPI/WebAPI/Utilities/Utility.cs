using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAPI.Utilities
{
    public class PropertyCompareResult
    {
        public string Name { get; private set; }
        public object OldValue { get; private set; }
        public object NewValue { get; private set; }

        public PropertyCompareResult(string name, object oldValue, object newValue)
        {
            Name = name;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
    public class IgnorePropertyCompareAttribute : Attribute { }

    public class Utility
    {
        private List<PropertyCompareResult> Compare<T>(T oldObject, T newObject)
        {
            var type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            List<PropertyCompareResult> result = new List<PropertyCompareResult>();

            foreach (PropertyInfo pi in properties)
            {
                if (pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(IgnorePropertyCompareAttribute)))
                {
                    continue;
                }

                object oldValue = pi.GetValue(oldObject), newValue = pi.GetValue(newObject);

                if (!object.Equals(oldValue, newValue))
                {
                    var displayName = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().FirstOrDefault();
                    result.Add(new PropertyCompareResult(displayName == null ? pi.Name : displayName.DisplayName, oldValue, newValue));
                }
            }

            return result;
        }
        public string CreateMesssage<T>(T oldobject, T newobject)
        {
            var response = "";
            try
            {
                var result = Compare(oldobject, newobject);
                foreach (PropertyCompareResult item in result)
                {
                    if (item.OldValue == null && item.NewValue != null)
                    {
                        response += $"{item.Name}={item.NewValue};"; // for AddRec & Delete
                    }
                    else if (item.OldValue != null && item.NewValue != null) // for EditRec
                    {
                        response += $"{item.Name}:{item.OldValue}={item.NewValue};";
                    }
                }

                response = response.TrimEnd(',', ' ').Trim();
            }
            catch (Exception)
            {

            }
            return response;
        }
        public string CreateMessage<T>(IEnumerable<T> oldObj, IEnumerable<T> newObj)
        {
           // var result = oldObj.Where(p => !newObj.Any());

            var response = "";
            try
            {
                var result = Compare(oldObj, newObj);
                foreach (PropertyCompareResult item in result)
                {
                    if (item.OldValue == null && item.NewValue != null)
                    {
                        response += $"{item.Name}={item.NewValue};"; // for AddRec & Delete
                    }
                    else if (item.OldValue != null && item.NewValue != null) // for EditRec
                    {
                        response += $"{item.Name}:{item.OldValue}={item.NewValue};";
                    }
                }

                response = response.TrimEnd(',', ' ').Trim();
            }
            catch (Exception)
            {

            }
            return response;
        }
        public T Clone<T>(T source)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }
        //public int SaveAuditToSP(string connectionstring, string sp, SqlParameter[] param, bool Commit = false)
        //{
        //    var baseworker = new BaseWorker();

        //    var result = baseworker.ExecuteProcedure(connectionstring, "", sp, param, 1, Commit, 0);
        //    return result.Item2;
        //}
    }
}
