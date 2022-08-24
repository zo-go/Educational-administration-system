using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web.Application.Common.Interface;
using Web.Application.Utils;
using Web.Domain.Entity;
using Web.Infrastructure.Persistence;

namespace Web.Infrastructure
{
    public static class ApplicationStartupExtensions
    {
        public static async void MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<TeaachAdminDbContext>();

                // 自动同步迁移文件到数据库服务器，前提是必须先（手动）生成迁移文件
                context.Database.Migrate();

                var appUserRes = services.GetRequiredService<IRepository<AppUser>>();
                var appRoleRes = services.GetRequiredService<IRepository<AppRole>>();
                var TeaRes = services.GetRequiredService<IRepository<TeacherInfo>>();
                var StuRes = services.GetRequiredService<IRepository<StudentInfo>>();
                var buidAdress = services.GetRequiredService<IRepository<BuildingInfo>>();
                //院系
                var DepRes = services.GetRequiredService<IRepository<AcademyInfo>>();
                //专业
                var SpecializedRes = services.GetRequiredService<IRepository<SpecializedInfo>>();
                var ClassRes = services.GetRequiredService<IRepository<ClassInfo>>();
                var ScoreRes = services.GetRequiredService<IRepository<ScoreInfo>>();
                var BookRes = services.GetRequiredService<IRepository<TextBookInfo>>();
                var CourRes = services.GetRequiredService<IRepository<CourseInfo>>();
                var TermInfo = services.GetRequiredService<IRepository<TermInfo>>();

