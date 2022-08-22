using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Aplication.Generics
{
    public class AResponse<T>
    {
        public string Message { get; set; }
        public bool Successful { get; set; }
        public T Data { get; set; }
    }
}
