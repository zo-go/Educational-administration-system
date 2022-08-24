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
    public class IntentionServices : IIntentionServices
    {
        private readonly IRepository<IntentionInfo> _intention;
        private readonly IRepository<StudentInfo> _student;
        private readonly IRepository<ClassInfo> _class;

        public IntentionServices(IRepository<StudentInfo> student, IRepository<ClassInfo> Class, IRepository<IntentionInfo> intention)
        {
            _student = student;
            _class = Class;
            _intention = intention;
        }

        // 通过 ID 查找 异步
        // 传入：需要删除的意向的 主键ID
        // 返回：string
        // data：对应 ID 的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> GetbyId(Guid id)
        {
            var entity = await _intention.GetByIdAsync(id);

            if (entity != null)
            {
                return new
                {
                    Code = 200,
                    Msg = "获取数据成功",
                    Data = entity
                }.SerializeObject();
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "获取数据失败，意向不存在"
                }.SerializeObject();
            }
        }

        // 查询意向列表或者（指定名称模糊查询）
        // 传入：需要查询的的意向的 UserName，分页信息
        // 返回：IQueryable<AppUser>
        public string GetListOrByIntentionName(PageFromQuery query)
        {
            var list = _intention.Table.Where(x => x.IsDeleted == false);

            // 判断keyword 是否为空，为空则查询所有
            // if (!string.IsNullOrEmpty(keyword))
            // {
            //     list = list.Where(x => x..Contains(keyword));
            // }

            // join 角色表
            var tmp = list.Join(_student.Table.Where(x => x.IsDeleted == false), x => x.StudentId, p => p.Id, (x, p) => new
            {
                Intention = x,
                UserInfo = p
            });

            // join 班级表
            var tmp2 = tmp.Join(_class.Table.Where(x => x.IsDeleted == false), x => x.Intention.ClassId, p => p.Id, (x, p) => new
            {
                Intention = x,
                classInfo = p
            });

            // 分页
            var paging = tmp2.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();

            return new
            {
                Code = 200,
                Msg = "获取意向数据成功",
                Data = paging,
                Page = new PageDto
                {
                    pageIndex = query.PageIndex,
                    pageSize = query.PageSize,
                    OnThisPage = paging.Count(),
                    Count = tmp2.Count()
                }
            }.SerializeObject();
        }

        // 添加方法 异步
        // 传入：指定类型的 Dto (数据传输对象)
        // 返回：string
        // data：添加完成后的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> AddIntention(IntentionDTO intentionDTO)
        {
            var isExist = _intention.Table.Where(x => x.StudentId == intentionDTO.StudentId).FirstOrDefault() == null;
            // 判断是否存在
            if (isExist)
            {
                var entity = new IntentionInfo { };

                entity.StudentId = intentionDTO.StudentId;
                entity.ClassId = intentionDTO.ClassId;

                await _intention.AddAsync(entity);

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
                    Msg = "添加失败，该学生以有意向"
                }.SerializeObject();
            }
        }

        // 修改方法 异步
        // 传入：需要修改的意向的 主键 ID 和修改内容
        // 返回：string
        // data：修改完成后的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> UpdateIntention(Guid id, IntentionDTO intentionDTO)
        {
            var tmp = _intention.Table.Where(x => x.Id == id).FirstOrDefault();
            // 判断是否存在
            if (tmp != null)
            {
                var name = _intention.Table.Where(x => x.StudentId == intentionDTO.StudentId && x.Id != id).FirstOrDefault() == null;

                if (name)
                {
                    tmp.StudentId = intentionDTO.StudentId;
                    tmp.ClassId = intentionDTO.ClassId;

                    var entity = await _intention.UpdateAsync(tmp);

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
                        Msg = "修改数据失败，该学生以有意向"
                    }.SerializeObject();
                }
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "修改数据失败，意向不存在"
                }.SerializeObject();
            }
        }

        // 删除方法 异步
        // 传入：需要删除的意向的 主键ID
        // 返回：string
        // data：无
        // 成功返回 200
        // 失败返回 402
        public async Task<string> DeleteIntention(Guid id)
        {
            var isExist = _intention.Table.Where(x => x.Id == id).FirstOrDefault() == null;
            // 判断是否存在
            if (!isExist)
            {
                await _intention.DeleteAsync(id, false);

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
                    Msg = "删除失败，意向不存在"
                }.SerializeObject();
            }
        }
    }
}