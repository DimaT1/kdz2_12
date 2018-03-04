using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ListLibrary
{
    /// <summary>
    /// Структура описывает географические координаты землетрясения
    /// </summary>
    struct Coordinates : IValid
    {
        /// <summary>
        /// Географическая широта
        /// </summary>
        private QuakeItem<double> _lat;

        /// <summary>
        /// Свойство географической широты
        /// </summary>
        public QuakeItem<double> Lat
        {
            get
            {
                try
                {
                    return _lat;
                }
                catch (ArgumentNullException)
                {
                    _lat = new QuakeItem<double>();
                    return _lat;
                }
            }
            set
            {
                _lat = value;
            }
        }

        /// <summary>
        /// Географическая долгота
        /// </summary>
        private QuakeItem<double> _long;

        /// <summary>
        /// Свойство географической долготы
        /// </summary>
        public QuakeItem<double> Long
        {
            get
            {
                try
                {
                    return _long;
                }
                catch (ArgumentNullException)
                {
                    _long = new QuakeItem<double>();
                    return _long;
                }
            }
            set
            {
                _long = value;
            }
        }

        /// <summary>
        /// Конструктор структуры
        /// </summary>
        /// <param name="_lat">Географическая широта</param>
        /// <param name="_long">Географическая долгота</param>
        /// <param name="cultureInfo">Локаль языка</param>
        public Coordinates(string _lat, string _long, CultureInfo cultureInfo)
        {
            this._lat = new QuakeItem<double>(_lat, cultureInfo);
            this._long = new QuakeItem<double>(_long, cultureInfo);
        }

        /// <summary>
        /// Реализация интерфейса IValid
        /// </summary>
        public bool Valid => _lat.Valid && _long.Valid;
    }
}
