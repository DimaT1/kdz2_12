using System.Collections.Generic;

namespace ListLibrary
{
    public class EarthQuake
    {
        private QuakeItem<int> id = new QuakeItem<int>();
        private Coordinates coordinates;
        private QuakeItem<double> depth = new QuakeItem<double>();
        private QuakeItem<double> mag = new QuakeItem<double>();
        private QuakeItem<int> stations = new QuakeItem<int>();

        public EarthQuake(List<string> dataList)
        {
            id.SetFromStr(dataList[0]);
            coordinates = new Coordinates(dataList[1], dataList[2]);
            depth.SetFromStr(dataList[3]);
            mag.SetFromStr(dataList[4]);
            stations.SetFromStr(dataList[5]);
        }

        public List<string> ToStringList()
        {
            List<string> res = new List<string>();

            res.Add(id.ToString());

            res.Add(coordinates.Lat.ToString());
            res.Add(coordinates.Long.ToString());

            res.Add(depth.ToString());
            res.Add(depth.ToString());
            res.Add(stations.ToString());

            return res;
        }

        // TODO
        public override string ToString()
        {
            return $"\"{id}\",{coordinates.Lat},{coordinates.Long},{depth},{mag},{stations}";
        }
    }
}
