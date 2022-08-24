using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Common.Interface;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.ResDto;
using Web.Application.Utils;
using Web.Domain.Entity;

namespace Web.Services.Services
{
    public class QuestionnaireRecordServices : IQuestionnaireRecordServices
    {
        private readonly IRepository<QuestionnaireRecord> _questRecord;
        public QuestionnaireRecordServices(IRepository<QuestionnaireRecord> questRecord)
        {
            _questRecord = questRecord;
        }
        public async Task<string> AddQuestionnaireRecord(ListRecordDto listRecordDto)
        {
            List<QuestionnaireRecord> list = new List<QuestionnaireRecord>();
            foreach (var questTO in listRecordDto.RecordDTOs)
            {
                var questRecordentity = await _questRecord.AddAsync(new QuestionnaireRecord
                {
                    QuestionnaireID = listRecordDto.QuestionnaireID,                       //关联问题主题表ID
                    QuestionnaireQuestion = questTO.QuestionnaireQuestion,                //问卷问题
                    OptionDescribe = questTO.OptionDescribe,                              //问题描述
                    QuestionnaireOptionType = questTO.QuestionnaireOptionType,            //问题选项类型
                    QuestionnaireFlag = questTO.QuestionnaireFlag                         //是否必填
                });
                list.Add(questRecordentity);
            }



            return new
            {
                Code = 200,
                Msg = "添加问卷问题成功",
                Data = list,
            }.SerializeObject();
        }

        public async Task<string> DeleteQuestionnaireRecord(Guid id)
        {
            var entity = await _questRecord.DeleteAsync(id);
            if (entity == null)
            {
                return new
                {
                    Code = 200,
                    Msg = "删除问卷问题失败 查询不到记录",
                    Data = "",
                }.SerializeObject();
            }
            return new
            {
                Code = 200,
                Msg = "删除问卷问题成功",
                Data = entity,
            }.SerializeObject();
        }

        public async Task<string> GetbyId(Guid id)
        {
            var entity = await _questRecord.GetByIdAsync(id);

            return new
            {
                Code = 200,
                Msg = "查询问卷问题详情成功",
                Data = entity,
            }.SerializeObject();
        }

        public string GetListOrByQuestionnaireRecordName(PageFromQuery query)
        {
            var entity = _questRecord.Table.Where(x => x.IsDeleted == false);

            if (!string.IsNullOrEmpty(query.keyword))
            {
                entity = entity.Where(x => x.QuestionnaireQuestion.Contains(query.keyword!));
            }
            var paging = entity.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();

            return new
            {
                Code = 200,
                Msg = "查询问卷问题列表 成功",
                Data = entity,
                Page = new PageDto
                {
                    pageIndex = query.PageIndex,
                    pageSize = query.PageSize,
                    OnThisPage = paging.Count(),
                    Count = entity.Count()
                }
            }.SerializeObject();
        }

        public async Task<string> UpdateQuestionnaireRecord(Guid id, QuestionnaireRecordDTO RecordDTO)
        {
            var entity = await _questRecord.GetByIdAsync(id);
            if (entity != null)
            {
                entity.QuestionnaireQuestion = RecordDTO.QuestionnaireQuestion;
                entity.OptionDescribe = RecordDTO.OptionDescribe;
                entity.QuestionnaireOptionType = RecordDTO.QuestionnaireOptionType;
                entity.QuestionnaireFlag = RecordDTO.QuestionnaireFlag;
                entity = await _questRecord.UpdateAsync(entity);

            }
            else if (entity == null)
            {
                return new
                {
                    Code = 402,
                    Msg = "修改问卷问题失败 没有此问题",
                    Data = "",
                }.SerializeObject();
            }
            return new
            {
                Code = 200,
                Msg = "修改问卷问题成功",
                Data = entity,
            }.SerializeObject();
        }
    }
}