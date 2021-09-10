using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCatalogAvalonia.Models
{
    /// <summary>
    /// This interface allows you to clone an object without explicitly casting
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface ICloneable<T>
    {
        public T Clone();
    }
}
