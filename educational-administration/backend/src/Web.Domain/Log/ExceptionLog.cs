using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Log
{

    public class ExceptionLog : Base.BaseEntity
    {
        public string ShortMessage { get; set; } = null!;
        public string FullMessage { get; set; } = null!;
    }

}