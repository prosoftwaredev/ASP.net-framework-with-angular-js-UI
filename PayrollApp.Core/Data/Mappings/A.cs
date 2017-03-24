using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.Mappings
{
    public class A
    {
        public int AProperty { get; set; }

        public A()
        {

        }

        public virtual int AMethod()
        {
            return AProperty;
        }
    }
}
