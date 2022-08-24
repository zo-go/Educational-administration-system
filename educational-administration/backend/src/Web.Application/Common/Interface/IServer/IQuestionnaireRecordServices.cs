using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IQuestionnaireRecordServices
    {
        //新增问卷模板信息
        Task<string> AddQuestionnaireRecord(ListRecordDto listRecordDto);

        //修改问卷模板信息(根据id)
        Task<string> UpdateQuestionnaireRecord(Guid id, QuestionnaireRecordDTO questionnaireRecordDTO);

        //查询问卷模板信息列表或者（指定名称模糊查询）
        string GetListOrByQuestionnaireRecordName(PageFromQuery query);

        //查询问卷模板信息（根据id）
        Task<string> GetbyId(Guid id);

        //删除问卷模板(根据id)
        Task<string> DeleteQuestionnaireRecord(Guid id);
    }
}