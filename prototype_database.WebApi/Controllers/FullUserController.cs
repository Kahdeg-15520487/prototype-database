using System;

using Microsoft.AspNetCore.Mvc;
using prototype_database.Contract;
using prototype_database.Contract.DTOs;

namespace prototype_database.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/user/full")]
    public class FullUserController : Controller
    {
        private readonly Func<ServiceType, IUserService> _serviceAccessor;
        private readonly IRandomIdGenerator _randomIdGenerator;

        public FullUserController(Func<ServiceType, IUserService> serviceAccessor, IRandomIdGenerator randomIdGenerator)
        {
            _serviceAccessor = serviceAccessor;
            _randomIdGenerator = randomIdGenerator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var service = _serviceAccessor(ServiceType.Full);
            return Json(service.GetUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var service = _serviceAccessor(ServiceType.Full);
            var user = service.GetUser(id);

            if (user == null)
            {
                return NotFound(id);
            }

            return Json(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO dto)
        {
            var service = _serviceAccessor(ServiceType.Full);
            var id = _randomIdGenerator.GetId(5);
            string result = null;
            try
            {
                result = service.Create(dto, id);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserDTO dto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            var service = _serviceAccessor(ServiceType.Full);
            var result = service.Delete(id);
            if (result)
            {
                return Ok(id);
            }
            else
            {
                return NotFound(id);
            }
        }
    }
}
