using System.Collections.Generic;

namespace ListLibrary
{
    public class ListItem
    {
        private DataListItem<string> _id = new DataListItem<string>();
        private DataListItem<double> _lat = new DataListItem<double>();
        private DataListItem<double> _long = new DataListItem<double>();
        private DataListItem<double> _depth = new DataListItem<double>();
        private DataListItem<double> _mag = new DataListItem<double>();
        private DataListItem<int> _stations = new DataListItem<int>();

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

        // TODO
        public override string ToString()
        {
            string res = "";

            List<string> list = this.ToStringList();
            foreach (string str in list)
            {
                res += str + ",";
            }
            return res;
        }
    }
}
