using Web.Domain.Entity;
using Web.Application.Utils;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.Common.Interface;
using Web.Application.Common.Interface.IServer;
using Web.Application.ResDto;

namespace Web.Services.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly IRepository<CourseInfo> _course;
        private readonly IRepository<TextBookInfo> _textbook;
        private readonly IRepository<TeacherInfo> _teacher;
        private readonly IRepository<SpecializedInfo> _specialized;

        public CourseServices(IRepository<CourseInfo> course, IRepository<TextBookInfo> textbook, IRepository<TeacherInfo> teacher, IRepository<SpecializedInfo> specialized)
        {
            _course = course;
            _textbook = textbook;
            _teacher = teacher;
            _specialized = specialized;
        }

        // 通过 ID 查找 异步
        // 传入：需要删除的课程的 主键ID
        // 返回：string
        // data：对应 ID 的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> GetbyId(Guid id)
        {
            var entity = await _course.GetByIdAsync(id);

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
                    Msg = "获取数据失败，课程不存在"
                }.SerializeObject();
            }
        }

        // 查询课程列表或者（指定名称模糊查询）
        // 传入：分页信息
        // 返回：string，分页信息
        // data：查询到的内容
        // 成功返回 200
        public string GetListOrByCourseName(PageFromQuery query)
        {
            var list = _course.Table.Where(x => x.IsDeleted == false);

            // 判断keyword 是否为空，为空则查询所有
            if (!string.IsNullOrEmpty(query.keyword))
            {
                list = list.Where(x => x.CourseName.Contains(query.keyword));
            }

            var tmp = list.Join(_textbook.Table, x => x.TextBookId, p => p.Id, (x, p) => new
            {
                course = x,
                textBook = p
            });

            var tmp2 = tmp.Join(_teacher.Table, x => x.course.TeacherId, p => p.WorkNumber, (x, p) => new
            {
                course = x.course,
                textBook = x.textBook,
                teacher = p
            });

            // 分页
            var paging = tmp2.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).OrderByDescending(x => x.course.CreatedAt).ToList();

            return new
            {
                Code = 200,
                Msg = "获取课程数据成功",
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
        public async Task<string> AddCourse(CourseDTO courseDTO)
        {
            var isExist = _course.Table.Where(x => x.CourseName == courseDTO.CourseName).FirstOrDefault() == null;
            // 判断是否存在
            if (isExist)
            {
                var entity = new CourseInfo { };

                entity.CourseName = courseDTO.CourseName;
                entity.TextBookId = courseDTO.TextBookId;
                entity.isPublicCourses = courseDTO.isPublicCourses;
                entity.SpecializedNum = courseDTO.SpecializedNum;
                entity.TeacherId = courseDTO.TeacherId;

                await _course.AddAsync(entity);

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
                    Msg = "添加失败，课程已存在"
                }.SerializeObject();
            }
        }

        // 修改方法 异步
        // 传入：需要修改的课程的 主键 ID 和修改内容
        // 返回：string
        // data：修改完成后的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> UpdateCourse(Guid id, CourseDTO courseDTO)
        {
            var tmp = _course.Table.Where(x => x.Id == id).FirstOrDefault();
            // 判断是否存在
            if (tmp != null)
            {
                var name = _course.Table.Where(x => x.CourseName == courseDTO.CourseName && x.Id != id).FirstOrDefault() == null;

                if (name)
                {
                    tmp.CourseName = courseDTO.CourseName;
                    tmp.TextBookId = courseDTO.TextBookId;
                    tmp.isPublicCourses = courseDTO.isPublicCourses;
                    tmp.SpecializedNum = courseDTO.SpecializedNum;
                    tmp.TeacherId = courseDTO.TeacherId;

                    var entity = await _course.UpdateAsync(tmp);

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
                        Msg = "修改数据失败，课程已存在"
                    }.SerializeObject();
                }
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "修改数据失败，课程不存在"
                }.SerializeObject();
            }
        }

        // 删除方法 异步
        // 传入：需要删除的课程的 主键ID
        // 返回：string
        // data：无
        // 成功返回 200
        // 失败返回 402
        public async Task<string> DeleteCourse(Guid id)
        {
            var isExist = _course.Table.Where(x => x.Id == id).FirstOrDefault() == null;
            // 判断是否存在
            if (!isExist)
            {
                await _course.DeleteAsync(id, false);

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
                    Msg = "删除失败，课程不存在"
                }.SerializeObject();
            }
        }
    }
}