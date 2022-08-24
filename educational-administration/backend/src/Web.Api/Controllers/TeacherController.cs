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
    public class TeacherController : ControllerBase
    {

        private readonly ITeacherServices _iTeacherRes;

        public TeacherController(ITeacherServices iTeacherRes)
        {
            _iTeacherRes = iTeacherRes;
        }

        /// <summary>
        /// 查询 教师列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetTeacherList([FromQuery] PageFromQuery query)
        {
            var tmp = _iTeacherRes.GetListOrByTeacherName(query);

            return tmp;
        }
        [HttpGet("/teacher/key")]
        public string GetTeacherName([FromQuery] PageFromQuery query)
        {
            var tmp = _iTeacherRes.GetTeacherName(query);

            return tmp;
        }

        public string GetTeacherKeyList([FromQuery] PageFromQuery query)
        {
            var tmp = _iTeacherRes.GetTeacherName(query);

            return tmp;
        }

        // /// <summary>
        // /// 更新教师
        // /// </summary>
        // /// <returns></returns>
        [HttpPut("{id}")]
        public Task<string> UpdateTeacherInfo(Guid id, [FromBody] TeacherDTO teacherDTO)
        {
            var entity = _iTeacherRes.UpdateTeacher(id, teacherDTO);
            return entity;
        }
        // /// <summary>
        // /// 添加教师
        // /// </summary>
        // /// <returns></returns>
        [HttpPost]
        public Task<string> AddTeacher([FromBody] TeacherDTO teacherDTO)
        {
            var entity = _iTeacherRes.AddTeacher(teacherDTO);
            return entity;
        }

        /// <summary>
        /// 删除教师
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<string> DeleteTeacher(Guid id)
        {
            var entity = await _iTeacherRes.DeleteTeacher(id);

            return entity;
        }
    }
}