using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IClassServices
    {
        //新增班级
        Task<string> AddClass(ClassDTO classDTO);


        //删除班级（指定id）

        Task<string> DeleteClass(Guid id);

        //获取班级列表或者（指定名称模糊查询）
        string GetListOrByClassName(PageFromQuery query);


        //修改班级名称（指定id）
        Task<string> UpdatedClass(Guid id, ClassDTO classDTO);
    }
}