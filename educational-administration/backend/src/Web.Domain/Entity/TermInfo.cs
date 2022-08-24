using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.Entity
{
    public class TermInfo : Base.BaseEntity
    {
        //学期名
        public string TermName { get; set; } = null!;
    }
}