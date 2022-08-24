using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcademyController : ControllerBase
    {
        private readonly IAcademyInfoServices _iacademy;

        public AcademyController(IAcademyInfoServices iacademy)
        {
            _iacademy = iacademy;
        }

        [HttpPost]
        public async Task<string> AddAcademy([FromBody] AcadeDTO AcadeDTO)
        {
            var academyinfo = await _iacademy.AddAcade(AcadeDTO);

            return academyinfo;
        }
        [HttpPut("{id}")]
        public async Task<string> UpdateAcademy(Guid id, [FromBody] AcadeDTO AcadeDTO)
        {
            var academyinfo = await _iacademy.UpdatedAcadeName(id, AcadeDTO);

            return academyinfo;
        }
        [HttpDelete("{id}")]
        public async Task<string> DeleteAcademy(Guid id)
        {
            var academyinfo = await _iacademy.DeleteAcade(id);

            return academyinfo;
        }

        [HttpGet]
        public string GetAcademyList([FromQuery] PageFromQuery query)
        {
            var academyinfo = _iacademy.GetListOrByAcadeName(query);

            return academyinfo;
        }
    }
}