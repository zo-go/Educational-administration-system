using Web.Domain.Entity;
using Web.Application.Utils;
using Web.Application.ReqDto;
using Web.Application.Common.Interface;
using Web.Application.ReqDto.ServerDto;
using Web.Application.Common.Interface.IServer;

namespace Web.Services.Services
{
    public class CurriCulumServices : ICurriCulumServices
    {
        // 依赖注入
        private readonly IRepository<CurriCulum> _curriCulum;
        public CurriCulumServices(IRepository<CurriCulum> curriCulum)
        {
            _curriCulum = curriCulum;
        }

        // 查询排课列表
        // 传入：需要查询的排课列表的分页信息
        // 返回类型：string
        // 成功返回 200
        public string GetListOrByCurriCulumName(PageFromQuery query)
        {
            var entity = _curriCulum.Table.Where(x => x.IsDeleted == false).ToList();

            if (entity != null)
            {
                var res = new
                {
                    code = 200,
                    data = entity,
                    msg = "获取数据成功！"
                };

                return res.SerializeObject();
            }
            else
            {
                var res = new
                {
                    code = 402,
                    data = entity,
                    msg = "数据列表为空，获取数据失败！"
                };

                return res.SerializeObject();
            }
        }

        // 添加方法 异步
        // 传入：指定类型的 Dto (数据传输对象)
        // 返回：string
        // data：添加完成后的数据
        // 成功返回 200
        // 传入格式错误的数据或传入为空的数据返回 400
        // 失败返回 402
        public async Task<string> AddCurriCulum(CurriCulumDTO curriCulumDTO)
        {
            var ClassCount = _curriCulum.Table.Where(x => x.SpecializedName == curriCulumDTO.SpecializedName).Count();

            if (ClassCount < 5)
            {
                var tmp = await _curriCulum.AddAsync(new CurriCulum
                {
                    CurriCulumData = curriCulumDTO.CurriCulumData,
                    SpecializedName = curriCulumDTO.SpecializedName
                });

                return new
                {
                    Code = 200,
                    Msg = "添加成功",
                    Data = tmp
                }.SerializeObject();
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "添加失败"
                }.SerializeObject();
            }

        }

        // 修改方法 异步
        // 传入：需要修改的意向的 主键 ID 和修改内容
        // 返回：string
        // data：修改完成后的数据
        // 成功返回 200
        // 传入格式错误的数据或传入为空的数据返回 400
        // 失败返回 405
        public async Task<string> UpdatedCurriCulumName(Guid id, CurriCulumDTO curriCulumDTO)
        {
            var entity = await _curriCulum.GetByIdAsync(id);
            var allData = _curriCulum.Table.Where(x => x.SpecializedName != curriCulumDTO.SpecializedName).ToArray();
            // var count = -1;



            if (entity != null)
            {
                // foreach (var item in allData)
                // {
                //     foreach (var i in item.CurriCulumData!)
                //     {
                //         foreach (var j in curriCulumDTO.CurriCulumData!)
                //         {
                //             if (i == j)
                //             {
                //                 count++;
                //             }
                //         }
                //     }
                // }


                // if (count != -1)
                // {
                entity.CurriCulumData = curriCulumDTO.CurriCulumData;
                entity.SpecializedName = curriCulumDTO.SpecializedName;

                await _curriCulum.UpdateAsync(entity);

                return new
                {
                    Code = 200,
                    Msg = "课程修改成功！",
                    Data = curriCulumDTO
                }.SerializeObject();
                // }
                // else
                // {
                //     return new
                //     {
                //         Code = 402,
                //         Msg = "课程修改失败,你在这个时间段在其他班级有课"
                //     }.SerializeObject();
                // }
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "课程不存在，修改失败！"
                }.SerializeObject();
            }
        }

        // 删除方法 异步
        // 传入：需要删除的意向的 主键ID
        // 返回：string
        // data：无
        // 成功返回 200
        // 失败返回 402
        public async Task<string> DeleteCurriCulum(Guid id)
        {
            var entity = await _curriCulum.GetByIdAsync(id);

            if (entity != null)
            {
                await _curriCulum.DeleteAsync(id);

                var res = new
                {
                    code = 200,
                    data = "",
                    msg = "删除成功！"
                };

                return res.SerializeObject();
            }
            else
            {
                var res = new
                {
                    code = 405,
                    data = "",
                    msg = "课程不存在，删除失败！"
                };

                return res.SerializeObject();
            }
        }

        public string GetCurriCulumNameBySpecializedName(string SpecializedName)
        {
            throw new NotImplementedException();
        }
    }
}