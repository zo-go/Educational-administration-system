using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IStudentServices
    {
        //新增学生信息
        Task<string> AddStudent(StudentDTO studentDTO);

        //修改学生信息(根据id)
        Task<string> UpdateStudent(Guid id, StudentDTO studentDTO);

        //查询学生信息列表或者（指定名称模糊查询）

        string GetListOrByStudentName(PageFromQuery query);

        //查询学生信息（根据id）
        Task<string> GetbyId(Guid id);

        //删除学生信息(根据id)
        Task<string> DeleteStudent(Guid id);
    }
}