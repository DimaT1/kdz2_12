using System;
using System.Collections.Generic;
using System.Globalization;

namespace ModelLibrary
{
    public class EarthQuake : IFormattable, IValid, ICorrect
    {

        private QuakeItem<int> id = new QuakeItem<int>();

        public QuakeItem<int> Id => id;


        private Coordinates coordinates = new Coordinates();

        public QuakeItem<double> CoordinateLat => coordinates.Lat;

        public QuakeItem<double> CoordinateLong => coordinates.Long;


        private QuakeItem<double> depth = new QuakeItem<double>();

        public QuakeItem<double> Depth => depth;

        private QuakeItem<double> mag = new QuakeItem<double>();

        public QuakeItem<double> Mag => mag;

        private QuakeItem<int> stations = new QuakeItem<int>();

        public QuakeItem<int> Stations => stations;

        public bool Valid => id.Valid && coordinates.Valid && depth.Valid && mag.Valid && stations.Valid;

        public bool Correct => id.Correct && coordinates.Correct && depth.Correct && mag.Correct && stations.Correct;

        public EarthQuake(List<string> dataList, CultureInfo cultureInfo)
        {
            id.SetFromStr(dataList[0], cultureInfo, QuakeItem<int>.Id);
            coordinates = new Coordinates(dataList[1], dataList[2], cultureInfo);
            depth.SetFromStr(dataList[3], cultureInfo, QuakeItem<double>.Depth);
            mag.SetFromStr(dataList[4], cultureInfo, QuakeItem<double>.Mag);
            stations.SetFromStr(dataList[5], cultureInfo, QuakeItem<int>.Stations);
        }

        public EarthQuake()
        {
            id = new QuakeItem<int>();
            coordinates = new Coordinates();
            coordinates.Lat = new QuakeItem<double>();
            coordinates.Long = new QuakeItem<double>();
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

        public override string ToString()
        {
            return $"\"{id}\",{coordinates.Lat},{coordinates.Long},{depth},{mag},{stations}";
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            return $"\"{id.ToString(format, formatProvider)},\"{coordinates.Lat.ToString(format, formatProvider)},{coordinates.Long.ToString(format, formatProvider)},{depth.ToString(format, formatProvider)},{mag.ToString(format, formatProvider)},{stations.ToString(format, formatProvider)}";
        }

        public string ToString(CultureInfo cultureInfo = null)
        {
            return $"\"{id.ToString(cultureInfo)},\"{coordinates.Lat.ToString(cultureInfo)},{coordinates.Long.ToString(cultureInfo)},{depth.ToString(cultureInfo)},{mag.ToString(cultureInfo)},{stations.ToString(cultureInfo)}";
        }
    }
}
