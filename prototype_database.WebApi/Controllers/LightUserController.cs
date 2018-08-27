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
        private readonly Func<ServiceType, IUserService> _serviceAccessor;
        private readonly IRandomIdGenerator _randomIdGenerator;

        public LightUserController(Func<ServiceType, IUserService> serviceAccessor, IRandomIdGenerator randomIdGenerator)
        {
            _serviceAccessor = serviceAccessor;
            _randomIdGenerator = randomIdGenerator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_serviceAccessor(ServiceType.Light).GetUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var service = _serviceAccessor(ServiceType.Light);
            var user = service.GetUser(id);

            if (user == null)
            {
                return NotFound(id);
            }

            return Json(user);
        }
    }
}
