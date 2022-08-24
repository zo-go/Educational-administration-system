using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Common.Interface;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.ResDto;
using Web.Application.Utils;
using Web.Domain.Entity;

namespace Web.Services.Services
{
    public class SpecializedServices : ISpecializedServices
    {
        private readonly IRepository<SpecializedInfo> _specialize;
        private readonly IRepository<AcademyInfo> _academy;

        public SpecializedServices(IRepository<SpecializedInfo> specialize, IRepository<AcademyInfo> academy)
        {
            _specialize = specialize;
            _academy = academy;
        }

        public Task<string> GetbyId(Guid id)
        {
            throw new NotImplementedException();
        }

        // 查询专业列表或者（指定名称模糊查询）
        // 传入：需要查询的的专业的 SpecializedName，分页信息
        // 返回：string，分页信息
        // data：查询到的内容
        // 成功返回 200
        public string GetListOrBySpecializedName(PageFromQuery query)
        {
            var list = _specialize.Table.Where(x => x.IsDeleted == false);

            // 判断keyword 是否为空，为空则查询所有
            if (!string.IsNullOrEmpty(query.keyword))
            {
                list = list.Where(x => x.SpecializedName.Contains(query.keyword) || x.AcademyNum == query.keyword);
            }

            // join 学院表
            var tmp = list.Join(_academy.Table.Where(x => x.IsDeleted == false), x => x.AcademyNum, p => p.AcademyNum, (x, p) => new
            {
                Specialized = x,
                p.AcademyName
            });

            // 分页
            var paging = tmp.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();

            return new
            {
                Code = 200,
                Msg = "获取专业数据成功",
                Data = list,
                Page = new PageDto
                {
                    pageIndex = query.PageIndex,
                    pageSize = query.PageSize,
                    OnThisPage = paging.Count(),
                    Count = tmp.Count()
                }
            }.SerializeObject();
        }

        // 添加方法 异步
        // 传入：指定类型的 Dto (数据传输对象)
        // 返回：string
        // data：添加完成后的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> AddSpecialized(SpecializedDTO specializedDTO)
        {
            var isExist = _specialize.Table.Where(x => x.SpecializedName == specializedDTO.SpecializedName).FirstOrDefault() == null;
            var count = _specialize.Table.Count();
            // 判断是否存在
            if (isExist)
            {
                var entity = new SpecializedInfo { };

                entity.AcademyNum = specializedDTO.AcademyNum;
                entity.SpecializedNum = (count + 1).ToString();
                entity.SpecializedName = specializedDTO.SpecializedName;

                await _specialize.AddAsync(entity);

                return new
                {
                    Code = 200,
                    Msg = "添加成功",
                    Data = entity
                }.SerializeObject();
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "添加失败，专业已存在"
                }.SerializeObject();
            }
        }

        // 修改方法 异步
        // 传入：需要修改的班级的 主键 ID 和修改内容
        // 返回：string
        // data：修改完成后的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> UpdateSpecialized(Guid id, SpecializedDTO specializedDTO)
        {
            var tmp = _specialize.Table.Where(x => x.Id == id).FirstOrDefault();
            // 判断是否存在
            if (tmp != null)
            {
                var name = _specialize.Table.Where(x => x.SpecializedName == specializedDTO.SpecializedName && x.Id != id).FirstOrDefault();

                if (name == null)
                {
                    tmp.AcademyNum = specializedDTO.AcademyNum;
                    tmp.SpecializedNum = specializedDTO.SpecializedNum;
                    tmp.SpecializedName = specializedDTO.SpecializedName;

                    var entity = await _specialize.UpdateAsync(tmp);

                    return new
                    {
                        Code = 200,
                        Msg = "修改数据成功",
                        Data = entity
                    }.SerializeObject();
                }
                else
                {
                    return new
                    {
                        Code = 402,
                        Msg = "修改数据失败，专业已存在"
                    }.SerializeObject();
                }
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "修改数据失败，用户不存在"
                }.SerializeObject();
            }
        }

        // 删除方法 异步
        // 传入：需要删除的专业的 主键ID
        // 返回：string
        // data：无
        // 成功返回 200
        // 失败返回 402
        public async Task<string> DeleteSpecialized(Guid id)
        {
            var isExist = _specialize.Table.Where(x => x.Id == id).FirstOrDefault() == null;
            // 判断是否存在
            if (!isExist)
            {
                await _specialize.DeleteAsync(id, false);

                return new
                {
                    Code = 200,
                    Msg = "删除成功"
                }.SerializeObject();
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "删除失败，专业不存在"
                }.SerializeObject();
            }
        }
    }
}