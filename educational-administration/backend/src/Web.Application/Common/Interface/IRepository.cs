using Web.Domain.Base;

namespace Web.Application.Common.Interface
{

    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Table { get; }


        /// <summary>
        /// 根据Id获得实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// 批量新增添加
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> AddBulkAsync(IEnumerable<T> entities);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// 删除指定Id的实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> DeleteAsync(Guid id, bool canHardDelete = false);

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity, bool canHardDelete = false);

        /// <summary>
        /// 删除指定的一组实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteRangeAsync(IEnumerable<T> entities, bool canHardDelete = false);



    }
}