using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLibrary
{
    public struct CellEventArgs
    {
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
        public string Content { get; set; }

        public CellEventArgs(int col, int row, string content)
        {
            ColumnIndex = col;
            RowIndex = row;
            Content = content;
        }
    }
}
