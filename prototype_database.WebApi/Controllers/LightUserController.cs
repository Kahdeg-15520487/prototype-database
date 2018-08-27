using System;

using Microsoft.AspNetCore.Mvc;
using prototype_database.Contract;
using prototype_database.Contract.DTOs;

namespace prototype_database.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/user/light")]
    public class LightUserController : Controller
    {
        private readonly IUserService _service;
        private readonly IRandomIdGenerator _randomIdGenerator;

        public LightUserController(IUserService service, IRandomIdGenerator randomIdGenerator)
        {
            _service = service;
            _randomIdGenerator = randomIdGenerator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_service.GetLightUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var user = _service.GetLightUser(id);

            if (user == null)
            {
                return NotFound(id);
            }

            return Json(user);
        }
    }
}
