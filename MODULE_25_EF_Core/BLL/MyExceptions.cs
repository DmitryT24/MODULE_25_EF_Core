using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODULE25_EF.BLL
{
    public class MyExceptions : Exception
    {
        public MyExceptions() : base() { }
        public MyExceptions(string msg) : base(msg) { }
    }
}
