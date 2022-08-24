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

    public class AcademyInfoServices : IAcademyInfoServices
    {

        private readonly IRepository<AcademyInfo> _academyRepository;

        public AcademyInfoServices(IRepository<AcademyInfo> academyRepository)
        {
            _academyRepository = academyRepository;
        }
        public async Task<string> AddAcade(AcadeDTO AcadeDTO)
        {

            var count = _academyRepository.Table.Count();
            var isHas = _academyRepository.Table.Where(x => x.AcademyName == AcadeDTO.AcademyName).FirstOrDefault();
            if (isHas != null)
            {
                return new
                {
                    Code = 402,
                    Msg = "添加学院 失败该学院已存在",
                    Data = isHas
                }.SerializeObject();
            }

            var entity = await _academyRepository.AddAsync(new AcademyInfo
            {
                AcademyName = AcadeDTO.AcademyName,
                AcademyNum = (count + 1).ToString()
            });

            return new
            {
                Code = 200,
                Msg = "添加学院成功",
                Data = entity
            }.SerializeObject();
        }

        public async Task<string> DeleteAcade(Guid id)
        {
            var entity = await _academyRepository.DeleteAsync(id);

            return new
            {
                Code = 200,
                Msg = "删除学院成功",
                Data = entity
            }.SerializeObject();
        }

        public string GetListOrByAcadeName(PageFromQuery query)
        {
            var keyword = query.keyword;
            var entity = _academyRepository.Table.Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(keyword))
            {
                entity = entity.Where(x => x.AcademyName.Contains(keyword) || x.AcademyNum == keyword);
            }

            var paging = entity.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);

            return new
            {
                Code = 200,
                Msg = "获取学院信息成功",
                Data = paging,
                Page = new PageDto
                {
                    pageIndex = query.PageIndex,
                    pageSize = query.PageSize,
                    OnThisPage = paging.Count(),
                    Count = entity.Count()
                }
            }.SerializeObject();

        }

        public async Task<string> UpdatedAcadeName(Guid id, AcadeDTO AcadeDTO)
        {

            var entity = await _academyRepository.GetByIdAsync(id);

            if (entity != null)
            {
                var isHas = _academyRepository.Table.Where(x => (x.AcademyName == AcadeDTO.AcademyName && x.Id != id) || (x.AcademyNum == AcadeDTO.AcademyNum && x.Id != id)).FirstOrDefault();
                if (isHas != null)
                {
                    return new
                    {
                        Code = 402,
                        Msg = "更新学院信息失败,学院已存在",
                        Data = isHas
                    }.SerializeObject();

                }

                entity.AcademyName = AcadeDTO.AcademyName;
                entity.AcademyNum = AcadeDTO!.AcademyNum!;
                entity = await _academyRepository.UpdateAsync(entity);
            }
            return new
            {
                Code = 200,
                Msg = "更新学院信息成功",
                Data = entity
            }.SerializeObject();

        }
    }
}