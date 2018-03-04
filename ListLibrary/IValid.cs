using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    /// <summary>
    /// Интерфейс валидности
    /// </summary>
    interface IValid
    {
        /// <summary>
        /// Определяет валидность
        /// </summary>
        bool Valid { get; }
    }
}
