using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface ITeacherServices
    {
        //新增教师信息
        Task<string> AddTeacher(TeacherDTO teacherDTO);

        //修改教师信息(根据id)
        Task<string> UpdateTeacher(Guid id, TeacherDTO teacherDTO);

        //查询教师信息列表或者（指定名称模糊查询）
        string GetListOrByTeacherName(PageFromQuery query);

        //查询教师信息（根据id）
        Task<string> GetbyId(Guid id);

        //删除教师信息(根据id)
        Task<string> DeleteTeacher(Guid id);

        //学院号对应教师
        string GetTeacherName(PageFromQuery query);
    }
}