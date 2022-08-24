using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Application.Common.Interface;
using Web.Domain.Base;

namespace Web.Infrastructure.Persistence.Repositoty
{

    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly TeaachAdminDbContext _db;
        private readonly DbSet<T> _table;
        private readonly ISessionUserService _sessionUserService;
        public EfRepository(TeaachAdminDbContext db, ISessionUserService sessionUserService)
        {
            _sessionUserService = sessionUserService;
            _db = db;
            _table = _db.Set<T>();
        }

        public IQueryable<T> Table => _table.AsNoTracking();


        public async Task<T> AddAsync(T entity)
        {

            entity.IsDeleted = false;
            entity.IsActived = true;
            entity.CreatedBy = _sessionUserService.UserId;
            entity.UpdatedBy = _sessionUserService.UserId;
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            await _table.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddBulkAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                entity.CreatedBy = _sessionUserService.UserId;
                entity.UpdatedBy = _sessionUserService.UserId;

                entity.IsActived = true;
                entity.IsDeleted = false;

            }
            await _table.AddRangeAsync(entities);
            await _db.SaveChangesAsync();
            return entities;
        }

        public async Task<T> DeleteAsync(Guid id, bool canHardDelete)
        {
            var entity = await this.GetByIdAsync(id);
            if (entity != null)
            {
                await this.DeleteAsync(entity, canHardDelete);
                return entity;
            }
            else
            {
                return entity!;
            }


        }

        public async Task DeleteAsync(T entity, bool canHardDelete = false)
        {
            // 如果为true，则实际删除记录，否则为伪删除/软删除;默认为false
            if (canHardDelete)
            {
                _table.Remove(entity);
                await _db.SaveChangesAsync();
            }
            else
            {
                entity.IsDeleted = true;
                entity.UpdatedBy = _sessionUserService.UserId;
                await this.UpdateAsync(entity);
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, bool canHardDelete = false)
        {
            // 如果为true，则实际删除记录，否则为伪删除/软删除;默认为false
            if (canHardDelete)
            {
                _table.RemoveRange(entities);
                await _db.SaveChangesAsync();
            }
            else
            {
                foreach (var entity in entities)
                {
                    entity.IsDeleted = true;
                    entity.UpdatedAt = DateTime.Now;
                    entity.UpdatedBy = _sessionUserService.UserId;
                }
                _table.UpdateRange(entities);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await _table.FindAsync(id);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = _sessionUserService.UserId;
            _table.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}