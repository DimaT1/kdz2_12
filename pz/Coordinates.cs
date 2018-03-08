using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ModelLibrary
{
    struct Coordinates : IValid, ICorrect
    {

        private QuakeItem<double> _lat;

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

        private QuakeItem<double> _long;
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

        public Coordinates(string _lat, string _long, CultureInfo cultureInfo)
        {
            this._lat = new QuakeItem<double>(_lat, cultureInfo, QuakeItem<double>.Lat);
            this._long = new QuakeItem<double>(_long, cultureInfo, QuakeItem<double>.Long);
        }


        public bool Valid => _lat.Valid && _long.Valid;


        public bool Correct => _lat.Correct && _long.Correct;
    }
}
