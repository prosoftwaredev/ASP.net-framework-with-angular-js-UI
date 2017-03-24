using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.Mappings
{
    public class B : A
    {
        public int BProperty { get; set; }

        public B()
        {
            base.AMethod();
        }

        public virtual int BMethod()
        {
            return BProperty;
        }

        public override int AMethod()
        {
            return base.AMethod();
        }
    }
}
