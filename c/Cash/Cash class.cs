using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsDataStructures
{
    class NativeCache<T>
    {
        public int size;
        public String[] slots;
        public T[] values;
        public int[] hits;
        // ...
    }
}
