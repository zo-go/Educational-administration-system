using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IIntentionServices
    {
        //新增学生班级关联信息
        Task<string> AddIntention(IntentionDTO intentionDTO);

        //修改学生班级关联信息(根据id)
        Task<string> UpdateIntention(Guid id, IntentionDTO intentionDTO);

        //查询学生班级关联信息列表或者（指定名称模糊查询）
        string GetListOrByIntentionName(PageFromQuery query);

        //查询学生班级关联信息（根据id）
        Task<string> GetbyId(Guid id);

        //删除学生班级关联(根据id)
        Task<string> DeleteIntention(Guid id);
    }
}