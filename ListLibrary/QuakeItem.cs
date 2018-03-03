using System;
using System.ComponentModel;
using System.Globalization;

namespace ListLibrary
{
    class QuakeItem<T> : IFormattable where T : IFormattable
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
                    throw new ArgumentException($"{typeof(T)} object {this} is invalid");
            }
        }

        public QuakeItem() { }

        public QuakeItem(string str)
        {
            this.SetFromStr(str);
        }

        public void SetFromStr(string str)
        {
            str = str.Replace("\"", "");
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                obj = (T)converter.ConvertFromString(null, CultureInfo.GetCultureInfo("en-US"), str);
                objValid = true;
            }
            catch
            {
                objValid = false;
                throw new ArgumentException($"{typeof(T)} object {this} is invalid");
            }
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (objValid)
            {
                //TypeConverter converter = TypeDescriptor.GetConverter(typeof(string));
                if (format != null)
                {
                    //string res = (string)converter.ConvertToString(obj);
                    return obj.ToString(format, formatProvider);
                }
                return obj.ToString();
            }
            return "NA";
        }

        public override string ToString()
        {
            return $"{this}";
        }
    }
}
