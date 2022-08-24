using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.ResDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IQuestionnaireServices
    {
        //新增问卷调查表记录信息
        Task<string> AddQuestionnaire(QuestionnaireDTO questionnaireDTO);

        //修改问卷调查表记录信息(根据id)
        Task<string> UpdateQuestionnaire(Guid id, QuestionnaireDTO questionnaireDTO);

        //查询问卷调查表记录信息列表或者（指定名称模糊查询）
        string GetListOrByQuestionnaireName(PageFromQuery query);

        //查询问卷调查表记录信息（根据id）
        string GetbyId(Guid id);

        //删除问卷调查表记录(根据id)
        Task<string> DeleteQuestionnaire(Guid id);
    }
}