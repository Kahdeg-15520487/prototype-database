using System;
using System.Collections.Generic;

using prototype_database.Contract.DTOs;

namespace prototype_database.Contract
{
    public interface IGroupService
    {
        IEnumerable<GroupDTO> GetAllGroup();
        IEnumerable<GroupDTO> GetAllGroupBelongToOrganization(Guid organizationId);
        GroupDTO GetGroup(Guid groupId);
    }
}
