using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;


namespace ModelLibrary
{
    static class CSVProcessor
    {

        const string normalHeader = "\"id\",\"lat\",\"long\",\"depth\",\"mag\",\"stations\"";

        const int normalWidthSize = 6;


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


        public static void AppendToCSV(List<EarthQuake> table, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename, append: true))
            {
                writer.WriteLine(normalHeader);
                foreach (EarthQuake item in table)
                {
                    writer.WriteLine(item.ToString(CultureInfo.GetCultureInfo("en-US")));
                }
            }
        }
    }
}
