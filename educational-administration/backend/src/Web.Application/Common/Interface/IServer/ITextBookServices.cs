using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface ITextBookServices
    {
        //新增教材信息
        Task<string> AddTextBook(TextBookDTO textBookDTO);

        //修改教材信息(根据id)
        Task<string> UpdateTextBook(Guid id, TextBookDTO textBookDTO);

        //查询教材信息列表或者（指定名称模糊查询）

        string GetListOrByTextBookName(PageFromQuery query);

        //查询教材信息（根据id）
        Task<string> GetbyId(Guid id);

        //删除教材信息(根据id)
        Task<string> DeleteTextBook(Guid id);
    }
}