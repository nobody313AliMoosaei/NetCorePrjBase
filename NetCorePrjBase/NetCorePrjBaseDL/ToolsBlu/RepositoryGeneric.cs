using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NetCorePrjBaseDL.ApplicationDbContext;
namespace NetCorePrjBaseDL.ToolsBlu
{
    public class RepositoryGeneric<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _dbSet;
        private Application_DbContext _context;
        public RepositoryGeneric(Application_DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Entity
        {
            get
            {
                return _dbSet.AsQueryable();
            }
        }
        public virtual async Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? _where = null)
        {
            try
            {
                if (_where == null)
                    _where = _where = o => true;
                return await _dbSet.Where(_where).ToListAsync<TEntity>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public virtual IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>>? _where = null)
        {
            try
            {
                if (_where == null)
                    _where = _where = o => true;
                return _dbSet.Where(_where).AsQueryable<TEntity>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public virtual bool InsertEntity(TEntity Entity)
        {
            _dbSet.Add(Entity);
            return true;
        }
        public virtual async Task<bool> InsertEntity(TEntity Entity, bool IsCommit = false)
        {
            _dbSet.Add(Entity);
            if (IsCommit)
                await CommitAsync();
            return true;
        }
        public virtual async Task<bool> InsertEntity(List<TEntity> Entity, bool IsCommit = false)
        {
            _dbSet.AddRange(Entity);
            if (IsCommit)
                await CommitAsync();
            return true;
        }

        public virtual bool InsertEntity(List<TEntity> Entities)
        {
            _dbSet.AddRange(Entities);
            return true;
        }
        public virtual bool UpdateEntity(TEntity Entity)
        {
            _dbSet.Update(Entity);
            return true;
        }
        public virtual bool UpdateEntity(List<TEntity> Entities)
        {
            _dbSet.UpdateRange(Entities);
            return true;
        }
        public virtual bool DeleteEntity(TEntity Entity)
        {
            _dbSet.Remove(Entity);
            return true;
        }
        public virtual bool DeleteEntity(List<TEntity> Entity)
        {
            _dbSet.RemoveRange(Entity);
            return true;
        }
        public virtual async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public virtual void Commit()
        {
            _context.SaveChanges();
        }
    }
}
