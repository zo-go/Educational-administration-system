using Microsoft.AspNetCore.Mvc;

using Web.Application.ReqDto;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto.ServerDto;
using Web.Application.ResDto;
using Web.Domain.Entity;
using System.Linq;
using Web.Application.Utils;
using Web.Application.Common.Interface;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // 请求地址 http://localhost:5003/class
    public class ClassController : ControllerBase
    {
        private readonly IClassServices _classes;
        private readonly IRepository<ClassInfo> _class;
        public ClassController(IRepository<ClassInfo> Class, IClassServices classes)
        {
            _classes = classes;
            _class = Class;
        }

        // get
        public string getClass([FromQuery] PageFromQuery query)
        {
            var tmp = _classes.GetListOrByClassName(query);

            // 分页
            var list = tmp.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();

            return tmp;
        }

        // get
        // 请求地址 http://localhost:5003/class/workNumber/{workNumber}
        [Route("workNumber/{workNumber}")]
        public string getClassByWorkNumber(string workNumber)
        {

            // 分页
            var list = _class.Table.Where(x => x.CounselorId == workNumber).ToList();

            return new
            {
                Code = 200,
                Msg = "获取数据成功",
                Data = list
            }.SerializeObject();
        }

        // post
        [HttpPost]
        public async Task<string> addClass(ClassDTO classDto)
        {
            var tmp = await _classes.AddClass(classDto);

            return tmp;
        }

        // put
        [HttpPut("{id}")]
        public async Task<string> updateClass(ClassDTO classDto, Guid id)
        {
            var tmp = await _classes.UpdatedClass(id, classDto);

            return tmp;
        }

        // delete
        [HttpDelete("{id}")]
        public async Task<string> deleteClass(Guid id)
        {
            var tmp = await _classes.DeleteClass(id);

            return tmp;
        }
    }
}