using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListLibrary
{
    class DataListItem<T>
    {
        private T obj;
        private bool objValid;

        public T Obj
        {
            get
            {
                if (objValid)
                    return obj;
                else
                    throw new ArgumentException("id is invalid");
            }
        }

        public void Set(string str)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                obj = (T)converter.ConvertFromString(str);
                objValid = true;
            }
            catch
            {
                objValid = false;
                throw new ArgumentException($"{typeof(T)} object {this} is invalid");
            }
        }

        public override string ToString()
        {
            string res = obj.ToString();
            if (objValid)
            {
                object[] types = { typeof(float), typeof(decimal), typeof(double) };
                foreach (var t in types)
                {
                    if (obj.GetType() == t)
                    {
                        res = res.Replace(",", ".");
                    }
                }
            }
            else
            {
                return "NA";
            }
            return res;
        }
    }
}
