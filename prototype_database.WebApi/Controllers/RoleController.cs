using System;
using Microsoft.AspNetCore.Mvc;

using prototype_database.Contract;

namespace prototype_database.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    class RoleController : Controller
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_service.GetRoles());
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (!Guid.TryParse(id, out Guid orgId))
            {
                return BadRequest(id);
            }

            var org = _service.GetRole(orgId);

            if (org == null)
            {
                return NotFound(id);
            }

            return Json(org);
        }
    }
}
