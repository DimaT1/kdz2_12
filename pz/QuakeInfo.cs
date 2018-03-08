using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace ModelLibrary
{
    public class QuakeInfo : IValid, ICorrect
    {
        private List<EarthQuake> quakes = new List<EarthQuake>();
        public List<EarthQuake> Quakes {
            set { quakes = value; }
            get { return quakes; }
        }
        public bool Valid {
            get
            {
                bool res = true;
                for (int i = 0; i < quakes.Count && res; i++)
                {
                    res = res && quakes[i].Valid;
                }
                return res;
            }
        }
        public bool Correct
        {
            get
            {
                bool res = true;
                for (int i = 0; i < quakes.Count && res; i++)
                {
                    res = res && quakes[i].Correct;
                }
                return res;
            }
        }

        public QuakeInfo() {
            quakes = new List<EarthQuake>();
        }
        public QuakeInfo(string filename)
        {
            quakes = CSVProcessor.ReadFromCSV(filename);
        }
        public List<EarthQuake> SortedByID()
        {
            List<EarthQuake> res = quakes;
            res.Sort((q1, q2) => q1.Id.CompareTo(q2.Id));
            return res;
        }

        public List<EarthQuake> SortedByStations()
        {
            List<EarthQuake> res = quakes;
            res.Sort((q1, q2) => q1.Stations.CompareTo(q2.Stations));
            return res;
        }

        
        public List<EarthQuake> MaxListByMag(double minValue)
        {
            List<EarthQuake> res = new List<EarthQuake>();
            
            foreach (EarthQuake quake in quakes)
            {
                if (quake.Mag > minValue)
                {
                    res.Add(quake);
                }
            }
            return res;
        }


        public List<string> MinDepthQuake => getMinDepthQuake().GetList();

        public List<string> MaxDepthQuake => getMaxDepthQuake().GetList();

        private EarthQuake getMinDepthQuake()
        {
            EarthQuake minDepthQuake = new EarthQuake();
            foreach (EarthQuake quake in quakes)
            {
                if (quake.Correct)
                {
                    if (!minDepthQuake.Correct || quake.Depth < minDepthQuake.Depth)
                    {
                        minDepthQuake = quake;
                    }
                }
            }
            return minDepthQuake;
        }

        private EarthQuake getMaxDepthQuake()
        {
            EarthQuake maxDepthQuake = new EarthQuake();
            foreach (EarthQuake quake in quakes)
            {
                if (quake.Correct)
                {
                    if (!maxDepthQuake.Correct || quake.Depth > maxDepthQuake.Depth)
                    {
                        maxDepthQuake = quake;
                    }
                }
            }
            return maxDepthQuake;
        }

        public List<List<string>> GetList(CultureInfo cultureInfo=null)
        {
            List<List<string>> res = new List<List<string>>(); 
            foreach (EarthQuake quake in quakes)
            {
                res.Add(quake.GetList(cultureInfo));
            }
            return res;
        }

        public List<string> NewCell(string val, int columnIndex, int rowIndex, CultureInfo cultureInfo)
        {
            if (rowIndex >= quakes.Count)
            {
                EarthQuake earthQuake = new EarthQuake();
                earthQuake.SetElemFromStr(val, columnIndex, cultureInfo);
                quakes.Add(earthQuake);
                return earthQuake.GetList(cultureInfo);
            }
            else
            {
                quakes[rowIndex].SetElemFromStr(val, columnIndex, cultureInfo);
                return quakes[rowIndex].GetList(cultureInfo);
            }
        }

        public void SaveToFile(string filename, string mode)
        {
            switch (mode)
            {
                case "rewrite":
                    CSVProcessor.WriteToCSV(quakes, filename);
                    break;
                case "append":
                    CSVProcessor.AppendToCSV(quakes, filename);
                    break;
            }
        }

        public EarthQuake EarthQuake
        {
            get => default(EarthQuake);
            set
            {
            }
        }
    }
}
