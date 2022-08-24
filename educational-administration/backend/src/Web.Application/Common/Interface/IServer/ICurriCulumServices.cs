using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface ICurriCulumServices
    {
        //增加排课
        Task<string> AddCurriCulum(CurriCulumDTO curriCulumDTO);

        //删除排课(指定id)
        Task<string> DeleteCurriCulum(Guid id);

        //获取排课列表或者（指定名称模糊查询）
        string GetListOrByCurriCulumName(PageFromQuery query);

        //修改排课内容
        Task<string> UpdatedCurriCulumName(Guid id, CurriCulumDTO curriCulumDTO);

        // 根据关键字查询排课内容（通过班级搜索）
        string GetCurriCulumNameBySpecializedName(string SpecializedName);
    }
}