using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IAcademyInfoServices
    {
        //增加学院
        Task<string> AddAcade(AcadeDTO AcadeDTO);

        //删除学院(指定id)
        Task<string> DeleteAcade(Guid id);

        //获取学院列表或者（指定名称模糊查询）
        //不需要多次拼接的表 返回string
        string GetListOrByAcadeName(PageFromQuery query);

        //修改学院名称
        Task<string> UpdatedAcadeName(Guid id, AcadeDTO AcadeDTO);
    }
}