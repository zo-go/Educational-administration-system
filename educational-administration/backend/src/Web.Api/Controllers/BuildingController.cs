using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.ResDto;
using Web.Application.Utils;

namespace Web.Api.Controllers
{
    [Route("[controller]")]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingServices _budingRes;
        public BuildingController(IBuildingServices budingRes)
        {
            _budingRes = budingRes;
        }


        [HttpGet]
        public string GetBuildingList([FromQuery] PageFromQuery query)
        {
            var tmp = _budingRes.GetListOrByBuildingName(query);

            return tmp;
        }

        [HttpPut("{id}")]
        public async Task<string> UpdateBuildingInfo(Guid id, [FromBody] BuildingDTO buildingDTO)
        {
            var BuildingInfo = await _budingRes.UpdatedBuilding(id, buildingDTO);
            return BuildingInfo;
        }

        [HttpPost]
        public async Task<string> AddBuilding([FromBody] BuildingDTO buildingDTO)
        {
            var buildingInfo = await _budingRes.AddBuilding(buildingDTO);
            return buildingInfo;
        }


        [HttpDelete("{id}")]
        public async Task<string> DeleteBuild(Guid id)
        {
            var buildingInfo = await _budingRes.DeleteBuilding(id);
            return buildingInfo;
        }
    }
}