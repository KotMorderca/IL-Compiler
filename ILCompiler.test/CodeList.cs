using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILCompiler.test
{
    class CodeList
    {
        public List<string> linia;
        public ulong excepted;
        public CodeList(string s1, string s2, ulong exc)
        {
            linia = new List<string>();
            linia.Add(s1);
            linia.Add(s2);
            excepted = exc;
        }
    }
}
