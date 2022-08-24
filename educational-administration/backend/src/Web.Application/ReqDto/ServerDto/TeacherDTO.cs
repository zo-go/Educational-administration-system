using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Application.ReqDto.ServerDto
{
    public class TeacherDTO
    {
        //工号Id(用户id)
        //不能修改教师工号即用户id
        public string? WorkNumber { get; set; }
        //添加的教师角色 辅导员或教师
        public string? RoleName { get; set; }
        //教师名
        public string TeacherName { get; set; } = null!;
        //身份证号
        public string IdNumber { get; set; } = null!;
        //学院号
        public string? AcademyNum { get; set; } = null!;
        //性别
        public string Sex { get; set; } = null!;
        //手机号
        public string? PhoneNumber { get; set; }
        //qq号
        public string? qqNumber { get; set; }
        //微信号
        public string? WeChat { get; set; }
        //家庭住址
        public string? Address { get; set; }
    }
}