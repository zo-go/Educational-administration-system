using Web.Domain.Entity;
using Web.Application.Utils;
using Web.Application.ResDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.Common.Interface;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;

namespace Web.Services.Services
{
    public class ClassServices : IClassServices
    {
        private readonly IRepository<ClassInfo> _classes;
        private readonly IRepository<SpecializedInfo> _specialize;
        private readonly IRepository<TeacherInfo> _teacher;

        public ClassServices(IRepository<ClassInfo> classes, IRepository<SpecializedInfo> specialize, IRepository<TeacherInfo> teacher)
        {
            _classes = classes;
            _specialize = specialize;
            _teacher = teacher;
        }

        // 查询班级列表或者（指定名称模糊查询）
        // 传入：需要查询的的班级的 ClassName，分页信息
        // 返回：string，分页信息
        // data：查询到的内容
        // 成功返回 200
        public string GetListOrByClassName(PageFromQuery query)
        {
            var list = _classes.Table.Where(x => x.IsDeleted == false);

            // 判断keyword 是否为空，为空则查询所有
            if (!string.IsNullOrEmpty(query.keyword))
            {
                list = list.Where(x => x.ClassName.Contains(query.keyword));
            }

            // join 专业表
            var tmp = list.Join(_specialize.Table.Where(x => x.IsDeleted == false), x => x.SpecializedNum, p => p.SpecializedNum, (x, p) => new
            {
                classinfo = x,
                specializedInfo = p
            });

            // join 教师表
            var tmp2 = tmp.Join(_teacher.Table.Where(x => x.IsDeleted == false), x => x.classinfo.CounselorId, p => p.WorkNumber, (x, p) => new
            {
                classinfo = x.classinfo,
                specializedInfo = x.specializedInfo,
                teacherInfo = p
            });

            // 分页
            var paging = tmp2.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();

            return new
            {
                Code = 200,
                Msg = "获取班级数据成功",
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
        public async Task<string> AddClass(ClassDTO classDTO)
        {
            var isExist = _classes.Table.Where(x => x.ClassName == classDTO.ClassName).FirstOrDefault() == null;
            // 判断是否存在
            if (isExist)
            {
                var entity = new ClassInfo { };
                entity.ClassNum = classDTO.ClassNum;

                entity.ClassName = classDTO.ClassName;
                entity.SpecializedNum = classDTO.SpecializedNum;
                entity.CounselorId = classDTO.CounselorId;

                await _classes.AddAsync(entity);

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
                    Msg = "添加失败，班级已存在"
                }.SerializeObject();
            }
        }

        // 删除方法 异步
        // 传入：需要删除的班级的 主键ID
        // 返回：string
        // data：无
        // 成功返回 200
        // 失败返回 402
        public async Task<string> UpdatedClass(Guid id, ClassDTO classDTO)
        {
            var tmp = _classes.Table.Where(x => x.Id == id).FirstOrDefault();
            // 判断是否存在
            if (tmp != null)
            {
                var name = _classes.Table.Where(x => x.ClassName == classDTO.ClassName && x.Id != id).FirstOrDefault() == null;
                if (name)
                {
                    tmp.ClassName = classDTO.ClassName;
                    tmp.SpecializedNum = classDTO.SpecializedNum;
                    tmp.CounselorId = classDTO.CounselorId;

                    var entity = await _classes.UpdateAsync(tmp);

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
                        Msg = "修改数据失败，班级已存在"
                    }.SerializeObject();
                }
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "修改数据失败，班级不存在"
                }.SerializeObject();
            }
        }

        // 删除方法 异步
        // 传入：需要删除的班级的 主键ID
        // 返回：string
        // data：无
        // 成功返回 200
        // 失败返回 402
        public async Task<string> DeleteClass(Guid id)
        {
            var isExist = _classes.Table.Where(x => x.Id == id).FirstOrDefault() == null;
            // 判断是否存在
            if (!isExist)
            {
                await _classes.DeleteAsync(id, false);

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
                    Msg = "删除失败，班级不存在"
                }.SerializeObject();
            }
        }
    }
}