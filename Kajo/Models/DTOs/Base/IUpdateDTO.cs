using Kajo.Models.Entities;

namespace Kajo.Models.Dtos.Base
{
    public interface IUpdateDto<TEntity>
    {
        void Update(TEntity entity);
    }
}
