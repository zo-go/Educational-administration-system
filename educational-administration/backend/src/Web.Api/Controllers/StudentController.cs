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
    [ApiController]
    [Route("[controller]")]
    // 请求地址 http://localhost:5003/student
    public class StudentController : ControllerBase
    {

        private readonly IStudentServices _student;
        public StudentController(IStudentServices student)
        {
            _student = student;
        }

        // get
        public string getStudent([FromQuery] PageFromQuery query)
        {
            var tmp = _student.GetListOrByStudentName(query);



            return tmp;
        }

        // post
        [HttpPost]
        public async Task<string> addStudent(StudentDTO student)
        {
            var tmp = await _student.AddStudent(student);

            return tmp;
        }

        // put
        [HttpPut("{id}")]
        public async Task<string> updateStudent(StudentDTO student, Guid id)
        {
            var tmp = await _student.UpdateStudent(id, student);

            return tmp;
        }

        // delete
        [HttpDelete("{id}")]
        public async Task<string> deleteStudent(Guid id)
        {
            var tmp = await _student.DeleteStudent(id);

            return tmp;
        }
    }
}