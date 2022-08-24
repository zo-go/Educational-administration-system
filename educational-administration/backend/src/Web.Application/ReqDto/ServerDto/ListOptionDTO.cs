using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto.ServerDto
{
    public class ListOptionDTO
    {
        public List<OptionDTO> optionDTOs { get; set; } = new List<OptionDTO>();
    }
}