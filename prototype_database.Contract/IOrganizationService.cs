using System;
using System.Collections.Generic;

using prototype_database.Contract.DTOs;

namespace prototype_database.Contract
{
    public interface IOrganizationService
    {
        IEnumerable<OrganizationDTO> GetOrganizations();
        OrganizationDTO GetOrganization(Guid id);
    }
}
