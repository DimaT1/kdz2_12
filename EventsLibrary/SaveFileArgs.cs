using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary
{
    public struct SaveFileArgs
    {
        public bool Append { get; set; }
        public string FileName { get; set; }

        public SaveFileArgs(bool append=false, string str=null)
        {
            Append = append;
            FileName = str;
        }
    }
}
