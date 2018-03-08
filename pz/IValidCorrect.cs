using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    interface IValid
    {
        bool Valid { get; }
    }
    interface ICorrect
    {
        bool Correct { get; }
    }
}
