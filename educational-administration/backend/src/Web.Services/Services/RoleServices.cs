using Web.Domain.Entity;
using Web.Application.Utils;
using Web.Application.ResDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.Common.Interface;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;

namespace Web.Services.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRepository<AppRole> _appRole;

        public RoleServices(IRepository<AppRole> appRole)
        {
            _appRole = appRole;
        }

        // 返回的状态值：

        // 200：请求成功
        // 400：传入格式错误的数据或传入为空的数据
        // 401：登录验证过期
        // 404：请求失败
        // 405：响应失败


        // 添加方法 异步
        // 传入：指定类型的 Dto (数据传输对象)
        // 返回：string
        // data：添加完成后的数据
        // 成功返回 200
        // 失败返回 405
        // 输入为空返回 400
        public async Task<string> AddRole(RoleDTO role)
        {
            if (role.RoleName == "")
            {
                var res = new
                {
                    code = 400,
                    data = "",
                    msg = "输入为空！添加失败！"
                };

                return res.SerializeObject();
            }
            else
            {
                var boolRole = _appRole.Table.Where(x => x.RoleName == role.RoleName && x.IsDeleted == false).FirstOrDefault();

                if (boolRole == null)
                {
                    var entity = new AppRole { };

                    entity.RoleName = role.RoleName;


                    entity.RoleName = role.RoleName;

                    await _appRole.AddAsync(entity);

                    var res = new
                    {
                        code = 200,
                        data = entity,
                        msg = "添加成功！"
                    };

                    return res.SerializeObject();
                }
                else
                {
                    var res = new
                    {
                        code = 405,
                        data = "",
                        msg = "角色已存在，添加失败！"
                    };

                    return res.SerializeObject();
                }
            }
        }


        // 删除方法 异步
        // 传入：需要删除的用户的 属性 RoleName
        // 返回：string
        // data：无
        // 成功返回 200
        // 失败返回 405
        public async Task<string> DeleteRole(Guid id)
        {
            var entity = _appRole.Table.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();

            if (entity != null)
            {
                await _appRole.DeleteAsync(entity);

                var res = new
                {
                    code = 200,
                    data = "",
                    msg = "删除成功！"
                };

                return res.SerializeObject();
            }
            else
            {
                var res = new
                {
                    code = 405,
                    data = "",
                    msg = "角色不存在，删除失败！"
                };

                return res.SerializeObject();
            }
        }


        // 查询方法 同步
        // 传入：需要查询的指定类型的 RoleName（角色名） 或 （默认）没有输入
        // 返回：string，分页信息
        // data：（指定名称模糊查询）查询到的数据内容 或 （默认）返回角色列表
        // 成功返回 200
        // 失败返回 405
        public string GetListOrByRoleName(PageFromQuery query)
        {
            var list = _appRole.Table.Where(x => x.IsDeleted == false);

            // 判断 keyword 是否为空，为空返回所有角色列表
            if (!string.IsNullOrEmpty(query.keyword))
            {
                list = list.Where(x => x.RoleName.Contains(query.keyword.Trim()));
            }

            // 分页
            var paging = list.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();

            return new
            {
                Code = 200,
                Msg = "获取角色数据成功",
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


        // 通过 ID 查找 异步
        // 传入：需要查找的用户的 主键ID
        // 返回：string
        // data：对应 ID 的数据
        // 成功返回 200
        // 失败返回 405
        public async Task<string> GetbyId(Guid id)
        {
            var entity = await _appRole.GetByIdAsync(id);

            if (entity != null)
            {
                var res = new
                {
                    code = 200,
                    data = entity,
                    msg = "查询成功！"
                };

                return res.SerializeObject();
            }
            else
            {
                var res = new
                {
                    code = 405,
                    data = "",
                    msg = "角色不存在！查询失败！"
                };

                return res.SerializeObject();
            }
        }


        // 修改方法 异步
        // 传入：需要修改的指定类型的 id（角色Id）以及 指定类型的 RoleName（角色名）
        // 返回：string
        // data：修改完成后的数据
        // 成功返回 200
        // 失败返回 405
        // 输入为空返回 400
        public async Task<string> UpdatedRoleName(Guid id, RoleDTO role)
        {
            if (role.RoleName == "")
            {
                var res = new
                {
                    code = 400,
                    data = "",
                    msg = "没有传入修改值，修改失败！"
                };

                return res.SerializeObject();
            }
            else
            {
                var entity = await _appRole.GetByIdAsync(id);

                if (entity != null)
                {
                    if (entity.RoleName == role.RoleName)
                    {
                        var res = new
                        {
                            code = 405,
                            data = entity,
                            msg = "角色已存在，修改值与之前没有什么不同，修改失败！"
                        };

                        return res.SerializeObject();
                    }
                    else
                    {
                        entity.RoleName = role.RoleName;

                        await _appRole.UpdateAsync(entity);

                        var res = new
                        {
                            code = 200,
                            data = entity,
                            msg = "修改成功！"
                        };

                        return res.SerializeObject();
                    }
                }
                else
                {
                    var res = new
                    {
                        code = 405,
                        data = "",
                        msg = "角色不存在，修改失败！"
                    };

                    return res.SerializeObject();
                }
            }
        }
    }
}