using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.Mappings
{
    public class C : B
    {
        public int CProperty { get; set; }

        public C()
        {
            base.BMethod();
        }

        public override int BMethod()
        {
            return base.BMethod();
        }

        public override int AMethod()
        {
            return base.AMethod();
        }

        public virtual int CMethod()
        {
            return CProperty;
        }
    }
}
