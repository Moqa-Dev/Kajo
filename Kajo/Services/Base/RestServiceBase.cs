using Kajo.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using Kajo.Models.Dtos.Base;
using Kajo.Models.Entities.Base;
using Kajo.DataContext;

namespace Kajo.Services.Base
{
    public abstract class RestServiceBase<TId, TEntity, TCreateDto, TUpdateDto>
        : IRestService<TId, TEntity, TCreateDto, TUpdateDto>
        where TEntity : EntityBase<TId>
        where TCreateDto : ICreateDto<TEntity>
        where TUpdateDto : IUpdateDto<TEntity>
    {
        private readonly KajoContext _context;

        public RestServiceBase(KajoContext context)
        {
            _context = context;
        }

        public async Task<DbSet<TEntity>> GetAll()
        {
            return await Task.Run(() => _context.Set<TEntity>());
        }

        public async Task<TEntity> GetById(TId id)
        {
            return await GetEntityById(id);
        }

        public async Task<TEntity> Create(String userID, TCreateDto createDto)
        {
            var entity = createDto.ToEntity(userID);
            _context.Add<TEntity>(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TId id, TUpdateDto updateDto)
        {
            var entity = await GetEntityById(id);
            updateDto.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async void Delete(TId id)
        {
            var entity = await GetEntityById(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        protected async Task<TEntity> GetEntityById(TId id)
        {
            return await Task.Run(() =>
            {
                var entity = _context.Set<TEntity>().FindAsync(id).Result;
                if (entity == null)
                {
                    throw new ApiException("ID doesn't match any Entry!", ApiException.NOT_FOUND_CODE);
                }
                return entity;
            } );
        }
    }
}
