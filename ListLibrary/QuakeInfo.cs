using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    /// <summary>
    /// Класс информации о землетрясениях
    /// </summary>
    public class QuakeInfo
    {
        /// <summary>
        /// Список землетрясений
        /// </summary>
        private List<EarthQuake> quakes = new List<EarthQuake>();

        /// <summary>
        /// Свойство списка землетрясений
        /// </summary>
        public List<EarthQuake> Quakes {
            set { quakes = value; }
            get { return quakes; }
        }

        /// <summary>
        /// Конструктор объекта
        /// </summary>
        public QuakeInfo() { }

        /// <summary>
        /// Конструктор объекта с чтением списка из файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        public QuakeInfo(string filename)
        {
            quakes = CSVProcessor.ReadFromCSV(filename);
        }

        /// <summary>
        /// Возвращает отсортированный по количеству станций список землетрясений
        /// </summary>
        /// <returns>Отсортированный по количеству станций список землетрясений</returns>
        public List<EarthQuake> SortedByStations()
        {
            List<EarthQuake> res = quakes;
            res.Sort((q1, q2) => q1.Stations.CompareTo(q2.Stations));
            return res;
        }

        /// <summary>
        /// Возвращает отсортированный по магнитуре список землетрясений
        /// </summary>
        /// <returns>Отсортированный по магнитуре список землетрясений</returns>
        public List<EarthQuake> SortedByMag()
        {
            List<EarthQuake> res = quakes;
            res.Sort((q1, q2) => q1.Mag.CompareTo(q2.Mag));
            return res;
        }

        /// <summary>
        /// Поиск землетрясения, произошеднего на минимальной глубине.
        /// </summary>
        /// <returns></returns>
        public EarthQuake GetMinDepthQuake()
        {
            EarthQuake minDepthQuake = new EarthQuake();
            foreach (EarthQuake quake in quakes)
            {
                if (quake.Depth.Valid)
                {
                    if (!minDepthQuake.Depth.Valid || quake.Depth < minDepthQuake.Depth)
                    {
                        minDepthQuake = quake;
                    }
                }
            }
            return minDepthQuake;
        }

        /// <summary>
        /// Поиск землетрясения, произошеднего на максимальной глубине.
        /// </summary>
        /// <returns></returns>
        public EarthQuake GetMaxDepthQuake()
        {
            EarthQuake maxDepthQuake = new EarthQuake();
            foreach (EarthQuake quake in quakes)
            {
                if (quake.Depth.Valid)
                {
                    if (!maxDepthQuake.Depth.Valid || quake.Depth > maxDepthQuake.Depth)
                    {
                        maxDepthQuake = quake;
                    }
                }
            }
            return maxDepthQuake;
        }
    }
}
