﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace ModelLibrary
{
    /// <summary>
    /// Класс информации о землетрясениях
    /// </summary>
    public class QuakeInfo : IValid, ICorrect
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
        /// Реализация интерфейса IValid
        /// </summary>
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

        /// <summary>
        /// Реализация интерфейса ICorrect
        /// </summary>
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

        /// <summary>
        /// Конструктор объекта
        /// </summary>
        public QuakeInfo() {
            quakes = new List<EarthQuake>();
        }

        /// <summary>
        /// Конструктор объекта с чтением списка из файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        public QuakeInfo(string filename)
        {
            quakes = CSVProcessor.ReadFromCSV(filename);
        }

        /// <summary>
        /// Возвращает отсортированный по id список землетрясений
        /// </summary>
        /// <returns>Отсортированный по количеству станций список землетрясений</returns>
        public List<EarthQuake> SortedByID()
        {
            List<EarthQuake> res = quakes;
            res.Sort((q1, q2) => q1.Id.CompareTo(q2.Id));
            return res;
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
        /// Возвращает отсортированный по магнитуре список максимальных землетрясений
        /// </summary>
        /// <param name="maxShift">Максимальное отклонение в баллах по шкале Рихтера</param>
        /// <param name="minValue">Минимальное значение по шкале Рихтера</param>
        /// <param name="itemCount">Максимальное количество элементов в списке</param>
        /// <returns>Отсортированный по магнитуре список максимальных землетрясений</returns>
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

        /// <summary>
        /// Информация о землетрясении на минимальной глубине
        /// </summary>
        public List<string> MinDepthQuake => getMinDepthQuake().GetList();
        /// <summary>
        /// Информация о землетрясении на максимальной глубине
        /// </summary>
        public List<string> MaxDepthQuake => getMaxDepthQuake().GetList();

        /// <summary>
        /// Поиск землетрясения, произошеднего на минимальной глубине.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Поиск землетрясения, произошеднего на максимальной глубине.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Получить представление в виде списка списков строк
        /// </summary>
        /// <param name="cultureInfo">Локаль языка</param>
        /// <returns>Список</returns>
        public List<List<string>> GetList(CultureInfo cultureInfo=null)
        {
            List<List<string>> res = new List<List<string>>(); 
            foreach (EarthQuake quake in quakes)
            {
                res.Add(quake.GetList(cultureInfo));
            }
            return res;
        }

        /// <summary>
        /// Установка нового значения ячейки и возвращение нового списка строк строки таблицы
        /// </summary>
        /// <param name="val">Новое значение</param>
        /// <param name="columnIndex">Индекс столбца</param>
        /// <param name="rowIndex">Индекс строки</param>
        /// <param name="cultureInfo">Локаль языка</param>
        /// <returns></returns>
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

        /// <summary>
        /// Запись в файл
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <param name="mode">Запись/Дозапсиь</param>
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
