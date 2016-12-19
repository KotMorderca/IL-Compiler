using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILCompiler
{
    public class MemoryAddress
    {
        public byte address = 0;
        public string name = null;
        public MemoryAddress(string nam, byte add)
        {
            name = nam;
            address = add;
        }
    }
}
