using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface ISpecializedServices
    {
        //新增专业 信息
        Task<string> AddSpecialized(SpecializedDTO specializedDTO);

        //修改专业 信息(根据id)
        Task<string> UpdateSpecialized(Guid id, SpecializedDTO specializedDTO);

        //查询专业 信息列表或者（指定名称模糊查询）
        string GetListOrBySpecializedName(PageFromQuery query);

        //查询专业 信息（根据id）
        Task<string> GetbyId(Guid id);

        //删除专业 (根据id)
        Task<string> DeleteSpecialized(Guid id);
    }
}