using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public class StudentServices : IStudentServices
    {
        private readonly IRepository<StudentInfo> _student;
        private readonly IRepository<AppUser> _user;
        private readonly IRepository<ClassInfo> _class;
        private readonly IRepository<SpecializedInfo> _specialized;
        private readonly IUserServices _userServices;

        public StudentServices(IRepository<StudentInfo> student, IRepository<AppUser> user, IRepository<ClassInfo> Class, IRepository<SpecializedInfo> specialized, IUserServices userServices)
        {
            _user = user;
            _student = student;
            _class = Class;
            _specialized = specialized;
            _userServices = userServices;
        }
        public Task<string> GetbyId(Guid id)
        {
            throw new NotImplementedException();
        }

        // 查询用户列表或者（指定名称模糊查询）
        // 传入：需要查询的的用户的 UserName，分页信息
        // 返回：IQueryable
        public string GetListOrByStudentName(PageFromQuery query)
        {
            var list = _student.Table.Where(x => x.IsDeleted == false);

            // 判断keyword 是否为空，为空则查询所有
            if (!string.IsNullOrEmpty(query.keyword))
            {
                list = list.Where(x => x.StudentName.Contains(query.keyword) || x.StudentId!.Contains(query.keyword));
            }

            // join 用户表
            var tmp = list.Join(_user.Table.Where(x => x.IsDeleted == false), x => x.StudentId, p => p.UserName, (x, p) => new
            {
                studentInfo = x,
                userInfo = p
            });

            // join 班级信息表
            var tmp2 = tmp.Join(_class.Table.Where(x => x.IsDeleted == false), x => x.studentInfo.ClassId, p => p.Id, (x, p) => new
            {
                studentInfo = x.studentInfo,
                userInfo = x.userInfo,
                classInfo = p
            });

            // 分页
            var paging = tmp2.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).OrderByDescending(x => x.studentInfo.CreatedAt).ToList();

            return new
            {
                Code = 200,
                Msg = "获取用户数据成功",
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
        public async Task<string> AddStudent(StudentDTO studentDTO)
        {
            var isExist = _student.Table.Where(x => x.StudentId == studentDTO.StudentId && x.StudentId != "").FirstOrDefault() == null;
            // 判断是否存在
            if (isExist)
            {
                var entity = new StudentInfo { };

                var time = DateTime.Now.ToString().Split("-")[0].Substring(2, 2);
                var classinfo = _class.Table.Where(x => x.Id == studentDTO.ClassId).FirstOrDefault();
                var specialized = _specialized.Table.Where(x => x.SpecializedNum == classinfo!.SpecializedNum).FirstOrDefault();
                var count = _student.Table.Count();

                var StudentId = RandomGeneration.generateStudentNumber(time, specialized!.AcademyNum, classinfo!.SpecializedNum, classinfo!.SpecializedNum, 50)[count];

                entity.StudentId = StudentId;
                entity.StudentName = studentDTO.StudentName;
                entity.Sex = studentDTO.Sex;
                entity.IdNumber = studentDTO.IdNumber;
                entity.PhoneNumber = studentDTO.PhoneNumber;
                entity.qqNumber = studentDTO.qqNumber;
                entity.WeChat = studentDTO.WeChat;
                entity.Address = studentDTO.Address;
                entity.EnrollmentTime = studentDTO.EnrollmentTime;
                entity.FatherName = studentDTO.FatherName;
                entity.MotherName = studentDTO.MotherName;
                entity.ContactDetails = studentDTO.ContactDetails;
                entity.ClassId = studentDTO.ClassId;

                await _student.AddAsync(entity);

                await _userServices.AddUser(new UserDTO
                {
                    UserName = entity.StudentId,
                    PassWord = "123456"
                });

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
                    Code = 200,
                    Msg = "添加失败，学生已存在"
                }.SerializeObject();
            }

        }

        // 修改方法 异步
        // 传入：需要修改的用户的 主键 ID 和修改内容
        // 返回：string
        // data：修改完成后的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> UpdateStudent(Guid id, StudentDTO studentDTO)
        {
            var tmp = _student.Table.Where(x => x.Id == id).FirstOrDefault();
            // 判断是否存在
            if (tmp != null)
            {
                var name = _student.Table.Where(x => x.StudentId == studentDTO.StudentId).FirstOrDefault() == null;

                if (!name)
                {
                    tmp.StudentId = studentDTO.StudentId;
                    tmp.StudentName = studentDTO.StudentName;
                    tmp.Sex = studentDTO.Sex;
                    tmp.IdNumber = studentDTO.IdNumber;
                    tmp.PhoneNumber = studentDTO.PhoneNumber;
                    tmp.qqNumber = studentDTO.qqNumber;
                    tmp.WeChat = studentDTO.WeChat;
                    tmp.Address = studentDTO.Address;
                    tmp.FatherName = studentDTO.FatherName;
                    tmp.MotherName = studentDTO.MotherName;
                    tmp.ContactDetails = studentDTO.ContactDetails;

                    var entity = await _student.UpdateAsync(tmp);

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
                        Msg = "修改数据失败，学生已存在"
                    }.SerializeObject();
                }
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "修改数据失败，学生不存在"
                }.SerializeObject();
            }
        }

        // 删除方法 异步
        // 传入：需要删除的学生的 主键ID
        // 返回：string
        // data：无
        // 成功返回 200
        // 失败返回 402
        public async Task<string> DeleteStudent(Guid id)
        {
            var isExist = _student.Table.Where(x => x.Id == id).FirstOrDefault() == null;
            // 判断是否存在
            if (!isExist)
            {
                await _student.DeleteAsync(id, true);

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
                    Msg = "删除失败，学生不存在"
                }.SerializeObject();
            }
        }
    }
}