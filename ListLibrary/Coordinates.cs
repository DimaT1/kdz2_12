using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListLibrary
{
    struct Coordinates
    {
        public QuakeItem<double> Lat;
        public QuakeItem<double> Long;

        public Coordinates(string _lat, string _long)
        {
            Lat = new QuakeItem<double>();
            Long = new QuakeItem<double>();
            Lat.SetFromStr(_lat);
            Long.SetFromStr(_long);
        }
    }
}
