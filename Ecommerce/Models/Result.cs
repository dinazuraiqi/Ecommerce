using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Result
    {
        public Boolean Success { get; set; }
        public string Error { get; set; }
        public Object ResultObject { get; set; }
    }
}
