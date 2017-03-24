using System.Collections.Generic;

namespace PayrollApp.Core.Data.System
{
    public class PagedData<T>
    {
        public List<T> Items { get; set; }
        public int Count { get; set; }
    }
}
