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
    // 请求地址 http://localhost:5003/textbook
    public class TextBookController : ControllerBase
    {
        private readonly ITextBookServices _textbook;
        public TextBookController(ITextBookServices textbook)
        {
            _textbook = textbook;
        }

        // get
        public string getTextBook([FromQuery] PageFromQuery query)
        {
            var tmp = _textbook.GetListOrByTextBookName(query);

            return tmp;
        }

        // post
        [HttpPost]
        public async Task<string> addTextBook(TextBookDTO textbookDto)
        {
            var tmp = await _textbook.AddTextBook(textbookDto);

            return tmp;
        }

        // put
        [HttpPut("{id}")]
        public async Task<string> updateTextBook(TextBookDTO textbookDto, Guid id)
        {
            var tmp = await _textbook.UpdateTextBook(id, textbookDto);

            return tmp;
        }

        // delete
        [HttpDelete("{id}")]
        public async Task<string> deleteTextBook(Guid id)
        {
            var tmp = await _textbook.DeleteTextBook(id);

            return tmp;
        }
    }
}