using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IQuestionnaireOptionServices
    {
        //新增选项
        Task<string> AddOption(ListOptionDTO listOptionDTO);

        //修改选项
        Task<string> UpdateOptione(Guid id, OptionDTO optionDto);

        //查询选项
        Task<string> GetbyId(Guid id);

        //删除选项
        Task<string> DeleteOption(Guid id);
    }
}