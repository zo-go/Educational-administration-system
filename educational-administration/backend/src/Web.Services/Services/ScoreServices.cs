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
    public class ScoreServices : IScoreServices
    {
        //注入分数表，学生表,课程表，学期表,班级表
        public readonly IRepository<ScoreInfo> _scoreInfo;
        public readonly IRepository<StudentInfo> _studentInfo;
        public readonly IRepository<CourseInfo> _courseInfo;
        public readonly IRepository<TermInfo> _termInfo;
        public readonly IRepository<ClassInfo> _classInfo;

        public ScoreServices(IRepository<ScoreInfo> scoreInfo, IRepository<StudentInfo> studentInfo, IRepository<CourseInfo> courseInfo, IRepository<TermInfo> termInfo, IRepository<ClassInfo> classInfo)
        {
            _scoreInfo = scoreInfo;
            _classInfo = classInfo;
            _courseInfo = courseInfo;
            _termInfo = termInfo;
            _studentInfo = studentInfo;
        }
        // 新增分数 异步
        // 传入：分数DTO
        // 返回：string
        // data：code，msg，data
        // 成功返回 200
        // 失败返回 402
        public async Task<string> AddScore(ScoreDTO scoreDTO)
        {
            var termname = _termInfo.Table.Where(x => x.TermName == scoreDTO.SemesterName).FirstOrDefault();
            var isExist = _scoreInfo.Table.Where(x => x.StudentId == scoreDTO.StudentId && x.SubjectId == scoreDTO.SubjectId &&
            x.Score == scoreDTO.Score &&
            x.ClassId == scoreDTO.ClassId && x.SemesterId == termname!.Id).FirstOrDefault();
            // 判断是否存在
            if (isExist == null)
            {
                var entity = new ScoreInfo { };

                var tmp = _termInfo.Table.Where(x => x.TermName == scoreDTO.SemesterName).FirstOrDefault();
                if (tmp != null)
                {
                    entity.StudentId = scoreDTO.StudentId;
                    entity.SubjectId = scoreDTO.SubjectId;
                    entity.Score = scoreDTO.Score;
                    entity.SemesterId = tmp.Id;
                    entity.ClassId = scoreDTO.ClassId;

                    await _scoreInfo.AddAsync(entity);

                    return new
                    {
                        Code = 200,
                        Msg = "添加分数成功",
                        Data = entity
                    }.SerializeObject();
                }
                else
                {
                    return new
                    {
                        Code = 402,
                        Msg = "所选学期不存在",
                        Data = entity
                    }.SerializeObject();
                }


            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "添加失败，该条记录已存在"
                }.SerializeObject();
            }
        }

        // 删除分数 异步
        // 传入：分数DTO
        // 返回：string
        // data：code，msg，data
        // 成功返回 200
        // 失败返回 402
        public async Task<string> DeleteScore(Guid id)
        {
            var entity = await _scoreInfo.GetByIdAsync(id);
            if (entity != null)
            {
                await _scoreInfo.DeleteAsync(entity);
                return new
                {
                    Code = 200,
                    Msg = "删除分数记录成功",
                    Data = entity
                }.SerializeObject();
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "删除失败，该条记录不存在"
                }.SerializeObject();
            }
        }


        // 查询分数列表或者（指定名称模糊查询）
        // 传入：需要查询的的课程的 courseName，分页信息
        // 返回：string，分页信息
        // data：查询到的内容
        // 成功返回 200
        public string GetListOrByScoreName(PageFromQuery query)
        {
            var keyword = query.keyword;
            var scoreTable = _scoreInfo.Table.Where(x => x.IsDeleted == false);


            var studnetTable = _studentInfo.Table.Where(x => x.IsDeleted == false);

            var termTable = _termInfo.Table.Where(x => x.IsDeleted == false);

            if (keyword != null && keyword != "")
            {
                studnetTable = _studentInfo.Table.Where(x => x.IsDeleted == false
                && (x.StudentName != null && x.StudentName.Contains(keyword))
                || (x.StudentId != null && x.StudentId.Contains(keyword)));

                // termTable = _termInfo.Table.Where(x => x.IsDeleted == false
                // && (x.TermName != null && x.TermName.Contains(keyword))
                // );
            }

            var classTable = _classInfo.Table.Where(x => x.IsDeleted == false);



            var courseTable = _courseInfo.Table.Where(x => x.IsDeleted == false);

            var scoreclass = scoreTable.Join(classTable, x => x.ClassId, p => p.Id, (x, p) => new
            {
                scoreInfo = x,
                classinfo = p
            });

            var scoreClassStudnet = scoreclass.Join(studnetTable, x => x.scoreInfo.StudentId, p => p.Id, (x, y) => new
            {
                scoreInfo = x.scoreInfo,
                classInfo = x.classinfo,
                studnetInfo = y

            });
            var scoreClassStudnetTerm = scoreClassStudnet.Join(termTable, x => x.scoreInfo.SemesterId, p => p.Id, (x, y) => new
            {
                scoreInfo = x.scoreInfo,
                classInfo = x.classInfo,
                studnetInfo = x.studnetInfo,
                termInfo = y
            });

            var result = scoreClassStudnetTerm.Join(courseTable, x => x.scoreInfo.SubjectId, p => p.Id, (x, y) => new
            {
                scoreInfo = x.scoreInfo,
                classInfo = x.classInfo,
                studnetInfo = x.studnetInfo,
                termInfo = x.termInfo,
                courseInfo = y
            });
            var list = result.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).OrderByDescending(x => x.scoreInfo.CreatedAt).ToList();

            if (result != null)
            {
                return new
                {
                    Code = 200,
                    Msg = "获取分数列表成功",
                    Data = list
                }.SerializeObject();
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "获取分数列表失败",
                    Data = list
                }.SerializeObject();
            }


        }

        // 更新分数 异步
        // 传入：id，分数DTO
        // 返回：string
        // data：code，msg，data
        // 成功返回 200
        // 失败返回 402
        public async Task<string> UpdateScore(Guid id, ScoreDTO scoreDTO)
        {
            var entity = await _scoreInfo.GetByIdAsync(id);
            if (entity != null)
            {
                var TermName = scoreDTO.SemesterName;
                var tmp = _termInfo.Table.Where(x => x.TermName == TermName).FirstOrDefault();
                if (tmp != null)
                {
                    entity.SemesterId = tmp.Id;
                    entity.Score = scoreDTO.Score;
                    entity.SubjectId = scoreDTO.SubjectId;
                    entity.StudentId = scoreDTO.StudentId;
                    entity.ClassId = scoreDTO.ClassId;
                    await _scoreInfo.UpdateAsync(entity);
                    return new
                    {
                        Code = 200,
                        Msg = "更新分数记录成功",
                        Data = entity
                    }.SerializeObject();
                }
                else
                {
                    return new
                    {
                        Code = 402,
                        Msg = "所选学期不存在",
                        Data = entity
                    }.SerializeObject();
                }




            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "更新分数记录失败",
                }.SerializeObject();

            }

        }
    }
}