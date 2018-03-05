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
        /// Определяет валидность введённых данных
        /// </summary>
        bool Valid { get; }
    }

    /// <summary>
    /// Интерфейс корректности
    /// </summary>
    interface ICorrect
    {
        /// <summary>
        /// Определяет корректность введённых данных
        /// </summary>
        bool Correct { get; }
    }
}
