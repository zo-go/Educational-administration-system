using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Common.Interface;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto.ServerDto;
using Web.Application.Utils;
using Web.Domain.Entity;

namespace Web.Services.Services
{
    public class QuestionnaireOptionServices : IQuestionnaireOptionServices
    {
        private readonly IRepository<QuestionnaireOptions> _questOption;
        public QuestionnaireOptionServices(IRepository<QuestionnaireOptions> questOption)
        {
            _questOption = questOption;
        }
        public async Task<string> AddOption(ListOptionDTO listOptionDTO)
        {
            List<QuestionnaireOptions> list = new List<QuestionnaireOptions>();

            foreach (var item in listOptionDTO.optionDTOs)
            {
                var questOptionentity = await _questOption.AddAsync(new QuestionnaireOptions
                {
                    QuestionnaireId = item.QuestionnaireID,
                    QuestionnaireQuestionId = item.QuestionnaireQuestionId,
                    OptionName = item.OptionName
                });

                list.Add(questOptionentity);
            }


            return new
            {
                Code = 200,
                Msg = "添加问卷问题选项成功",
                Data = list,
            }.SerializeObject();
        }

        public async Task<string> DeleteOption(Guid id)
        {
            var entity = await _questOption.DeleteAsync(id);
            if (entity == null)
            {
                return new
                {
                    Code = 402,
                    Msg = "删除问卷问题选项失败",
                    Data = "",
                }.SerializeObject();
            }
            return new
            {
                Code = 200,
                Msg = "删除问卷问题选项成功",
                Data = entity,
            }.SerializeObject();
        }

        public async Task<string> GetbyId(Guid id)
        {
            var entity = await _questOption.GetByIdAsync(id);

            return new
            {
                Code = 200,
                Msg = "查询问卷问题选项成功",
                Data = entity,
            }.SerializeObject();
        }

        public async Task<string> UpdateOptione(Guid id, OptionDTO optionDTO)
        {
            var entity = await _questOption.GetByIdAsync(id);
            if (entity != null)
            {
                entity.OptionName = optionDTO.OptionName;
            }
            else if (entity == null)
            {
                return new
                {
                    Code = 402,
                    Msg = "修改问卷选项失败",
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