using System.Collections.Generic;

namespace PayrollApp.Core.Data.Common
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> result, int totalRows, int totalPages)
        {
            Result = result;
            TotalRows = totalRows;
            TotalPages = totalPages;
        }
        public int TotalRows { get; set; }
        public IEnumerable<T> Result { get; set; }
        public int TotalPages { get; set; }
    }
}
