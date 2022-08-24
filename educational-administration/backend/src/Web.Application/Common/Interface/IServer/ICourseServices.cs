using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface ICourseServices
    {
        //新增课程
        Task<string> AddCourse(CourseDTO courseDTO);

        //修改课程(根据id)
        Task<string> UpdateCourse(Guid id, CourseDTO courseDTO);

        //查询课程列表或者（指定名称模糊查询）
        string GetListOrByCourseName(PageFromQuery query);

        //查询课程（根据id）
        Task<string> GetbyId(Guid id);

        //删除课程(根据id)
        Task<string> DeleteCourse(Guid id);
    }
}