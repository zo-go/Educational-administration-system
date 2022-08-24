using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.ResDto;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQuestionnaireServices _iQuestionnaire;
        private readonly IQuestionnaireRecordServices _iRecord;
        private readonly IQuestionnaireOptionServices _iOption;

        public QuestionnaireController(IQuestionnaireServices iQuestionnaire, IQuestionnaireRecordServices iRecord, IQuestionnaireOptionServices iOption)
        {
            _iQuestionnaire = iQuestionnaire;
            _iRecord = iRecord;
            _iOption = iOption;
        }

        /// <summary>
        /// 新增 问卷调查主题
        /// </summary>
        /// <param name="questionnaireDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<string> AddQuestionnaire([FromBody] QuestionnaireDTO questionnaireDTO)
        {
            var entity = _iQuestionnaire.AddQuestionnaire(questionnaireDTO);

            return entity;
        }

        /// <summary>
        /// 删除问卷调查主题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<string> DeletedQuestionnaire(Guid id)
        {
            var entity = _iQuestionnaire.DeleteQuestionnaire(id);

            return entity;
        }

        /// <summary>
        /// 更新问卷调查主题
        /// </summary>
        /// <param name="id"></param>
        /// <param name="questionnaireDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task<string> UpdateQuestionnaire(Guid id, [FromBody] QuestionnaireDTO questionnaireDTO)
        {
            var entity = _iQuestionnaire.UpdateQuestionnaire(id, questionnaireDTO);
            return entity;
        }

        /// <summary>
        /// 查询创建完成后的问卷主题信息
        /// </summary>
        /// <param name="pageDto"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetQuestionnaireList([FromQuery] PageFromQuery pageDto)
        {
            var entity = _iQuestionnaire.GetListOrByQuestionnaireName(pageDto);
            return entity;
        }

        /// <summary>
        /// 获取问卷详情
        /// </summary>
        /// <param name="pageDto"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string GetFullInfo(Guid id)
        {
            var entity = _iQuestionnaire.GetbyId(id);

            return entity;
        }




        /// <summary>
        /// 新增 问卷问题
        /// </summary>
        /// <param name="questionnaireDTO"></param>
        /// <returns></returns>
        [HttpPost("record")]
        public async Task<string> AddQuestionnaireRecord([FromBody] ListRecordDto listRecordDto)
        {
            var entity = await _iRecord.AddQuestionnaireRecord(listRecordDto);

            return entity;
        }
        //更新问卷问题
        [HttpPut("record/{id}")]
        public async Task<string> UpdateRecord(Guid id, [FromBody] QuestionnaireRecordDTO recordDTO)
        {
            var entity = await _iRecord.UpdateQuestionnaireRecord(id, recordDTO);
            return entity;
        }
        //删除问卷问题
        [HttpDelete("record/{id}")]
        public async Task<string> DeleteRecord(Guid id)
        {
            var entity = await _iRecord.DeleteQuestionnaireRecord(id);
            return entity;
        }
        //获取问卷问题
        [HttpGet("record")]
        public string GetRecordList([FromQuery] PageFromQuery query)
        {
            var entity = _iRecord.GetListOrByQuestionnaireRecordName(query);
            return entity;
        }

        /// <summary>
        /// 新增 问卷问题选项
        /// </summary>
        /// <param name="questionnaireDTO"></param>
        /// <returns></returns>
        [HttpPost("option")]
        public Task<string> AddQuestionnaireOption([FromBody] ListOptionDTO listoptionDTO)
        {
            var entity = _iOption.AddOption(listoptionDTO);
            return entity;
        }
        //更新选项
        [HttpPut("option/{id}")]
        public async Task<string> UpdateOption(Guid id, [FromBody] OptionDTO optionDTO)
        {
            var entity = await _iOption.UpdateOptione(id, optionDTO);
            return entity;
        }
        //删除选项
        [HttpDelete("option/{id}")]
        public async Task<string> DeleteOption(Guid id)
        {
            var entity = await _iOption.DeleteOption(id);
            return entity;
        }
        //获取问题选项
        [HttpGet("option/{id}")]
        public async Task<string> GetOptionList(Guid id)
        {
            var entity = await _iOption.GetbyId(id);
            return entity;
        }

    }
}