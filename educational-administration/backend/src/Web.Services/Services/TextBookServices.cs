using Web.Application.Common.Interface;
using Web.Application.Common.Interface.IServer;
using Web.Application.ReqDto;
using Web.Application.ReqDto.ServerDto;
using Web.Application.ResDto;
using Web.Application.Utils;
using Web.Domain.Entity;

namespace Web.Services.Services
{
    public class TextBookServices : ITextBookServices
    {
        private readonly IRepository<TextBookInfo> _textbook;

        public TextBookServices(IRepository<TextBookInfo> textbook)
        {
            _textbook = textbook;
        }

        public Task<string> GetbyId(Guid id)
        {
            throw new NotImplementedException();
        }

        // 查询教材列表或者（指定名称模糊查询）
        // 传入：需要查询的的教材的 TextBookName，分页信息
        // 返回：IQueryable
        public string GetListOrByTextBookName(PageFromQuery query)
        {
            var list = _textbook.Table.Where(x => x.IsDeleted == false);

            // 判断keyword 是否为空，为空则查询所有
            if (!string.IsNullOrEmpty(query.keyword))
            {
                list = list.Where(x => x.TextBookName.Contains(query.keyword));
            }

            // 分页
            var paging = list.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).OrderByDescending(x => x.CreatedAt).ToList();

            return new
            {
                Code = 200,
                Msg = "获取用户数据成功",
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

        // 添加方法 异步
        // 传入：指定类型的 Dto (数据传输对象)
        // 返回：string
        // data：添加完成后的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> AddTextBook(TextBookDTO textBookDTO)
        {
            var isExist = _textbook.Table.Where(x => x.TextBookName == textBookDTO.TextBookName).FirstOrDefault() == null;
            // 判断是否存在
            if (isExist)
            {
                var entity = new TextBookInfo { };

                entity.TextBookName = textBookDTO.TextBookName;
                entity.Press = textBookDTO.Press;
                entity.Price = textBookDTO.Price;
                entity.ContactNumber = textBookDTO.ContactNumber;

                await _textbook.AddAsync(entity);

                return new
                {
                    Code = 200,
                    Msg = "添加成功",
                    Data = entity
                }.SerializeObject();
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "添加失败，教材已存在"
                }.SerializeObject();
            }
        }

        // 修改方法 异步
        // 传入：需要修改的教材的 主键 ID 和修改内容
        // 返回：string
        // data：修改完成后的数据
        // 成功返回 200
        // 失败返回 402
        public async Task<string> UpdateTextBook(Guid id, TextBookDTO textBookDTO)
        {
            var tmp = _textbook.Table.Where(x => x.Id == id).FirstOrDefault();
            // 判断是否存在
            if (tmp != null)
            {
                var name = _textbook.Table.Where(x => x.TextBookName == textBookDTO.TextBookName && x.Id != id).FirstOrDefault() == null;

                if (name)
                {
                    tmp.TextBookName = textBookDTO.TextBookName;
                    tmp.Press = textBookDTO.Press;
                    tmp.Price = textBookDTO.Price;
                    tmp.ContactNumber = textBookDTO.ContactNumber;

                    var entity = await _textbook.UpdateAsync(tmp);

                    return new
                    {
                        Code = 200,
                        Msg = "修改数据成功",
                        Data = entity
                    }.SerializeObject();
                }
                else
                {
                    return new
                    {
                        Code = 402,
                        Msg = "修改数据失败，教材已存在"
                    }.SerializeObject();
                }
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "修改数据失败，教材不存在"
                }.SerializeObject();
            }
        }

        // 删除方法 异步
        // 传入：需要删除的教材的 主键ID
        // 返回：string
        // data：无
        // 成功返回 200
        // 失败返回 402
        public async Task<string> DeleteTextBook(Guid id)
        {
            var isExist = _textbook.Table.Where(x => x.Id == id).FirstOrDefault() == null;
            // 判断是否存在
            if (!isExist)
            {
                await _textbook.DeleteAsync(id, false);

                return new
                {
                    Code = 200,
                    Msg = "删除成功"
                }.SerializeObject();
            }
            else
            {
                return new
                {
                    Code = 402,
                    Msg = "删除失败，教材不存在"
                }.SerializeObject();
            }
        }
    }
}