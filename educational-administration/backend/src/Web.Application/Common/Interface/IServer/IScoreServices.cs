using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IScoreServices
    {
        //新增分数信息
        Task<string> AddScore(ScoreDTO scoreDTO);

        //修改分数 信息(根据id)
        Task<string> UpdateScore(Guid id, ScoreDTO scoreDTO);

        //查询分数 信息列表或者（指定名称模糊查询）
        string GetListOrByScoreName(PageFromQuery query);

        //删除分数 (根据id)
        Task<string> DeleteScore(Guid id);
    }
}