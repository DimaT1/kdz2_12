using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListLibrary
{
    public class ListItem
    {
        private DataListItem<string> _id;
        private DataListItem<double> _lat;
        private DataListItem<double> _long;
        private DataListItem<double> _depth;
        private DataListItem<double> _mag;
        private DataListItem<int> _stations;

        public ListItem(List<string> dataList)
        {
            _id.Set(dataList[0]);
            _lat.Set(dataList[1]);
            _long.Set(dataList[2]);
            _depth.Set(dataList[3]);
            _mag.Set(dataList[4]);
            _stations.Set(dataList[5]);
        }

        public List<string> ToStringList()
        {
            List<string> res = new List<string>();

            res.Add(_id.ToString());
            res.Add(_lat.ToString());
            res.Add(_long.ToString());
            res.Add(_depth.ToString());
            res.Add(_depth.ToString());
            res.Add(_stations.ToString());

            return res;
        }
    }
}
