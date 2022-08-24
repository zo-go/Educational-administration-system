using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IQuestionnaireResServices
    {
        //新增问卷调查接受 信息
        Task<string> AddQuestionnaireRes(ResListDto resListDto);


        //查询问卷调查接受 信息列表或者（指定名称模糊查询）
        string GetListOrByQuestionnaireResName(PageFromQuery query);

        //查询问卷调查接受 信息（根据id）
        string GetbyIdFullInfo(KeyResDto keyResDto);

        //删除问卷调查接受 (根据id)
        Task<string> DeleteQuestionnaireRes(Guid id);
    }
}