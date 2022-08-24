using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto.ServerDto
{
    public class ListRecordDto
    {
        //问卷主题表Id
        public Guid QuestionnaireID { get; set; }
        public List<QuestionnaireRecordDTO> RecordDTOs { get; set; } = new List<QuestionnaireRecordDTO>();
    }
}