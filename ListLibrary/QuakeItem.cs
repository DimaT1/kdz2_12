using System;
using System.ComponentModel;
using System.Globalization;

namespace ModelLibrary
{
    /// <summary>
    /// Класс, описывающий обобщённую характеристику землетрясения
    /// </summary>
    /// <typeparam name="T">int, double</typeparam>
    public class QuakeItem<T> : IFormattable, IComparable, IValid, ICorrect where T : IFormattable, IComparable
    {
        /// <summary>
        /// Численная характеристика землетрясения
        /// </summary>
        private T obj;

        /// <summary>
        /// Свойство характеристики obj
        /// </summary>
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

        /// <summary>
        /// Поле определяет валидность (наличие) характеристики
        /// </summary>
        private bool objValid;

        /// <summary>
        /// Поле определяет название характеристики
        /// </summary>
        private string objName;

        private bool objCorrect;

        /// <summary>
        /// Реализация интерфейса IValid
        /// </summary>
        public bool Valid => objValid;

        /// <summary>
        /// Реализация интерфейса ICorrect
        /// </summary>
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
                    minMag = 0,
                    maxMag = 10;

                if (!objCorrect)
                {
                    return false;
                }

                switch (objName)
                {
                    case Id:
                        return objValid;
                    case Lat:
                        return objValid && (obj.CompareTo(minLat) > 0) && (obj.CompareTo(maxLat) < 0);
                    case Long:
                        return objValid && (obj.CompareTo(minLong) > 0) && (obj.CompareTo(maxLong) < 0);
                    case Depth:
                        return objValid && (obj.CompareTo(minDepth) > 0) && (obj.CompareTo(maxDepth) < 0);
                    case Mag:
                        return objValid && (obj.CompareTo(minMag) > 0) && (obj.CompareTo(maxMag) < 0);
                    case Stations:
                        return objValid;
                }
                return false;
            }
        }

        /// Глобальные константы
        public const string Id = "id";
        public const string Lat = "lat";
        public const string Long = "long";
        public const string Depth = "depth";
        public const string Mag = "mag";
        public const string Stations = "stations";
        public const string StrError = "Error";
        public const string StrNa = "Na";

        /// <summary>
        /// Конструктор создаёт объект характеристики с отрицательной валидностью
        /// </summary>
        public QuakeItem()
        {
            objValid = false;
        }

        /// <summary>
        /// Конструктор создаёт объект характеристики из строки и определяет валидность значения
        /// </summary>
        /// <param name="str">Строка со значением характеристики</param>
        /// <param name="cultureInfo">Сведения о языке</param>
        public QuakeItem(string str, CultureInfo cultureInfo, string objName)
        {
            this.SetFromStr(str, cultureInfo, objName);
        }

        /// <summary>
        /// Устанавливает значение из строки и определяет валидность значения
        /// </summary>
        /// <param name="str">Строка со значением характеристики</param>
        /// <param name="cultureInfo">Сведения о языке</param>
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

        /// <summary>
        /// Реализация интерфейса IFormattable.
        /// Локаль по умолчанию английская.
        /// </summary>
        /// <param name="format">Формат строки</param>
        /// <param name="formatProvider">Механизм для извлечения объекта с целью управления форматированием</param>
        /// <returns>Строковое значение характеристики (NA при отрицательной валидности)</returns>
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

        /// <summary>
        /// Метод преобразования характеристики в строку
        /// </summary>
        /// <returns>Строковое значение характеристики (NA при отрицательной валидности)</returns>
        public override string ToString()
        {
            return $"{this}";
        }

        /// <summary>
        /// Метод преобразования характеристики в строку
        /// </summary>
        /// <param name="cultureInfo">Локаль языка</param>
        /// <returns>Строковое значение характеристики (NA при отрицательной валидности)</returns>
        public string ToString(CultureInfo cultureInfo=null)
        {
            return this.ToString("", cultureInfo);
        }


        /// <summary>
        /// Реализация сравнения с другой характеристикой.
        /// Не валидное значение считается меньшне валидного. 
        /// </summary>
        /// <param name="other">Другая характеристика</param>
        /// <returns></returns>
        public int CompareTo(QuakeItem<T> other)
        {
            if (objValid)
            {
                if (other.objValid)
                {
                    return obj.CompareTo(other);
                }
                return 1;
            }
            else if (other.objValid)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// Реализация интерфейса IComparable.
        /// Не валидное значение считается меньшне валидного.
        /// Объект приводится к английской локали языка
        /// </summary>
        /// <param name="other">Объект для сравнения</param>
        /// <returns>Результат сравнения характеристики с объектом other</returns>
        public int CompareTo(object other)
        {
            QuakeItem<T> otherQuake = new QuakeItem<T>();
            otherQuake.SetFromStr(other.ToString(), CultureInfo.GetCultureInfo("en-US"), objName);
            return this.CompareTo(otherQuake);
        }

        /// <summary>
        /// Перегрузка оператора <
        /// </summary>
        /// <param name="q1">Первый операнд</param>
        /// <param name="q2">Второй операнд</param>
        /// <returns>Результат операции сравнения</returns>
        public static bool operator <(QuakeItem<T> q1, QuakeItem<T> q2)
        {
            return q1.CompareTo(q2) < 0;
        }

        /// <summary>
        /// Перегрузка оператора >
        /// </summary>
        /// <param name="q1">Первый операнд</param>
        /// <param name="q2">Второй операнд</param>
        /// <returns>Результат операции сравнения</returns>
        public static bool operator >(QuakeItem<T> q1, QuakeItem<T> q2)
        {
            return q1.CompareTo(q2) > 0;
        }

        /// <summary>
        /// Перегрузка оператора <=
        /// </summary>
        /// <param name="q1">Первый операнд</param>
        /// <param name="q2">Второй операнд</param>
        /// <returns>Результат операции сравнения</returns>
        public static bool operator <=(QuakeItem<T> q1, QuakeItem<T> q2)
        {
            return q1.CompareTo(q2) <= 0;
        }

        /// <summary>
        /// Перегрузка оператора >=
        /// </summary>
        /// <param name="q1">Первый операнд</param>
        /// <param name="q2">Второй операнд</param>
        /// <returns>Результат операции сравнения</returns>
        public static bool operator >=(QuakeItem<T> q1, QuakeItem<T> q2)
        {
            return q1.CompareTo(q2) >= 0;
        }

        /// <summary>
        /// Перегрузка оператора ==
        /// </summary>
        /// <param name="q1">Первый операнд</param>
        /// <param name="q2">Второй операнд</param>
        /// <returns>Результат операции сравнения</returns>
        public static bool operator ==(QuakeItem<T> q1, QuakeItem<T> q2)
        {
            return q1.CompareTo(q2) == 0;
        }

        /// <summary>
        /// Перегрузка оператора !=
        /// </summary>
        /// <param name="q1">Первый операнд</param>
        /// <param name="q2">Второй операнд</param>
        /// <returns>Результат операции сравнения</returns>
        public static bool operator !=(QuakeItem<T> q1, QuakeItem<T> q2)
        {
            return q1.CompareTo(q2) != 0;
        }
    }
}
