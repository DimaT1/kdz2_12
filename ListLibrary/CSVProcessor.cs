using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ListLibrary
{
    static class CSVProcessor
    {
        /// <summary>
        /// Метод возвращает список строк таблицы, прочитанный из CSV-файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns>Список строк таблицы</returns>
        static List<ListItem> ReadFromCSV(string filename)
        {
            List<ListItem> res = new List<ListItem>();

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    List<string> values = line.Split(',').ToList();
                    res.Add(new ListItem(values));
                }
            }
            return res;
        }


    }
}
