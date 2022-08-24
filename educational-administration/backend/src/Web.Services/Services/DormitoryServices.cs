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
    public class DormitoryServices : IDormitoryServices
    {
        private readonly IRepository<Dormitory> _dormRes;
        private readonly IRepository<StudentInfo> _studentRes;
        private readonly IRepository<ClassInfo> _classRes;
        private readonly IRepository<SpecializedInfo> _specializedRes;
        private readonly IRepository<TeacherInfo> _teacherRes;
        private readonly IRepository<AcademyInfo> _academyRes;
        public DormitoryServices(IRepository<Dormitory> dormRes, IRepository<AcademyInfo> academyRes, IRepository<StudentInfo> studentRes, IRepository<ClassInfo> classRes, IRepository<SpecializedInfo> specializedRes, IRepository<TeacherInfo> teacherRes)
        {
            _dormRes = dormRes;
            _studentRes = studentRes;
            _classRes = classRes;
            _specializedRes = specializedRes;
            _teacherRes = teacherRes;
            _academyRes = academyRes;
        }

        public async Task<string> AddDormitory(DormitoryDTO dormitoryDTO)
        {
            var isHas = _dormRes.Table.Where(x => x.StudentId == dormitoryDTO.StudentId && x.IsDeleted == false).FirstOrDefault();
            var hasUser = _studentRes.Table.Where(x => x.StudentId == dormitoryDTO.StudentId).FirstOrDefault();
            //判断该学生是否有学生记录
            if (isHas != null)
            {
                return new
                {
                    Code = 402,
                    Msg = "要添加学生宿舍记录已存在",
                    Data = ""

                }.SerializeObject();
            }
            //判断是否有这个学生信息
            if (hasUser == null)
            {
                return new
                {
                    Code = 402,
                    Msg = "要添加学生账号不存在",
                    Data = ""

                }.SerializeObject();
            }

            //判断宿舍是否满人 满人则添加失败
            var list = _dormRes.Table.Where(x => x.BuildingNum == dormitoryDTO.BuildingNum)
            .GroupBy(x => x.DormitoryNum)
            .Select(x => new { x.Key, count = x.Count() }).Where(x => x.count >= 6);

            foreach (var item in list)
            {
                if (dormitoryDTO.DormitoryNum == item.Key)
                {
                    return new
                    {
                        Code = 402,
                        Msg = "要添加宿舍已满",
                        Data = ""

                    }.SerializeObject();
                }
            }

            var dorm = await _dormRes.AddAsync(new Dormitory
            {
                BuildingNum = dormitoryDTO.BuildingNum,
                DormitoryNum = dormitoryDTO.DormitoryNum,
                StudentId = dormitoryDTO.StudentId,
                isDormAdmin = dormitoryDTO.isDormAdmin
            });
            return new
            {
                Code = 200,
                Msg = "新增宿舍记录成功",
                Data = dorm
            }.SerializeObject();

        }

        public async Task<string> DeleteDormitory(Guid id)
        {
            var tmp = await _dormRes.GetByIdAsync(id);
            if (tmp!.IsDeleted == true)
            {
                return new
                {
                    Code = 200,
                    Msg = "该宿舍记录已删除,请勿重复删除",
                    Data = tmp
                }.SerializeObject();
            }

            var dorminfo = await _dormRes.DeleteAsync(id);

            return new
            {
                Code = 200,
                Msg = "删除宿舍记录成功",
                Data = dorminfo
            }.SerializeObject();
        }

        public async Task<string> GetbyId(Guid id)
        {
            var dorminfo = await _dormRes.GetByIdAsync(id);
            return new
            {

                Code = 200,
                Msg = "查询宿舍记录成功",
                Data = dorminfo
            }.SerializeObject();
        }

        public string GetDormitoryCount(string buildingNum)
        {
            var list = _dormRes.Table.Where(x => x.BuildingNum == buildingNum)
            .GroupBy(x => x.DormitoryNum)
            .Select(x => new { x.Key, count = x.Count() }).Where(x => x.count < 6);


            return new
            {
                Code = 200,
                Msg = "查询未满宿舍成功",
                Data = list
            }.SerializeObject();

        }

        public string GetListOrByDormitoryName(PageFromQuery query)
        {
            var dorminfo = _dormRes.Table.Where(x => x.IsDeleted == false);

            var userlist = _studentRes.Table.Where(x => x.IsDeleted == false);
            var classlist = _classRes.Table.Where(x => x.IsDeleted == false);
            var teacherlist = _teacherRes.Table.Where(x => x.IsDeleted == false);
            var academylist = _academyRes.Table.Where((x => x.IsDeleted == false));
            var specializedlist = _specializedRes.Table.Where(x => x.IsDeleted == false)
            .Join(academylist, x => x.AcademyNum, g => g.AcademyNum, (x, g) => new { x.SpecializedName, x.SpecializedNum, g.AcademyName });

            //学生表和班级表关联
            var userinfo = userlist.Join(classlist, x => x.ClassId, g => g.Id, (x, g) => new { x.StudentId, x.StudentName, x.ClassId, g.ClassName, g.SpecializedNum });

            //再和专业关联
            var userFullinfo = userinfo.Join(specializedlist, x => x.SpecializedNum, g => g.SpecializedNum, (x, g) => new { x.StudentId, x.StudentName, x.ClassName, g.SpecializedName, g.AcademyName });


            //学生表的完整信息与宿舍表关联

            var dromfullinfo = dorminfo.Join(userFullinfo, x => x.StudentId, g => g.StudentId, (x, g) => new { dorminfo = x, g.ClassName, g.StudentName, g.SpecializedName, g.AcademyName });

            if (!string.IsNullOrEmpty(query.keyword))
            {
                dromfullinfo.Where(x => x.dorminfo.StudentId == query.keyword || x.StudentName.Contains(query.keyword) || x.ClassName.Contains(query.keyword) || x.SpecializedName.Contains(query.keyword));
            }

            var paging = dromfullinfo.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).OrderByDescending(x => x.dorminfo.CreatedAt).ToList();

            return new
            {
                Code = 200,
                Msg = "获取宿舍列表成功",
                Data = paging,
                Page = new PageDto
                {
                    pageIndex = query.PageIndex,
                    pageSize = query.PageSize,
                    OnThisPage = paging.Count(),
                    Count = dromfullinfo.Count()
                }
            }.SerializeObject();

        }

        public async Task<string> UpdateDormitory(Guid id, DormitoryDTO dormitoryDTO)
        {
            var entity = await _dormRes.GetByIdAsync(id);

            if (entity != null)
            {
                //宿舍修改不做重复判断 修改宿舍不对学生的学号做修改所有不会有重复
                //要进行宿舍是否满人判断或者前端下拉款 选择未满人的宿舍
                entity.BuildingNum = dormitoryDTO.BuildingNum;
                entity.DormitoryNum = dormitoryDTO.DormitoryNum;
                entity.isDormAdmin = dormitoryDTO.isDormAdmin;

                var dorminfo = await _dormRes.UpdateAsync(entity);
                return new
                {
                    Code = 200,
                    Msg = "修改宿舍记录成功",
                    Data = dorminfo
                }.SerializeObject();
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "修改宿舍记录失败",
                    Data = entity
                }.SerializeObject();
            }

        }
    }
}