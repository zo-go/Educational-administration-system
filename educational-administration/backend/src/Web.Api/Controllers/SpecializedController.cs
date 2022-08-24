using Microsoft.AspNetCore.Mvc;

using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.ResDto;
using Web.Application.Utils;

namespace Web.Api.Configuration
{
    [ApiController]
    [Route("[controller]")]
    // 请求地址 http://localhost:5003/specialized
    public class SpecializedController : ControllerBase
    {
        private readonly ISpecializedServices _specialized;
        public SpecializedController(ISpecializedServices specialized)
        {
            _specialized = specialized;
        }

        // get
        public string getSpecialized([FromQuery] PageFromQuery query)
        {
            var tmp = _specialized.GetListOrBySpecializedName(query);

            return tmp;
        }

        // post
        [HttpPost]
        public async Task<string> addSpecialized(SpecializedDTO specializedDto)
        {
            var tmp = await _specialized.AddSpecialized(specializedDto);

            return tmp;
        }

        // put
        [HttpPut("{id}")]
        public async Task<string> updateSpecialized(SpecializedDTO specializedDto, Guid id)
        {
            var tmp = await _specialized.UpdateSpecialized(id, specializedDto);

            return tmp;
        }

        // delete
        [HttpDelete("{id}")]
        public async Task<string> deleteSpecialized(Guid id)
        {
            var tmp = await _specialized.DeleteSpecialized(id);

            return tmp;
        }
    }
}