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
                throw new ArgumentException($"{typeof(T)} object is invalid");
            }
        }

    }
}
