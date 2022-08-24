using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionnaireResController : ControllerBase
    {
        private readonly IQuestionnaireResServices _iquestRes;
        public QuestionnaireResController(IQuestionnaireResServices iquestRes)
        {
            _iquestRes = iquestRes;
        }

        [HttpGet]
        public string GetList([FromQuery] PageFromQuery pageDto)
        {
            var entity = _iquestRes.GetListOrByQuestionnaireResName(pageDto);
            return entity;
        }

        [HttpGet("key")]
        public string GetbyIdFullInfo([FromQuery] KeyResDto KeyDto)
        {
            var entity = _iquestRes.GetbyIdFullInfo(KeyDto);
            return entity;
        }


        [HttpPost]
        public async Task<string> AddQuestionnaireRes([FromBody] ResListDto resListDto)
        {
            var entity = await _iquestRes.AddQuestionnaireRes(resListDto);

            return entity;
        }
        [HttpDelete("{id}")]
        public async Task<string> DeleteQuestionnaireRes(Guid id)
        {
            var entity = await _iquestRes.DeleteQuestionnaireRes(id);

            return entity;
        }

    }
}