using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using prototype_database.Contract;

namespace prototype_database.WebApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly IGroupService _service;

        public GroupController(IGroupService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_service.GetAllGroup());
        }

        [HttpGet("{id}")]
        public IActionResult GetGroup(string id)
        {
            if (!Guid.TryParse(id, out Guid groupId))
            {
                return BadRequest(id);
            }

            var group = _service.GetGroup(groupId);

            if (group == null)
            {
                return NotFound(id);
            }

            return Json(group);
        }

        [HttpGet("/org/{id}")]
        public IActionResult GetGroupBelongToOrganization(string id)
        {
            if (!Guid.TryParse(id, out Guid orgId))
            {
                return BadRequest(id);
            }

            return Json(_service.GetAllGroupBelongToOrganization(orgId));
        }
    }
}
