using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace ListLibrary
{
    public static class CSVProcessor
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
                        throw new ArgumentException($"file {filename} is invalid");
                    }
                }

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    List<string> values = line.Split(',').ToList();
                    if (values.Capacity != normalWidthSize)
                    {
                        throw new ArgumentException($"file {filename} is invalid");
                    }
                    res.Add(new EarthQuake(values));
                }
            }
            return res;
        }

        /// <summary>
        /// Метод сохраняет список table в файл filename
        /// </summary>
        /// <param name="table">Список для сохранения</param>
        /// <param name="filename">Файл для сохранения</param>
        static void WriteToCSV(List<EarthQuake> table, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(normalHeader);
                foreach (EarthQuake item in table)
                {
                    writer.WriteLine();
                }
            }
        }
    }
}
