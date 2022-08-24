using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto.ServerDto
{
    public class StudentDTO
    {
        //学生Id
        public string? StudentId { get; set; }
        //学生名
        public string StudentName { get; set; } = null!;
        //性别
        public string Sex { get; set; } = null!;
        //身份证号
        public string IdNumber { get; set; } = null!;
        //入学时间
        public string EnrollmentTime { get; set; } = null!;
        //手机号
        public string? PhoneNumber { get; set; }
        //qq号
        public string? qqNumber { get; set; }
        //微信号
        public string? WeChat { get; set; }
        //家庭住址
        public string? Address { get; set; }
        //父名
        public string? FatherName { get; set; }
        //母名
        public string? MotherName { get; set; }
        //联系方式
        public string? ContactDetails { get; set; }
        //班级Id
        public Guid ClassId { get; set; }
    }
}