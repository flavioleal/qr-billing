using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.Common
{
    public class Pagination<T>
    {
        public Pagination()
        {
        }
        public Pagination(IEnumerable<T> list, int totalRecords )
        {
            TotalRecords = totalRecords;
            List = list;
        }

       

        public long TotalRecords { get; set; }
        public IEnumerable<T> List { get; set; }
    }
}
