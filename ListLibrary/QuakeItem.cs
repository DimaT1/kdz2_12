using System;
using System.ComponentModel;
using System.Globalization;

namespace ListLibrary
{
    /// <summary>
    /// Класс, описывающий обобщённую характеристику землетрясения
    /// </summary>
    /// <typeparam name="T">int, double</typeparam>
    public class QuakeItem<T> : IFormattable, IComparable, IValid where T : IFormattable, IComparable
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
                if (objValid)
                    return obj;
                else
                    throw new ArgumentException($"{typeof(T)} object {this} is invalid");
            }
        }

        /// <summary>
        /// Поле определяет валидность (действительность) характеристики
        /// </summary>
        private bool objValid;

        /// <summary>
        /// Реализация интерфейса IValid
        /// </summary>
        public bool Valid => objValid;

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
        public QuakeItem(string str, CultureInfo cultureInfo)
        {
            this.SetFromStr(str, cultureInfo);
        }

        /// <summary>
        /// Устанавливает значение из строки и определяет валидность значения
        /// </summary>
        /// <param name="str">Строка со значением характеристики</param>
        /// <param name="cultureInfo">Сведения о языке</param>
        public void SetFromStr(string str, CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
            {
                cultureInfo = CultureInfo.GetCultureInfo("en-US");
            }

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                obj = (T)converter.ConvertFromString(null, cultureInfo, str);
                objValid = true;
            }
            catch
            {
                objValid = false;
                throw new ArgumentException($"{typeof(T)} object {this} is invalid");
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
            otherQuake.SetFromStr(other.ToString(), CultureInfo.GetCultureInfo("en-US"));
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
