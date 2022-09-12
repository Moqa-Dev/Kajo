using Kajo.Models.Dtos.Base;
using Kajo.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Kajo.Services.Base
{
    public interface IRestService<TId, TEntity, TCreateDto, TUpdateDto> 
        where TEntity : EntityBase<TId>
        where TCreateDto : ICreateDto<TEntity>
        where TUpdateDto : IUpdateDto<TEntity>
    {
        Task<DbSet<TEntity>> GetAll();
        Task<TEntity> GetById(TId id);
        Task<TEntity> Create(String userID, TCreateDto createDto);
        Task<TEntity> Update(TId id, TUpdateDto updateDto);
        void Delete(TId id);
    }
}
