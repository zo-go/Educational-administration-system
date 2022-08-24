using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IBuildingServices
    {
        //新增建筑
        Task<string> AddBuilding(BuildingDTO buildingDTO);


        //删除建筑（指定id）
        Task<string> DeleteBuilding(Guid id);

        //获取建筑列表或者（指定名称模糊查询）
        string GetListOrByBuildingName(PageFromQuery query);


        //修改建筑名称
        Task<string> UpdatedBuilding(Guid id, BuildingDTO buildingDTO);
    }
}