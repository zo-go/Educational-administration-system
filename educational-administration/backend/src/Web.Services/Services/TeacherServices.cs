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
    public class TeacherServices : ITeacherServices
    {
        private readonly IRepository<TeacherInfo> _teacherRepository;
        private readonly IRepository<AppRole> _roleRepository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<SpecializedInfo> _specializedRes;
        private readonly IRepository<AcademyInfo> _academyRes;
        private readonly IUserServices _iuserServices;

        public TeacherServices(IRepository<TeacherInfo> teacherRepository, IUserServices iuserServices, IRepository<SpecializedInfo> specializedRes, IRepository<AcademyInfo> academyRes, IRepository<AppRole> roleRepository, IRepository<AppUser> userRepository)
        {
            _teacherRepository = teacherRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _specializedRes = specializedRes;
            _academyRes = academyRes;
            _iuserServices = iuserServices;
        }

        public string GetTeacherName(PageFromQuery query)
        {
            var academylist = _academyRes.Table.Where((x => x.IsDeleted == false));
            var teacherlist = _teacherRepository.Table.Where(x => x.IsDeleted == false);

            var role = _roleRepository.Table.Where(x => x.IsDeleted == false);
            var userFullInfo = _userRepository.Table.Join(role, x => x.RoleId, g => g.Id, (x, g) => new { x.UserName, g.RoleName });


            var list = teacherlist.Join(userFullInfo, x => x.WorkNumber, g => g.UserName, (x, g) => new
            {
                teacherInfo = x,
                g.RoleName
            }).Join(academylist, x => x.teacherInfo.AcademyNum, g => g.AcademyNum, (x, g) => new { g.AcademyName, x.RoleName, x.teacherInfo });

            if (!string.IsNullOrEmpty(query.keyword))
            {
                list = list
                .Where(x => x.teacherInfo.AcademyNum == query.keyword && x.RoleName == "辅导员");
            }

            var paging = list.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();

            return new
            {
                Code = 200,
                Msg = "获取学院号为" + query.keyword + "学院教师列表成功",
                Data = paging,
                Page = new PageDto
                {
                    pageIndex = query.PageIndex,
                    pageSize = query.PageSize,
                    OnThisPage = paging.Count(),
                    Count = list.Count()
                }
            }.SerializeObject();
        }


        public async Task<string> AddTeacher(TeacherDTO teacherDTO)
        {
            var teacherlist = _teacherRepository.Table;

            var roleId = _roleRepository.Table.ToList();
            var rid = new Guid();
            if (teacherDTO.RoleName == "教师")
            {
                rid = roleId.Where(x => x.RoleName == "教师").FirstOrDefault()!.Id;
            }
            else if (teacherDTO.RoleName == "辅导员")
            {
                rid = roleId.Where(x => x.RoleName == "辅导员").FirstOrDefault()!.Id;

            }


            var time = DateTime.Now.Year.ToString().Substring(2, 2);
            var academynum = teacherDTO.AcademyNum;

            var Tnumber = "T" + RandomGeneration.generateTeacherNumber(time, academynum!, 50)[teacherlist.Count()];

            var isHas = _teacherRepository.Table.Where(x => (x.WorkNumber == Tnumber) || x.IdNumber == teacherDTO.IdNumber).FirstOrDefault();
            if (isHas != null)
            {
                return new

                {
                    Code = 402,
                    Msg = "添加教师失败教师工号或同身份号,已存在",
                    Data = ""
                }.SerializeObject();
            }

            await _iuserServices.AddUser(new UserDTO
            {
                UserName = Tnumber,
                PassWord = "123456",
                RoleId = rid
            });
            var entity = await _teacherRepository.AddAsync(new TeacherInfo
            {
                WorkNumber = Tnumber,
                TeacherName = teacherDTO.TeacherName,
                Sex = teacherDTO.Sex,
                IdNumber = teacherDTO.IdNumber,
                AcademyNum = teacherDTO.AcademyNum,
                PhoneNumber = teacherDTO.PhoneNumber,
                qqNumber = teacherDTO.qqNumber,
                WeChat = teacherDTO.WeChat,
                Address = teacherDTO.Address
            });



            return new
            {
                Code = 200,
                Msg = "添加教师成功",
                Data = entity
            }.SerializeObject();
        }

        public async Task<string> DeleteTeacher(Guid id)
        {
            var entity = await _teacherRepository.DeleteAsync(id);

            return new
            {
                Code = 200,
                Msg = "删除教师成功",
                Data = entity
            }.SerializeObject();
        }

        public async Task<string> GetbyId(Guid id)
        {
            var entity = await _teacherRepository.GetByIdAsync(id);

            return new
            {
                Code = 200,
                Msg = "查找教师成功",
                Data = entity
            }.SerializeObject();
        }

        public string GetListOrByTeacherName(PageFromQuery query)
        {

            var academylist = _academyRes.Table.Where((x => x.IsDeleted == false));
            var teacherlist = _teacherRepository.Table.Where(x => x.IsDeleted == false);

            var role = _roleRepository.Table.Where(x => x.IsDeleted == false);
            var userFullInfo = _userRepository.Table.Join(role, x => x.RoleId, g => g.Id, (x, g) => new { x.UserName, g.RoleName });


            var list = teacherlist.Join(userFullInfo, x => x.WorkNumber, g => g.UserName, (x, g) => new
            {
                teacherInfo = x,
                g.RoleName
            }).Join(academylist, x => x.teacherInfo.AcademyNum, g => g.AcademyNum, (x, g) => new { g.AcademyName, x.RoleName, x.teacherInfo });

            if (!string.IsNullOrEmpty(query.keyword))
            {
                list = list
                .Where(x => x.teacherInfo.TeacherName.Contains(query.keyword) || x.teacherInfo.Address!.Contains(query.keyword)
                 || x.teacherInfo.IdNumber!.Contains(query.keyword) || x.RoleName == query.keyword);
            }

            var paging = list.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).OrderByDescending(x => x.teacherInfo.CreatedAt).ToList();

            return new
            {
                Code = 200,
                Msg = "获取教师列表成功",
                Data = paging,
                Page = new PageDto
                {
                    pageIndex = query.PageIndex,
                    pageSize = query.PageSize,
                    OnThisPage = paging.Count(),
                    Count = list.Count()
                }
            }.SerializeObject();
        }

        public async Task<string> UpdateTeacher(Guid id, TeacherDTO teacherDTO)
        {
            var entity = await _teacherRepository.GetByIdAsync(id);

            if (entity != null)
            {

                var user = _userRepository.Table.Where(x => x.UserName == entity.WorkNumber).FirstOrDefault();

                var roleid = _roleRepository.Table.Where(x => x.RoleName == teacherDTO.RoleName).FirstOrDefault();


                user!.RoleId = roleid!.Id;
                await _userRepository.UpdateAsync(user);




                entity.AcademyNum = teacherDTO.AcademyNum;
                entity.TeacherName = teacherDTO.TeacherName;
                entity.IdNumber = teacherDTO.IdNumber;
                entity.Sex = teacherDTO.Sex;
                entity.PhoneNumber = teacherDTO.PhoneNumber;
                entity.qqNumber = teacherDTO.qqNumber;
                entity.WeChat = teacherDTO.WeChat;
                entity.Address = teacherDTO.Address;

                await _teacherRepository.UpdateAsync(entity);


                return new
                {
                    Code = 200,
                    Msg = "更新教师信息成功",
                    Data = entity
                }.SerializeObject();

            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "未查到该教师信息",
                    Data = entity
                }.SerializeObject();

            }

        }
    }
}