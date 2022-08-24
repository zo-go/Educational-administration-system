using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoreController : Controller
    {
        private readonly IScoreServices _scoreServices;

        public ScoreController(IScoreServices scoreServices)
        {
            _scoreServices = scoreServices;
        }


        //获取分数列表
        [HttpGet]
        public string GetScoreList([FromQuery] PageFromQuery query)
        {
            var result = _scoreServices.GetListOrByScoreName(query);
            return result;

        }

        //新增分数信息
        [HttpPost]

        public async Task<string> AddScore([FromBody] ScoreDTO scoreDTO)
        {
            var result = await _scoreServices.AddScore(scoreDTO);
            return result;


        }

        //删除分数

        [HttpDelete("{id}")]

        public async Task<string> DeleteScore(Guid id)
        {
            var result = await _scoreServices.DeleteScore(id);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<string> UpdateScore(Guid id, [FromBody] ScoreDTO scoreDTO)
        {
            var result = await _scoreServices.UpdateScore(id, scoreDTO);
            return result;
        }

    }
}