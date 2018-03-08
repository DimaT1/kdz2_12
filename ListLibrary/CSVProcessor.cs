using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;


namespace ModelLibrary
{
    static class CSVProcessor
    {
        /// <summary>
        /// Заголовок файла с данными
        /// </summary>
        const string normalHeader = "\"id\",\"lat\",\"long\",\"depth\",\"mag\",\"stations\"";
        /// <summary>
        /// Количество параметров в каждой строке файла
        /// </summary>
        const int normalWidthSize = 6;

        /// <summary>
        /// Метод возвращает список строк таблицы, прочитанный из CSV-файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns>Список строк таблицы</returns>
        public static List<EarthQuake> ReadFromCSV(string filename)
        {
            List<EarthQuake> res = new List<EarthQuake>();

            using (StreamReader reader = new StreamReader(filename))
            {
                if(!reader.EndOfStream)
                {
                    string headerLine = reader.ReadLine();
                    if (headerLine != normalHeader)
                    {
                        throw new ArgumentException($"file {filename} is invalid", "file");
                    }
                }

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    List<string> values = line.Split(',').ToList();
                    if (values.Capacity != normalWidthSize)
                    {
                        throw new ArgumentException($"file {filename} is invalid", "file");
                    }
                    res.Add(new EarthQuake(values, CultureInfo.GetCultureInfo("en-US")));
                }
            }
            return res;
        }

        /// <summary>
        /// Метод сохраняет список table в файл filename
        /// </summary>
        /// <param name="table">Список для сохранения</param>
        /// <param name="filename">Файл для сохранения</param>
        public static void WriteToCSV(List<EarthQuake> table, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(normalHeader);
                foreach (EarthQuake item in table)
                {
                    writer.WriteLine(item.ToString(CultureInfo.GetCultureInfo("en-US")));
                }
            }
        }

        /// <summary>
        /// Метод сохраняет список table в файл filename
        /// </summary>
        /// <param name="table">Список для сохранения</param>
        /// <param name="filename">Файл для сохранения</param>
        public static void AppendToCSV(List<EarthQuake> table, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename, append: true))
            {
                foreach (EarthQuake item in table)
                {
                    writer.WriteLine(item.ToString(CultureInfo.GetCultureInfo("en-US")));
                }
            }
        }
    }
}
