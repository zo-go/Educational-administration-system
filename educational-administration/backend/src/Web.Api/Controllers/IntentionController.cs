using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.ResDto;
using Web.Application.Utils;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // 请求地址 http://localhost:5003/intention
    public class IntentionController : ControllerBase
    {
        private readonly IIntentionServices _intention;
        public IntentionController(IIntentionServices intention)
        {
            _intention = intention;
        }

        // get
        public string getUser([FromQuery] PageFromQuery query)
        {
            var tmp = _intention.GetListOrByIntentionName(query);

            return tmp;
        }

        // post
        [HttpPost]
        public async Task<string> addUser(IntentionDTO intentionDTO)
        {
            var tmp = await _intention.AddIntention(intentionDTO);

            return tmp;
        }

        // put
        [HttpPut("{id}")]
        public async Task<string> updateUser(IntentionDTO intentionDTO, Guid id)
        {
            var tmp = await _intention.UpdateIntention(id, intentionDTO);

            return tmp;
        }

        // delete
        [HttpDelete("{id}")]
        public async Task<string> deleteUser(Guid id)
        {
            var tmp = await _intention.DeleteIntention(id);

            return tmp;
        }
    }
}