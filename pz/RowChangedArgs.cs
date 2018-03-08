using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary
{
    public struct RowChangedArgs
    {
        public List<string> Row { get; set; }
        public int RowIndex { get; set; }

        public RowChangedArgs(int row, List<string> content)
        {
            RowIndex = row;
            Row = content;
        }
    }
}