                // 如果没有任何角色，则生成种子数据
                if (!appRoleRes.Table.Any())
                {
                    var role = new AppRole
                    {
                        RoleName = "管理员"
                    };
                    var role2 = new AppRole
                    {
                        RoleName = "学生"
                    };
                    var role3 = new AppRole
                    {
                        RoleName = "教师"
                    };
                    var role4 = new AppRole
                    {
                        RoleName = "辅导员"
                    };

                    var termList = new List<TermInfo>();

                    termList.Add(new TermInfo
                    {
                        TermName = "第一学期",
                    });
                    termList.Add(new TermInfo
                    {
                        TermName = "第二学期",
                    });
                    termList.Add(new TermInfo
                    {
                        TermName = "第三学期",
                    });
                    termList.Add(new TermInfo
                    {
                        TermName = "第四学期",
                    });
                    termList.Add(new TermInfo
                    {
                        TermName = "第五学期",
                    });
                    termList.Add(new TermInfo
                    {
                        TermName = "第六学期",
                    });
                    await TermInfo.AddBulkAsync(termList);

                    await appRoleRes.AddAsync(role);
                    await appRoleRes.AddAsync(role2);
                    await appRoleRes.AddAsync(role3);
                    await appRoleRes.AddAsync(role4);

                    var buiding = new BuildingInfo
                    {
                        BuildingName = "团结里11栋",
                        BuildingNum = "11",
                        floor = "1"
                    };

                    var buiding2 = new BuildingInfo
                    {
                        BuildingName = "团结里12栋",
                        BuildingNum = "12",
                        floor = "1"
                    };
                    var buiding3 = new BuildingInfo
                    {
                        BuildingName = "团结里13栋",
                        BuildingNum = "13",
                        floor = "1"
                    };
                    var buiding4 = new BuildingInfo
                    {
                        BuildingName = "团结里14栋",
                        BuildingNum = "14",
                        floor = "1"
                    };
                    var buiding5 = new BuildingInfo
                    {
                        BuildingName = "团结里15栋",
                        BuildingNum = "15",
                        floor = "1"
                    };
                    var buiding6 = new BuildingInfo
                    {
                        BuildingName = "团结里10栋",
                        BuildingNum = "10",
                        floor = "1"

                    };

                    await buidAdress.AddAsync(buiding);
                    await buidAdress.AddAsync(buiding2);
                    await buidAdress.AddAsync(buiding3);
                    await buidAdress.AddAsync(buiding4);
                    await buidAdress.AddAsync(buiding5);
                    await buidAdress.AddAsync(buiding6);


                    // 如果没有任何学院，则生成种子数据
                    if (!DepRes.Table.Any())
                    {
                        var department = new List<AcademyInfo>();

                        department.Add(new AcademyInfo
                        {
                            AcademyName = "软件工程学院",
                            AcademyNum = "40"
                        });
                        department.Add(new AcademyInfo
                        {
                            AcademyName = "城乡建筑学院",
                            AcademyNum = "41"
                        });
                        department.Add(new AcademyInfo
                        {

                            AcademyName = "医学护理学院",
                            AcademyNum = "42"
                        });

                        await DepRes.AddBulkAsync(department);
                    }

                    // 如果没有任何专业，则生成种子数据
                    if (!SpecializedRes.Table.Any())
                    {
                        var major = new List<SpecializedInfo>();

                        major.Add(new SpecializedInfo
                        {
                            SpecializedName = "软件技术",
                            SpecializedNum = "11",
                            AcademyNum = "40"
                        });
                        major.Add(new SpecializedInfo
                        {

                            SpecializedName = "Web前端",
                            SpecializedNum = "12",
                            AcademyNum = "40"
                        });

                        major.Add(new SpecializedInfo
                        {
                            SpecializedName = "搬砖",
                            SpecializedNum = "13",
                            AcademyNum = "41"
                        });
                        major.Add(new SpecializedInfo
                        {

                            SpecializedName = "混水泥",
                            SpecializedNum = "14",
                            AcademyNum = "41"
                        });

                        await SpecializedRes.AddBulkAsync(major);
                    }

                    // 如果没有任何教师，则生成种子数据
                    if (!TeaRes.Table.Any())
                    {
                        var teachers = new List<TeacherInfo>();

                        teachers.Add(new TeacherInfo
                        {
                            WorkNumber = "T" + RandomGeneration.generateStudentNumber("35", "77", "26", "43", 50)[0],
                            TeacherName = "曹非或",
                            Sex = "男",
                            IdNumber = "420101199307224991",
                            PhoneNumber = "15254506922",
                            WeChat = "gfihn_23746",
                            qqNumber = "675488635",
                            Address = "中国",
                            AcademyNum = "40"
                        });
                        teachers.Add(new TeacherInfo
                        {
                            WorkNumber = "T" + RandomGeneration.generateStudentNumber("35", "77", "26", "43", 50)[1],
                            TeacherName = "蒋昊雅",
                            Sex = "女",
                            IdNumber = "420101198509224426",
                            PhoneNumber = "13701503128",
                            WeChat = "lxkhf_85673",
                            qqNumber = "348765232",
                            Address = "中国",
                            AcademyNum = "40"
                        });

                        await TeaRes!.AddBulkAsync(teachers);
                    }

                    // 如果没有任何班级，则生成种子数据
                    if (!ClassRes.Table.Any())
                    {
                        var classes = new List<ClassInfo>();
                        var teacherOne = TeaRes.Table.FirstOrDefault();
                        classes.Add(new ClassInfo
                        {
                            ClassName = "软件一班",
                            ClassNum = "01",
                            CounselorId = teacherOne!.WorkNumber!,
                            SpecializedNum = "11",

                        });

                        await ClassRes.AddBulkAsync(classes);
                    }

                    // 如果没有任何教材，则生成种子数据
                    if (!BookRes.Table.Any())
                    {
                        var books = new List<TextBookInfo>();

                        books.Add(new TextBookInfo
                        {
                            TextBookName = "Node.js实战",
                            Press = "69.00",
                            Price = "人民邮电出版社",
                        });
                        books.Add(new TextBookInfo
                        {
                            TextBookName = "Vue.js 实战",
                            Press = "79.00",
                            Price = "清华大学出版社",
                        });
                        books.Add(new TextBookInfo
                        {
                            TextBookName = "CAD工程制图(中文版)",
                            Press = "25.40",
                            Price = "电子工业出版社",
                        });
                        books.Add(new TextBookInfo
                        {
                            TextBookName = "系统医学原理",
                            Press = "60.00",
                            Price = "中国科学技术出版社",
                        });

                        await BookRes.AddBulkAsync(books);
                    }

                    // 如果没有任何课程，则生成种子数据
                    if (!CourRes.Table.Any())
                    {
                        var courses = new List<CourseInfo>();
                        var bookOne = TeaRes.Table.FirstOrDefault();
                        var teacherOne = TeaRes.Table.FirstOrDefault();
                        var specialized = SpecializedRes.Table.Where(x => x.SpecializedName == "软件技术").FirstOrDefault();
                        courses.Add(new CourseInfo
                        {
                            CourseName = "Node.js",
                            TeacherId = teacherOne!.WorkNumber,
                            TextBookId = bookOne!.Id,
                            SpecializedNum = specialized!.SpecializedNum
                        });

                        await CourRes.AddBulkAsync(courses);
                    }



                    // 如果没有任何学生，则生成种子数据
                    if (!StuRes.Table.Any())
                    {
                        var classes = ClassRes.Table.FirstOrDefault();
                        var student = new List<StudentInfo>();

                        student.Add(new StudentInfo
                        {

                            StudentId = RandomGeneration.generateStudentNumber("20", "44", "01", "03", 50)[0],
                            StudentName = "施合轩",
                            Sex = "男",
                            IdNumber = RandomGeneration.GetIdCode(),
                            PhoneNumber = "13994160644",
                            WeChat = "liux_87246",
                            qqNumber = "54768283",
                            Address = "中国",
                            FatherName = "",
                            MotherName = "",
                            ContactDetails = "",
                            ClassId = classes!.Id,
                            EnrollmentTime = "2022"

                        });
                        student.Add(new StudentInfo
                        {
                            StudentId = RandomGeneration.generateStudentNumber("20", "44", "01", "03", 50)[1],
                            StudentName = "李码璐",
                            Sex = "女",
                            IdNumber = RandomGeneration.GetIdCode(),
                            PhoneNumber = "13816908532",
                            WeChat = "lasdj_12783",
                            qqNumber = "3752375239",
                            Address = "中国",
                            FatherName = "",
                            MotherName = "",
                            ContactDetails = "",
                            ClassId = classes!.Id,
                            EnrollmentTime = "2022"
                        });

                        await StuRes!.AddBulkAsync(student);
                    }
                    // 如果没有任何用户，则生成种子数据
                    if (!appUserRes.Table.Any())
                    {
                        var admins = appRoleRes.Table.Where(x => x.RoleName == "管理员").FirstOrDefault();
                        var counselors = appRoleRes.Table.Where(x => x.RoleName == "辅导员").FirstOrDefault();
                        var teachers = appRoleRes.Table.Where(x => x.RoleName == "教师").FirstOrDefault();
                        var students = appRoleRes.Table.Where(x => x.RoleName == "学生").FirstOrDefault();

                        var teacherOne = TeaRes.Table.Where(x => x.TeacherName == "曹非或").FirstOrDefault();
                        var teacherTwo = TeaRes.Table.Where(x => x.TeacherName == "蒋昊雅").FirstOrDefault();

                        var studentOne = StuRes.Table.Where(x => x.StudentName == "施合轩").FirstOrDefault();
                        var studentTwo = StuRes.Table.Where(x => x.StudentName == "李码璐").FirstOrDefault();

                        var userlist = new List<AppUser>();

                        // 管理员
                        userlist.Add(new AppUser
                        {
                            UserName = "admin",
                            PassWord = "113",
                            RoleId = admins!.Id,

                        });
                        userlist.Add(new AppUser
                        {
                            UserName = "administartor",
                            PassWord = "113",
                            RoleId = admins!.Id,

                        });

                        // 辅导员
                        userlist.Add(new AppUser
                        {
                            UserName = teacherOne!.WorkNumber!,
                            PassWord = "123456",
                            RoleId = counselors!.Id
                        });

                        // 教师
                        userlist.Add(new AppUser
                        {
                            UserName = teacherTwo!.WorkNumber!,
                            PassWord = "123456",
                            RoleId = teachers!.Id
                        });

                        // 学生
                        userlist.Add(new AppUser
                        {
                            UserName = studentOne!.StudentId!,
                            PassWord = "123456",
                            RoleId = students!.Id
                        });
                        userlist.Add(new AppUser
                        {
                            UserName = studentTwo!.StudentId!,
                            PassWord = "123456",
                            RoleId = students!.Id
                        });


                        await appUserRes!.AddBulkAsync(userlist);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred migrating the DB: {ex.Message}");
            }
        }
    }
}