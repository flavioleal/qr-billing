using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.Common
{
    public class PaginationFilterInput
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Skip => (this.Page - 1) * this.PageSize;
        public string? SortField { get; set; }
        public string? SortOrder { get; set; }
    }
}
