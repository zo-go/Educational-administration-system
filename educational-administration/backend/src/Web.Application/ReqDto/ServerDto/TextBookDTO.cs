using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto.ServerDto
{
    public class TextBookDTO
    {
        //教材名
        public string TextBookName { get; set; } = null!;
        //价格
        public string? Price { get; set; }
        //出版社
        public string Press { get; set; } = null!;
        //联系电话
        public string? ContactNumber { get; set; }
    }
}