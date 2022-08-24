using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class QuestionnaireServices : IQuestionnaireServices
    {
        private readonly IRepository<Questionnaire> _questRepository;
        private readonly IRepository<QuestionnaireRecord> _questRecord;
        private readonly IRepository<QuestionnaireRes> _questRes;
        private readonly IRepository<QuestionnaireOptions> _questOption;

        public QuestionnaireServices(IRepository<Questionnaire> questRepository, IRepository<QuestionnaireRecord> questRecord, IRepository<QuestionnaireRes> questRes, IRepository<QuestionnaireOptions> questOption)
        {
            _questRepository = questRepository;
            _questRecord = questRecord;
            _questOption = questOption;
            _questRes = questRes;

        }


        public async Task<string> AddQuestionnaire(QuestionnaireDTO questionnaireDTO)
        {
            var dateTime = DateTime.Now.Date;
            if (string.IsNullOrEmpty(questionnaireDTO.EndTime))
            {
                dateTime = dateTime.AddDays(1);
            }
            else
            {
                var dateString = questionnaireDTO.EndTime;
                var format = "yyyy-MM-dd HH:mm:ss";

                dateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            }
            var questentity = await _questRepository.AddAsync(new Questionnaire
            {
                QuestionnaireTheme = questionnaireDTO.QuestionnaireTheme,
                QuestionnaireTitle = questionnaireDTO.QuestionnaireTitle,
                EndTime = dateTime
            });
            return new
            {
                Code = 200,
                Msg = "添加问卷主题成功",
                Data = questentity,
            }.SerializeObject();

        }

        public async Task<string> DeleteQuestionnaire(Guid id)
        {
            var questEntity = _questRepository.Table.Where(x => x.Id == id);
            var recordEntity = _questRecord.Table.Where(x => x.QuestionnaireID == id);
            var optionEntity = _questOption.Table.Where(x => x.QuestionnaireId == id);
            if (questEntity != null)
            {
                await _questRepository.DeleteRangeAsync(questEntity, true);
                await _questRecord.DeleteRangeAsync(recordEntity, true);
                await _questOption.DeleteRangeAsync(optionEntity, true);
            }

            return new
            {
                Code = 200,
                Msg = "删除问卷主题及下所有问题与选项成功",
                Data = questEntity,
            }.SerializeObject();
        }

        public string GetbyId(Guid id)
        {

            var record = _questRecord.Table;
            var option = _questOption.Table;

            var entity = _questRepository.Table.Where(x => x.Id == id)
            .Join(record, x => x.Id, g => g.QuestionnaireID, (x, g) => new { x.Id, x.QuestionnaireTheme, x.EndTime, x.QuestionnaireTitle, RecordInfo = g })
            .Join(option, x => x.RecordInfo.Id, g => g.QuestionnaireQuestionId, (x, g) => new
            {
                x.Id,
                x.QuestionnaireTheme,
                x.QuestionnaireTitle,
                x.EndTime,
                x.RecordInfo.QuestionnaireOptionType,
                x.RecordInfo.QuestionnaireQuestion,
                g.OptionName,
                OptionId = g.Id,
                g.QuestionnaireQuestionId,
                g.CreatedAt,
                g.CreatedBy

            });


            return new
            {
                Code = 200,
                Msg = "获取问卷详情成功",
                Data = entity,
            }.SerializeObject();
        }

        public string GetListOrByQuestionnaireName(PageFromQuery query)
        {

            var questionnaire = _questRepository.Table.Where(x => x.IsDeleted == false);

            if (!string.IsNullOrEmpty(query.keyword))
            {
                questionnaire = questionnaire.Where(x => x.QuestionnaireTheme.Contains(query.keyword) || x.QuestionnaireTitle.Contains(query.keyword));
            }

            var list = questionnaire.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).OrderByDescending(x => x.CreatedAt);

            return new
            {
                Code = 200,
                Msg = "查询问卷主题成功",
                Data = list,
                Page = new PageDto
                {
                    pageIndex = query.PageIndex,
                    pageSize = query.PageSize,
                    OnThisPage = list.Count(),
                    Count = questionnaire.Count()
                }
            }.SerializeObject();

        }

        public async Task<string> UpdateQuestionnaire(Guid id, QuestionnaireDTO questionnaireDTO)
        {
            var entity = await _questRepository.GetByIdAsync(id);

            var dateString = questionnaireDTO.EndTime;
            var format = "dd/MM/yyyy HH:mm:ss";

            var dateTime = DateTime.ParseExact(dateString!, format, CultureInfo.InvariantCulture);

            if (entity != null)
            {
                entity.QuestionnaireTitle = questionnaireDTO.QuestionnaireTitle;
                entity.QuestionnaireTheme = questionnaireDTO.QuestionnaireTheme;
                entity.EndTime = dateTime;
            }

            return new
            {
                Code = 200,
                Msg = "修改问卷主题详情成功",
                Data = entity,
            }.SerializeObject();
        }

        public string noRecordInfo(PageFromQuery query)
        {
            var uid = Guid.Parse(query.keyword!);
            var resRecord = _questRes.Table.Where(x => x.CreatedBy == uid).ToList();
            var entity = _questRepository.Table.Where(x => x.IsDeleted == false).ToList();


            if (resRecord.Count() == 0)
            {
                var tmp = entity.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);
                return new
                {
                    Code = 200,
                    Msg = "查询未答 问卷主题成功",
                    Data = tmp,
                    Page = new PageDto
                    {
                        pageIndex = query.PageIndex,
                        pageSize = query.PageSize,
                        OnThisPage = tmp.Count(),
                        Count = entity.Count()
                    }
                }.SerializeObject();

            }
            List<Questionnaire> list = new List<Questionnaire>();

            var resinfo = resRecord.GroupBy(x => x.QuestionnaireId).Select((x, g) => new { key = x.Key });
            var questinfo = _questRepository.Table.Where(x => x.IsDeleted == false).ToList();
            for (int i = 0; i < questinfo.Count(); i++)
            {
                foreach (var item in resinfo)
                {

                    if (item.key == questinfo[i].Id)
                    {
                        list.Add(questinfo[i]);
                    }
                }

            }

            for (int k = 0; k < entity.Count(); k++)
            {
                foreach (var item in list)
                {
                    if (item.Id == entity[k].Id)
                    {
                        entity.Remove(entity[k]);
                    }
                }
            }
            if (entity.Count == 0)
            {
                return new
                {
                    Code = 200,
                    Msg = "您已完成所有问卷",
                    Data = entity
                }.SerializeObject();
            }
            return new
            {
                Code = 200,
                Msg = "查询未答 问卷主题成功",
                Data = entity,
                Page = new PageDto
                {
                    pageIndex = query.PageIndex,
                    pageSize = query.PageSize,
                    OnThisPage = entity.Count(),
                    Count = entity.Count()
                }
            }.SerializeObject();

        }

        public class RecordInfoDto
        {
            public Guid Id { get; set; }

        }
    }
}