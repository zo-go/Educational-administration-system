using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Common.Interface;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.ResDto;
using Web.Application.Utils;
using Web.Domain.Entity;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // 请求地址 http://localhost:5003/curriculum
    public class CurriCulumController : Controller
    {
        private readonly ICurriCulumServices _curriCulum;
        private readonly IRepository<CurriCulum> _curri;

        public CurriCulumController(ICurriCulumServices curriCulum, IRepository<CurriCulum> curri)
        {
            _curriCulum = curriCulum;
            _curri = curri;
        }

        [HttpPost]
        public async Task<string> AddCurriCulum(CurriCulumDTO curriCulumDTO)
        {
            var res = await _curriCulum.AddCurriCulum(curriCulumDTO);

            return res;
        }

        [HttpGet]
        public string GetCurriCulum([FromQuery] PageFromQuery query)
        {
            var res = _curriCulum.GetListOrByCurriCulumName(query);

            return res;
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteCurriCulum(Guid id)
        {
            var res = await _curriCulum.DeleteCurriCulum(id);

            return res;
        }

        [HttpPut("{id}")]
        public async Task<string> UpdateCurriCulum(Guid id, CurriCulumDTO curriCulumDTO)
        {
            var res = await _curriCulum.UpdatedCurriCulumName(id, curriCulumDTO);

            return res;
        }

        [HttpGet("{specializedName}")]
        public string GetCurriCulumBySpecializedName(string SpecializedName)
        {
            var res = _curriCulum.GetCurriCulumNameBySpecializedName(SpecializedName);

            return res;
        }
        // [HttpGet("class/{className}")]
        // public string GetCurriCulumByClassName(string ClassName)
        // {
        //     var tmp = _curri.Table.Where(x => x.SpecializedName == ClassName).ToList();

        //     return new
        //     {
        //         Code = 200,
        //         Msg = "获取数据成功",
        //         Data = tmp
        //     }.SerializeObject();
        // }
        [HttpGet("class/{className}")]
        public string GetCurriCulumByClassName(string ClassName)
        {
            var tmp = _curri.Table.Where(x => x.SpecializedName == ClassName).OrderBy(x=>x.CreatedAt);

            return new
            {
                Code = 200,
                Msg = "获取" + ClassName + "数据成功",
                Data = tmp
            }.SerializeObject();
        }

    }
}