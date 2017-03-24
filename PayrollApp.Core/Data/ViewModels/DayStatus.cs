using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Core.Data.ViewModels
{
    public class DayStatus
    {
        public string Label { get; set; }

        public string Date { get; set; }

        public string ForeColor { get; set; }

        public string BackColor { get; set; }

        public string FontWeight { get; set; }

        public bool Visible { get; set; }

        public int Type { get; set; }

        public int Index { get; set; }

        public bool IsRoll { get; set; }

        public bool Enabled { get; set; }

        public string FontStyle { get; set; }

        public string Text { get; set; }

        public string Status { get; set; }

        public bool Click { get; set; }

        public bool IsStarShow { get; set; }
    }
}
