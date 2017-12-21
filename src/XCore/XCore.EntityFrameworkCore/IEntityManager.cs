using System;
using System.Collections.Generic;

namespace XCore.EntityFrameworkCore
{
    public interface IEntityManager
    {
        IEnumerable<Type> GetEntitys();
        IEnumerable<Type> GetEntityTypeConfiguration();
    }
}
