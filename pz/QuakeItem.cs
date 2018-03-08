using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace ModelLibrary
{
    public class QuakeItem<T> : IFormattable, IComparable, IValid, ICorrect, IEquatable<QuakeItem<T>> where T : IFormattable, IComparable
    {
        private T obj;
        public T Obj
        {
            get
            {
                if (this.Correct)
                    return obj;
                else if (objValid)
                    throw new ArgumentException($"{typeof(T)} object {this} is incorrect", "objc");
                else
                    throw new ArgumentException($"{typeof(T)} object {this} is invalid", "objv");
            }
        }

        private bool objValid;

        private string objName;

        private bool objCorrect;

        public bool Valid => objValid;

        public bool Correct
        {
            get
            {
                const double minLat = -90,
                    maxLat = 90,
                    minLong = -180,
                    maxLong = 180,
                    minDepth = 0,
                    maxDepth = 1000,
                    minMag = 1,
                    maxMag = 9.5;

                if (!objCorrect)
                {
                    return false;
                }

                switch (objName)
                {
                    case Id:
                        return objValid;
                    case Lat:
                        return objValid && (obj.CompareTo(minLat) >= 0) && (obj.CompareTo(maxLat) <= 0);
                    case Long:
                        return objValid && (obj.CompareTo(minLong) >= 0) && (obj.CompareTo(maxLong) <= 0);
                    case Depth:
                        return objValid && (obj.CompareTo(minDepth) >= 0) && (obj.CompareTo(maxDepth) <= 0);
                    case Mag:
                        return objValid && (obj.CompareTo(minMag) >= 0) && (obj.CompareTo(maxMag) <= 0);
                    case Stations:
                        return objValid;
                }
                return false;
            }
        }

        public const string Id = "id";
        public const string Lat = "lat";
        public const string Long = "long";
        public const string Depth = "depth";
        public const string Mag = "mag";
        public const string Stations = "stations";
        public const string StrError = "Error";
        public const string StrNa = "NA";

        public QuakeItem()
        {
            objValid = false;
            objCorrect = false;
        }

        public QuakeItem(string str, CultureInfo cultureInfo, string objName)
        {
            this.SetFromStr(str, cultureInfo, objName);
        }
        public void SetFromStr(string str, CultureInfo cultureInfo, string objName)
        {
            if (str == StrError)
            {
                objValid = true;
                objCorrect = false;
            }

            switch (objName)
            {
                case Id:
                    str = str.Replace("\"", "");
                    break;
                case Lat:
                case Long:
                case Depth:
                case Mag:
                    if (cultureInfo == null)
                    {
                        cultureInfo = CultureInfo.GetCultureInfo("en-US");
                    }
                    break;
            }

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                this.objName = objName;
                obj = (T)converter.ConvertFromString(null, cultureInfo, str);
                objValid = true;
                objCorrect = true;
            }
            catch
            {
                objValid = false;
                objCorrect = false;
            }
        }
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (formatProvider == null)
            {
                formatProvider = CultureInfo.GetCultureInfo("en-US");
            }

            switch (objName)
            {
                case Id:
                    if (objValid)
                    {
                        if (Correct)
                        {
                            return "\"" + obj.ToString(format, formatProvider) + "\"";
                        }
                        return StrError;
                    }
                    return StrNa;
                case Lat:
                case Long:
                case Depth:
                case Mag:
                    if (objValid)
                    {
                        if (Correct)
                        {
                            return obj.ToString(format, formatProvider);
                        }
                        return StrError;
                    }
                    return StrNa;
                case Stations:
                    if (objValid)
                    {
                        if (Correct)
                        {
                            return obj.ToString(format, formatProvider);
                        }
                        return StrError;
                    }
                    return StrNa;
            }
            return StrNa;
        }
        public override string ToString()
        {
            return $"{this}";
        }
        public string ToString(CultureInfo cultureInfo=null)
        {
            return this.ToString("", cultureInfo);
        }

        public int CompareTo(QuakeItem<T> other)
        {
            if (objValid)
            {
                if (other.objValid)
                {
                    return obj.CompareTo(other.obj);
                }
                return 1;
            }
            else if (other.objValid)
            {
                return -1;
            }
            return 0;
        }
        public int CompareTo(object other)
        {
            QuakeItem<T> otherQuake = new QuakeItem<T>();
            otherQuake.SetFromStr(other.ToString(), CultureInfo.GetCultureInfo("en-US"), objName);
            return this.CompareTo(otherQuake);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as QuakeItem<T>);
        }

        public bool Equals(QuakeItem<T> other)
        {
            return other != null &&
                   EqualityComparer<T>.Default.Equals(obj, other.obj) &&
                   objName == other.objName &&
                   Valid == other.Valid &&
                   Correct == other.Correct;
        }
        public static bool operator <(QuakeItem<T> q1, QuakeItem<T> q2)
        {
            return q1.CompareTo(q2) < 0;
        }

        public static bool operator >(QuakeItem<T> q1, QuakeItem<T> q2)
        {
            return q1.CompareTo(q2) > 0;
        }

        public static bool operator <=(QuakeItem<T> q1, QuakeItem<T> q2)
        {
            return q1.CompareTo(q2) <= 0;
        }
        public static bool operator >=(QuakeItem<T> q1, QuakeItem<T> q2)
        {
            return q1.CompareTo(q2) >= 0;
        }
        public static bool operator ==(QuakeItem<T> q1, QuakeItem<T> q2)
        {
            return q1.CompareTo(q2) == 0;
        }
        public static bool operator !=(QuakeItem<T> q1, QuakeItem<T> q2)
        {
            return q1.CompareTo(q2) != 0;
        }
        public static bool operator <(QuakeItem<T> q1, double q2)
        {
            if (q1.Valid && q1.Correct)
            {
                return q1.obj.CompareTo(q2) < 0;
            }
            else
            {
                return true;
            }
        }

        public static bool operator >(QuakeItem<T> q1, double q2)
        {
            if (q1.Valid && q1.Correct)
            {
                return q1.obj.CompareTo(q2) > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
