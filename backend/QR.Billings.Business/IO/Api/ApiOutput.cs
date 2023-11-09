using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.Api
{
    public class ApiOutput<T>
    {
        public ApiOutput(T data, List<string> messages = null)
        {
            Data = data;
            Messages = messages;
        }

        public T Data { get; set; }
        public List<string> Messages { get; set; }
    }
}
