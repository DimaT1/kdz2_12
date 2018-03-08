using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary
{
    /// <summary>
    /// Структура параметров изменения списка
    /// </summary>
    public struct ModifyListClass
    {
        public bool SortByID { get; set; }
        public bool SortByStations { get; set; }
        public bool FilterByMag { get; set; }
        public double MagValue { get; set; }

        public ModifyListClass(bool sortId=false, bool sortSt=false, bool filt=false, double mag=0)
        {
            SortByID = sortId;
            SortByStations = sortSt;
            FilterByMag = filt;
            MagValue = mag;
        }
    }
}
