using Web.Domain.Entity;
using Web.Application.Utils;
using Web.Application.ResDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.Common.Interface;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using System.Collections;

namespace Web.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IRepository<AppUser> _user;
        private readonly IRepository<AppRole> _role;
        private readonly IRepository<StudentInfo> _student;
        private readonly IRepository<TeacherInfo> _teacher;
        private readonly IRepository<ClassInfo> _class;
        private readonly IRepository<AcademyInfo> _academy;
        private readonly IRepository<SpecializedInfo> _specialized;


        public UserServices(IRepository<AppUser> user, IRepository<AppRole> role, IRepository<StudentInfo> student, IRepository<TeacherInfo> teacher, IRepository<ClassInfo> classes, IRepository<AcademyInfo> academy, IRepository<SpecializedInfo> specialized)
        {
            _user = user;
            _role = role;
            _student = student;
            _teacher = teacher;
            _class = classes;
            _academy = academy;
            _specialized = specialized;
        }

        // 通过 ID 查找 异步
        // 传入：需要用户的 主键ID
        // 返回：string
        // data：对应 ID 的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> GetbyId(Guid id)
        {
            var entity = await _user.GetByIdAsync(id);

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
                    Msg = "获取数据失败，用户不存在"
                }.SerializeObject();
            }
        }

        // 查询用户列表或者（指定名称模糊查询）
        // 传入：需要查询的的用户的 UserName，分页信息
        // 返回：IQueryable<AppUser>
        public string GetListOrByUserName(PageFromQuery query)
        {
            var list = _user.Table.Where(x => x.IsDeleted == false);

            // 判断keyword 是否为空，为空则查询所有
            if (!string.IsNullOrEmpty(query.keyword))
            {
                list = list.Where(x => x.UserName.Contains(query.keyword) || (x.OpenId != null && x.OpenId.Contains(query.keyword)));
            }

            // // join 角色表
            var tmp = list.Join(_role.Table.Where(x => x.IsDeleted == false), x => x.RoleId, p => p.Id, (x, p) => new
            {
                UserInfo = x,
                RoleInfo = p
            });

            // join 学生
            var tmp2 = _student.Table.Join(tmp, x => x.StudentId, p => p.UserInfo.UserName, (x, p) => new
            {
                StudentInfo = x,
                UserInfo = p,
            }).ToList();

            // // join 教师
            var tmp3 = tmp.Join(_teacher.Table, x => x.UserInfo.UserName, p => p.WorkNumber, (x, p) => new
            {
                UserInfo = x.UserInfo,
                RoleInfo = x.RoleInfo,
                TeacherInfo = p
            }).ToList();

            ArrayList tmp4 = new ArrayList();
            ArrayList tmp5 = new ArrayList();

            var paging2 = tmp2.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();
            var paging3 = tmp3.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();


            // 添加到数组
            foreach (var item in tmp2)
            {
                tmp4.Add(item);
            }
            foreach (var item in tmp3)
            {
                tmp4.Add(item);
            }

            // 分页读取页数
            var aaa = (query.PageIndex - 1) * query.PageSize;
            var bbb = aaa + 10;

            if (aaa + 10 > tmp4.Count)
            {
                bbb = tmp4.Count;
            }

            // 分页
            for (int i = aaa; i < bbb; i++)
            {
                tmp5.Add(tmp4[i]);
            }

            return new
            {
                Code = 200,
                Msg = "获取用户数据成功",
                Data = tmp5,
                Page = new PageDto
                {
                    pageIndex = query.PageIndex,
                    pageSize = query.PageSize,
                    OnThisPage = tmp5.Count,
                    Count = list.Count()
                }
            }.SerializeObject();
        }

        // 添加方法 异步
        // 传入：指定类型的 Dto (数据传输对象)
        // 返回：string
        // data：添加完成后的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> AddUser(UserDTO user)
        {
            var isExist = _user.Table.Where(x => x.UserName == user.UserName).FirstOrDefault() == null;
            // 判断是否存在
            if (isExist)
            {
                var entity = new AppUser { };
                var role_student = _role.Table.Where(x => x.RoleName == "学生").FirstOrDefault();

                entity.UserName = user.UserName;
                entity.PassWord = user.PassWord;
                if (user.OpenId != null)
                {
                    entity.OpenId = user.OpenId;
                }

                // 默认为学生
                if (user.RoleId == new Guid("00000000-0000-0000-0000-000000000000"))
                {
                    entity.RoleId = role_student!.Id;
                }
                else
                {
                    entity.RoleId = user.RoleId;
                }

                await _user.AddAsync(entity);

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
                    Msg = "添加失败，用户已存在"
                }.SerializeObject();
            }
        }

        // 从 Excel 导入数据
        public Task<string> uploadExcel(string[] ExcelContent)
        {
            throw new NotImplementedException();
        }

        // 修改方法 异步
        // 传入：需要修改的用户的 主键 ID 和修改内容
        // 返回：string
        // data：修改完成后的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> UpdateUser(Guid id, UserDTO user)
        {
            var tmp = _user.Table.Where(x => x.Id == id).FirstOrDefault();
            // 判断是否存在
            if (tmp != null)
            {
                var name = _user.Table.Where(x => x.UserName == user.UserName && x.Id != id).FirstOrDefault() == null;

                if (name)
                {
                    tmp.UserName = user.UserName;
                    tmp.PassWord = user.PassWord;
                    if (user.OpenId != null)
                    {
                        tmp.OpenId = user.OpenId;
                    }

                    var entity = await _user.UpdateAsync(tmp);

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
                        Msg = "修改数据失败，用户已存在"
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
        // 传入：需要删除的用户的 主键ID
        // 返回：string
        // data：无
        // 成功返回 200
        // 失败返回 402
        public async Task<string> DeleteUser(Guid id)
        {
            var isExist = _user.Table.Where(x => x.Id == id).FirstOrDefault() == null;
            // 判断是否存在
            if (!isExist)
            {
                await _user.DeleteAsync(id, false);

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
                    Msg = "删除失败，用户不存在"
                }.SerializeObject();
            }
        }

        //传openid，获取openid是否存在
        public string GetOpenId(string OpenId)
        {
            var theUser = _user.Table.Where(x => x.OpenId == OpenId).FirstOrDefault();
            if (theUser != null)
            {

                if (string.IsNullOrEmpty(theUser.OpenId))
                {
                    return new
                    {
                        Code = 402,
                        Msg = "OpenId不存在"
                    }.SerializeObject();
                }
                else
                {
                    return new
                    {
                        Code = 200,
                        Msg = "OpenId获取成功",
                        Data = theUser
                    }.SerializeObject();
                }
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "用户不存在"
                }.SerializeObject();
            }

        }

        public async Task<string> AddOpenid(UserOpenIdDTO openDto)
        {
            var theUser = _user.Table.Where(x => x.UserName == openDto.UserName && x.PassWord == openDto.PassWord).FirstOrDefault();
            if (theUser != null)
            {
                theUser.OpenId = openDto.OpenId;
                var result = await _user.UpdateAsync(theUser);
                return new
                {
                    Code = 200,
                    Msg = "绑定openid成功",
                    Data = result
                }.SerializeObject();

            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "用户不存在"
                }.SerializeObject();
            }
        }

        public string GetByStudentInfo(UserForAuth user)
        {
            var theUser = _user.Table.Where(x => x.UserName == user.Username && x.PassWord == user.Password).FirstOrDefault();
            if (theUser != null)
            {
                var studentTable = _student.Table.Where(x => x.IsDeleted == false);
                var teacherTable = _teacher.Table.Where(x => x.IsDeleted == false);
                var academyTable = _academy.Table.Where(x => x.IsDeleted == false);
                var speciaTable = _specialized.Table.Where(x => x.IsDeleted == false);
                var classTable = _class.Table.Where(x => x.IsDeleted == false);
                var userTable = _user.Table.Where(x => x.UserName == user.Username && x.PassWord == user.Password);
                var userstudent = userTable.Join(studentTable, x => x.UserName, y => y.StudentId, (x, y) => new
                {
                    studentInfo = y
                });
                var userstudentclass = userstudent.Join(classTable, x => x.studentInfo.ClassId, y => y.Id, (x, y) => new
                {
                    classInfo = y,
                    studentInfo = x.studentInfo
                });
                var userstudentclassspecia = userstudentclass.Join(speciaTable, x => x.classInfo.SpecializedNum, y => y.SpecializedNum, (x, y) => new
                {
                    classInfo = x.classInfo,
                    studentInfo = x.studentInfo,
                    speciaInfo = y
                });
                var result = userstudentclassspecia.Join(academyTable, x => x.speciaInfo.AcademyNum, y => y.AcademyNum, (x, y) => new
                {
                    classInfo = x.classInfo,
                    studentInfo = x.studentInfo,
                    speciaInfo = x.speciaInfo,
                    academyinfo = y
                });
                return new
                {
                    Code = 402,
                    Msg = "获取指定学生信息成功",
                    Data = result
                }.SerializeObject();


            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "用户不存在"
                }.SerializeObject();
            }

        }
    }
}