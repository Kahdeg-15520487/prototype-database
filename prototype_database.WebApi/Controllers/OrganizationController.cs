﻿using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using prototype_database.Contract;

namespace prototype_database.WebApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _service;

        public OrganizationController(IOrganizationService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_service.GetOrganizations());
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (!Guid.TryParse(id, out Guid orgId))
            {
                return BadRequest(id);
            }

            var org = _service.GetOrganization(orgId);

            if (org == null)
            {
                return NotFound(id);
            }

            return Json(org);
        }
    }
}
