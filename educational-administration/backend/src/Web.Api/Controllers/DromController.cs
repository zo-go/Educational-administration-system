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
using Web.Application.ResDto;
using Web.Application.Utils;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DromController : ControllerBase
    {
        private readonly IDormitoryServices _idormRes;

        public DromController(IDormitoryServices idormRes)
        {
            _idormRes = idormRes;
        }

        /// <summary>
        /// 添加宿舍
        /// </summary>
        /// <param name="dormitoryDTO">宿舍数据接受类</param>
        /// <returns></returns>
        [HttpPost]
        public Task<string> AddDrom([FromBody] DormitoryDTO dormitoryDTO)
        {
            var drominfo = _idormRes.AddDormitory(dormitoryDTO);
            return drominfo;
        }
        /// <summary>
        /// 查询宿舍列表 无关键字的
        /// </summary>
        /// <param name="dormitoryDTO"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetDromList([FromQuery] PageFromQuery query)
        {
            var tmp = _idormRes.GetListOrByDormitoryName(query);

            return tmp;
        }


        /// <summary>
        /// 更新宿舍信息
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<string> UpdateDromInfo(Guid id, [FromBody] DormitoryDTO dormitoryDTO)
        {
            var budinginfo = await _idormRes.UpdateDormitory(id, dormitoryDTO);

            return budinginfo;
        }

        /// <summary>
        /// 删除宿舍信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dormitoryDTO"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<string> DeleteDromInfo(Guid id)
        {
            var budinginfo = await _idormRes.DeleteDormitory(id);

            return budinginfo;
        }

        /// <summary>
        /// 未满人的宿舍
        /// </summary>
        /// <returns>宿舍号和人数</returns>
        [HttpGet("count/{buildingNum}")]
        public string GetDormitoryCount(string buildingNum)
        {
            var list = _idormRes.GetDormitoryCount(buildingNum);

            return list;
        }
    }
}