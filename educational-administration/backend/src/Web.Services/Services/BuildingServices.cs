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
    public class BuildingServices : IBuildingServices
    {
        private readonly IRepository<BuildingInfo> _buidingRes;



        public BuildingServices(IRepository<BuildingInfo> buidingRes)
        {
            _buidingRes = buidingRes;

        }

        /// <summary>
        /// 新增楼栋
        /// </summary>
        /// <param name="buildingDTO"></param>
        /// <returns></returns>
        public async Task<string> AddBuilding(BuildingDTO buildingDTO)
        {
            var isHas = _buidingRes.Table.Where(x => x.BuildingName == buildingDTO.BuildingName && x.BuildingNum == buildingDTO.BuildingNum && x.floor == buildingDTO.Floor && x.IsDeleted == false).FirstOrDefault();
            if (isHas != null)
            {
                return new
                {
                    Code = 402,
                    Msg = "要添加的楼栋名称,编号，楼层已存在",
                    Data = isHas
                }.SerializeObject();
            }

            var entity = await _buidingRes.AddAsync(new BuildingInfo
            {
                BuildingName = buildingDTO.BuildingName,
                BuildingNum = buildingDTO.BuildingNum,
                floor = buildingDTO.Floor
            });

            return new
            {
                Code = 200,
                Msg = "新增楼栋成功",
                Data = entity
            }.SerializeObject();
        }

        /// <summary>
        /// 删除楼栋
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteBuilding(Guid id)
        {
            var entity = await _buidingRes.DeleteAsync(id);
            if (entity != null)
            {
                return new
                {
                    Code = 200,
                    Msg = "删除楼栋成功",
                    Data = entity
                }.SerializeObject();
            }
            else
            {
                return new
                {
                    Code = 200,
                    Msg = "删除楼栋失败",
                    Data = ""
                }.SerializeObject();
            }

        }

        /// <summary>
        /// 查询楼栋信息列表
        /// </summary>
        /// <param name="pageDto"></param>
        /// <returns></returns>
        public string GetListOrByBuildingName(PageFromQuery query)
        {
            var list = _buidingRes.Table.Where(x => x.IsDeleted == false);

            // 判断keyword 是否为空，为空则查询所有
            if (!string.IsNullOrEmpty(query.keyword))
            {
                list = list.Where(x => x.BuildingName.Contains(query.keyword));
            }

            // 分页
            var paging = list.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();

            return new
            {
                Code = 200,
                Msg = "获取楼栋信息成功",
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

        /// <summary>
        /// 更新楼栋信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buildingDTO"></param>
        /// <returns></returns>
        public async Task<string> UpdatedBuilding(Guid id, BuildingDTO buildingDTO)
        {
            var entity = await _buidingRes.GetByIdAsync(id);

            if (entity != null)
            {
                // if (entity.BuildingName == buildingDTO.BuildingName && entity.BuildingNum == buildingDTO.BuildingNum)
                // {

                // }
                // var isHas = _buidingRes.Table.Where(x => x.floor == buildingDTO.Floor && x.BuildingName == buildingDTO.BuildingName && x.BuildingNum == buildingDTO.BuildingNum && x.Id != id);
                // if (isHas != null)
                // {
                //     return new
                //     {
                //         Code = 402,
                //         Msg = "要修改的楼栋名称或编号已存在",
                //         Data = isHas
                //     }.SerializeObject();
                // }

                entity.BuildingName = buildingDTO.BuildingName;
                entity.BuildingNum = buildingDTO.BuildingNum;
                entity.floor = buildingDTO.Floor;

                await _buidingRes.UpdateAsync(entity);
                return new
                {
                    Code = 200,
                    Msg = "更新楼栋信息 成功",
                    Data = entity
                }.SerializeObject();
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "更新楼栋信息 失败",
                    Data = entity
                }.SerializeObject();
            }

        }

    }
}