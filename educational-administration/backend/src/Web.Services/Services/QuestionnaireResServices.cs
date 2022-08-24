using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Common.Interface;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.Utils;
using Web.Domain.Entity;

namespace Web.Services.Services
{
    public class QuestionnaireResServices : IQuestionnaireResServices
    {
        private readonly IRepository<Questionnaire> _questRepository;
        private readonly IRepository<QuestionnaireRecord> _questRecord;
        private readonly IRepository<QuestionnaireRes> _questRes;
        private readonly IRepository<QuestionnaireOptions> _questOption;
        private readonly IRepository<StudentInfo> _studentRes;
        private readonly IRepository<AppUser> _userRes;
        private readonly ISessionUserService _sessionUserService;

        public QuestionnaireResServices(IRepository<AppUser> userRes, ISessionUserService sessionUserService, IRepository<StudentInfo> studentRes, IRepository<Questionnaire> questRepository, IRepository<QuestionnaireRecord> questRecord, IRepository<QuestionnaireRes> questRes, IRepository<QuestionnaireOptions> questOption)
        {
            _questRes = questRes;
            _questRepository = questRepository;
            _questRecord = questRecord;
            _questOption = questOption;
            _studentRes = studentRes;
            _userRes = userRes;
            _sessionUserService = sessionUserService;
        }
        public async Task<string> AddQuestionnaireRes(ResListDto ResDTO)
        {

            List<QuestionnaireRes> list = new List<QuestionnaireRes>();
            foreach (var item in ResDTO.resDtoList)
            {
                var entity = await _questRes.AddAsync(new QuestionnaireRes
                {
                    QuestionnaireId = item.QuestionnaireId,
                    QuestionnaireQuestionId = item.QuestionnaireQuestionId,
                    OptionId = item.OptionId ?? new Guid(),
                    OptionValue = item.OptionValue!
                });
                list.Add(entity);
            }


            return new
            {
                Code = 200,
                Msg = "添加问卷答题记录成功",
                Data = list,
            }.SerializeObject();
        }

        public async Task<string> DeleteQuestionnaireRes(Guid id)
        {
            var entity = await _questRes.DeleteAsync(id);

            return new
            {
                Code = 200,
                Msg = "删除问卷答题记录成功",
                Data = entity,
            }.SerializeObject();
        }

        public string GetbyIdFullInfo(KeyResDto keyResDto)
        {
            var id = keyResDto.QuestionnaireId;
            var created = keyResDto.CreatedBy;

            var questRes = _questRes.Table.Where(x => x.CreatedBy == created && x.QuestionnaireId == id);
            var record = _questRecord.Table.Where(x => x.IsDeleted == false);
            var option = _questOption.Table.Where(x => x.IsDeleted == false);
            var student = _studentRes.Table.Where(x => x.IsDeleted == false);
            var user = _userRes.Table.Where(x => x.IsDeleted == false);

            var quest = _questRepository.Table.Where(x => x.IsDeleted == false)
                    .Join(record, x => x.Id, g => g.QuestionnaireID, (x, g) => new { quest = x, record = g })
                    .Join(option, x => x.record.Id, g => g.QuestionnaireQuestionId, (x, g) => new
                    {
                        QuestionnaireId = x.quest.Id,
                        x.quest.QuestionnaireTheme,
                        x.quest.QuestionnaireTitle,
                        x.quest.EndTime,
                        x.record.QuestionnaireQuestion,
                        x.record.QuestionnaireOptionType,
                        OptionId = g.Id,
                        Question = x.record.QuestionnaireQuestion,
                        g.OptionName
                    }).Join(questRes, x => x.OptionId, g => g.OptionId, (x, g) => new
                    {
                        x.QuestionnaireId,
                        x.QuestionnaireTheme,
                        x.QuestionnaireTitle,
                        x.Question,
                        x.EndTime,
                        x.OptionName,
                        x.QuestionnaireOptionType,
                        g.CreatedBy,
                        g.OptionValue
                    }).Join(user, x => x.CreatedBy, g => g.Id, (x, g) => new
                    {
                        x.QuestionnaireId,
                        x.Question,
                        x.QuestionnaireTheme,
                        x.QuestionnaireTitle,
                        x.EndTime,
                        x.OptionName,
                        x.OptionValue,
                        x.QuestionnaireOptionType,
                        x.CreatedBy,
                        g.UserName
                    }).Join(student, x => x.UserName, g => g.StudentId, (x, g) => new
                    {
                        x.QuestionnaireId,
                        x.QuestionnaireTheme,
                        x.QuestionnaireTitle,
                        x.EndTime,
                        x.Question,
                        x.OptionName,
                        x.CreatedBy,
                        x.QuestionnaireOptionType,
                        x.OptionValue,
                        g.StudentName
                    });

            var t = quest.GroupBy(x => x.QuestionnaireId, (x, g) => new
            {
                key = x,
                list = g.Select(x => new
                {
                    x.QuestionnaireTheme,
                    x.QuestionnaireTitle,
                    x.EndTime,
                    x.Question,
                    x.OptionName,
                    x.OptionValue,
                    x.StudentName,
                    x.QuestionnaireOptionType
                }).ToList()
            });
            return new
            {
                Code = 200,
                Msg = "查询问卷答题记录详情成功",
                Data = t
            }.SerializeObject();
        }

        public string GetListOrByQuestionnaireResName(PageFromQuery query)
        {
            var questRes = _questRes.Table.Where(x => x.IsDeleted == false);
            var record = _questRecord.Table.Where(x => x.IsDeleted == false);
            var option = _questOption.Table.Where(x => x.IsDeleted == false);
            var student = _studentRes.Table.Where(x => x.IsDeleted == false);
            var user = _userRes.Table.Where(x => x.IsDeleted == false);

            var quest = _questRepository.Table.Where(x => x.IsDeleted == false)
            .Join(record, x => x.Id, g => g.QuestionnaireID, (x, g) => new { quest = x, record = g })
            .Join(option, x => x.record.Id, g => g.QuestionnaireQuestionId, (x, g) => new
            {
                QuestionnaireId = x.quest.Id,
                x.quest.QuestionnaireTheme,
                x.quest.QuestionnaireTitle,
                x.quest.EndTime,
                x.record.QuestionnaireQuestion,
                OptionId = g.Id,
                g.OptionName
            }).Join(questRes, x => x.OptionId, g => g.OptionId, (x, g) => new
            {
                x.QuestionnaireId,
                x.QuestionnaireTheme,
                x.QuestionnaireTitle,
                x.EndTime,
                x.OptionName,
                g.CreatedBy,
                g.OptionValue
            }).Join(user, x => x.CreatedBy, g => g.Id, (x, g) => new
            {
                x.QuestionnaireId,
                x.QuestionnaireTheme,
                x.QuestionnaireTitle,
                x.EndTime,
                x.OptionName,
                x.OptionValue,
                x.CreatedBy,
                g.UserName
            }).Join(student, x => x.UserName, g => g.StudentId, (x, g) => new
            {
                x.QuestionnaireId,
                x.QuestionnaireTheme,
                x.QuestionnaireTitle,
                x.EndTime,
                x.OptionName,
                x.CreatedBy,
                x.OptionValue,
                g.StudentName
            });

            var t = quest.GroupBy(x => x.QuestionnaireId, (x, g) => new
            {
                key = x,
                list = g.ToList().FirstOrDefault()
            });
            return new
            {
                Code = 200,
                Msg = "查询问卷答题记录详情成功",
                Data = t
            }.SerializeObject();

        }


    }
}