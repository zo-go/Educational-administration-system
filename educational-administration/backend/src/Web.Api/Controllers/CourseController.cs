using System;
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
    // 请求地址 http://localhost:5003/course
    public class CourseController
    {
        private readonly ICourseServices _course;
        private readonly IRepository<CourseInfo> _courseRes;
        public CourseController(ICourseServices course, IRepository<CourseInfo> courseRes)
        {
            _course = course;
            _courseRes = courseRes;
        }

        // get
        public string getUser([FromQuery] PageFromQuery query)
        {
            // get请求默认从uri从获取参数，所以此处应该加上FromQuery的特性才能正确获得参数
            var tmp = _course.GetListOrByCourseName(query);



            return tmp;
        }

        // get
        [Route("teacher/{teacherNumber}")]
        public string getCourseByTeacher(string teacherNumber)
        {

            var list = _courseRes.Table.Where(x => x.TeacherId == teacherNumber);

            return new
            {
                Code = 200,
                Msg = "获取课程数据成功",
                Data = list
            }.SerializeObject();
        }

        // post
        [HttpPost]
        public async Task<string> addUser(CourseDTO courseDto)
        {
            var tmp = await _course.AddCourse(courseDto);

            return tmp;
        }

        // put
        [HttpPut("{id}")]
        public async Task<string> updateUser(CourseDTO courseDto, Guid id)
        {
            var tmp = await _course.UpdateCourse(id, courseDto);

            return tmp;
        }

        // delete
        [HttpDelete("{id}")]
        public async Task<string> deleteUser(Guid id)
        {
            var tmp = await _course.DeleteCourse(id);

            return tmp;
        }
    }
}