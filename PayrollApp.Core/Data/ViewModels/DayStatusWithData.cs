using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.ViewModels
{
    public class DayStatusWithData
    {
        public List<DayStatus> DayStatusList { get; set; }
        public DateTime ConfirmedThrough { get; set; }
        public DateTime RollOverDate { get; set; }
        public bool IsToBeRolledOver { get; set; }
    }
}
