using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListLibrary;

namespace KDZ_2_12_
{
    static class Jarvis
    {
        public static void FileReader(string filename)
        {
            CSVProcessor.ReadFromCSV(filename);
        }
    }
}
