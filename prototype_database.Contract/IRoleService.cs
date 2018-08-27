using System;
using System.Collections.Generic;

using prototype_database.Contract.DTOs;

namespace prototype_database.Contract
{
    public interface IRoleService
    {
        IEnumerable<RoleDTO> GetRoles();
        RoleDTO GetRole(Guid id);
    }
}
