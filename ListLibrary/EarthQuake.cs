using System;
using System.Collections.Generic;
using System.Globalization;

namespace ModelLibrary
{
    /// <summary>
    /// Класс землетрясения
    /// </summary>
    public class EarthQuake : IFormattable, IValid, ICorrect
    {
        /// <summary>
        /// Идентификатор землетрясения
        /// </summary>
        private QuakeItem<int> id = new QuakeItem<int>();
        /// <summary>
        /// Свойство идентификатора землетрясения
        /// </summary>
        public QuakeItem<int> Id => id;


        /// <summary>
        /// Координаты землетрясения
        /// </summary>
        private Coordinates coordinates;
        /// <summary>
        /// Свойство долготы землетрясения
        /// </summary>
        public QuakeItem<double> CoordinateLat => coordinates.Lat;
        /// <summary>
        /// Свойство широты землетрясения
        /// </summary>
        public QuakeItem<double> CoordinateLong => coordinates.Long;


        /// <summary>
        /// Глубина землетрясения
        /// </summary>
        private QuakeItem<double> depth = new QuakeItem<double>();
        /// <summary>
        /// Свойство глубины землетрясения
        /// </summary>
        public QuakeItem<double> Depth => depth;


        /// <summary>
        /// Магнитура землетрясения
        /// </summary>
        private QuakeItem<double> mag = new QuakeItem<double>();
        /// <summary>
        /// Свойство магнитуры землетрясения
        /// </summary>
        public QuakeItem<double> Mag => mag;


        /// <summary>
        /// Количество станций уловивших землетрясение 
        /// </summary>
        private QuakeItem<int> stations = new QuakeItem<int>();
        /// <summary>
        /// Свойство количества станций уловивших землетрясение
        /// </summary>
        public QuakeItem<int> Stations => stations;


        /// <summary>
        /// Реализация интерфейса IValid
        /// </summary>
        public bool Valid => id.Valid && coordinates.Valid && depth.Valid && mag.Valid && stations.Valid;

        /// <summary>
        /// Реализация интерфейса ICorrect
        /// </summary>
        public bool Correct => id.Correct && coordinates.Correct && depth.Correct && mag.Correct && stations.Correct;


        /// <summary>
        /// Конструктор из списка строк
        /// </summary>
        /// <param name="dataList">Список строк</param>
        /// <param name="cultureInfo">Локаль языка</param>
        public EarthQuake(List<string> dataList, CultureInfo cultureInfo)
        {
            id.SetFromStr(dataList[0], cultureInfo, QuakeItem<int>.Id);
            coordinates = new Coordinates(dataList[1], dataList[2], cultureInfo);
            depth.SetFromStr(dataList[3], cultureInfo, QuakeItem<double>.Depth);
            mag.SetFromStr(dataList[4], cultureInfo, QuakeItem<double>.Mag);
            stations.SetFromStr(dataList[5], cultureInfo, QuakeItem<int>.Stations);
        }

        /// <summary>
        /// Конструктор не валидного объекта землетрясения
        /// </summary>
        public EarthQuake()
        {
            id = new QuakeItem<int>();
            coordinates = new Coordinates();
            depth = new QuakeItem<double>();
            mag = new QuakeItem<double>();
            stations = new QuakeItem<int>();
        }

        public void SetElemFromStr(string val, int index, CultureInfo cultureInfo)
        {
            switch (index)
            {
                case 0:
                    id.SetFromStr(val, cultureInfo, QuakeItem<int>.Id);
                    break;
                case 1:
                    coordinates.Lat.SetFromStr(val, cultureInfo, QuakeItem<double>.Lat);
                    break;
                case 2:
                    coordinates.Long.SetFromStr(val, cultureInfo, QuakeItem<double>.Long);
                    break;
                case 3:
                    depth.SetFromStr(val, cultureInfo, QuakeItem<double>.Depth);
                    break;
                case 4:
                    mag.SetFromStr(val, cultureInfo, QuakeItem<double>.Mag);
                    break;
                case 5:
                    stations.SetFromStr(val, cultureInfo, QuakeItem<int>.Stations);
                    break;
            }
        }

        public string GetElemStr(int index, CultureInfo cultureInfo)
        {
            switch (index)
            {
                case 0:
                    return id.ToString(cultureInfo);
                case 1:
                    return coordinates.Lat.ToString(cultureInfo);
                case 2:
                    return coordinates.Long.ToString(cultureInfo);
                case 3:
                    return depth.ToString(cultureInfo);
                case 4:
                    return mag.ToString(cultureInfo);
                case 5:
                    return stations.ToString(cultureInfo);
            }
            return QuakeItem<int>.StrNa;
        }

        /// <summary>
        /// Переводит объект землетрясения в список строк характеристик.
        /// Локаль по умолчанию английская.
        /// </summary>
        /// <returns></returns>
        public List<string> GetList(CultureInfo cultureInfo=null)
        {
            List<string> res = new List<string>
            {
                id.ToString(),

                coordinates.Lat.ToString(cultureInfo),
                coordinates.Long.ToString(cultureInfo),

                depth.ToString(cultureInfo),
                mag.ToString(cultureInfo),
                stations.ToString(cultureInfo)
            };
            return res;
        }

        /// <summary>
        /// Реализация интерфейса IFormattable.
        /// Локаль английская.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"\"{id}\",{coordinates.Lat},{coordinates.Long},{depth},{mag},{stations}";
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
            return $"\"{id.ToString(format, formatProvider)},\"{coordinates.Lat.ToString(format, formatProvider)},{coordinates.Long.ToString(format, formatProvider)},{depth.ToString(format, formatProvider)},{mag.ToString(format, formatProvider)},{stations.ToString(format, formatProvider)}";
        }

        /// <summary>
        /// Реализация интерфейса IFormattable.
        /// Локаль по умолчанию английская.
        /// </summary>
        /// <param name="cultureInfo">Локаль языка</param>
        /// <returns>Строковое значение характеристики (NA при отрицательной валидности)</returns>
        public string ToString(CultureInfo cultureInfo = null)
        {
            return $"\"{id.ToString(cultureInfo)},\"{coordinates.Lat.ToString(cultureInfo)},{coordinates.Long.ToString(cultureInfo)},{depth.ToString(cultureInfo)},{mag.ToString(cultureInfo)},{stations.ToString(cultureInfo)}";
        }
    }
}
