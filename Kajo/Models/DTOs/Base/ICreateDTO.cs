using System;

namespace Kajo.Models.Dtos.Base
{
    public interface ICreateDto<TEntity>
    {
        TEntity ToEntity(String userID);
    }
}
