using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;

namespace Web.Application.Common.Interface.IServer
{
    public interface IUserServices
    {
        //上传用户信息
        Task<string> uploadExcel(string[] ExcelContent);

        //新增用户
        Task<string> AddUser(UserDTO user);

        //修改用户(根据id)
        Task<string> UpdateUser(Guid id, UserDTO user);

        //查询用户列表或者（指定名称模糊查询）
        string GetListOrByUserName(PageFromQuery query);
        //查询用户（根据id）
        Task<string> GetbyId(Guid id);

        //删除用户(根据id)
        Task<string> DeleteUser(Guid id);
        //根据openid获取是否存在openid
        string GetOpenId(string OpenId);

        //对已存在用户但不存在openid进行新增openid
        Task<string> AddOpenid(UserOpenIdDTO openDto);

        string GetByStudentInfo(UserForAuth user);
    }
}