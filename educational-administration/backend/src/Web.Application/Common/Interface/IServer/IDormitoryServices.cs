using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IDormitoryServices
    {
        //新增宿舍信息
        Task<string> AddDormitory(DormitoryDTO dormitoryDTO);

        //修改宿舍信息(根据id)
        Task<string> UpdateDormitory(Guid id, DormitoryDTO dormitoryDTO);

        //查询宿舍信息列表或者（指定名称模糊查询）
        string GetListOrByDormitoryName(PageFromQuery query);
        string GetDormitoryCount(string buildingNum);

        //查询宿舍信息（根据id）
        Task<string> GetbyId(Guid id);

        //删除宿舍(根据id)
        Task<string> DeleteDormitory(Guid id);
    }
}