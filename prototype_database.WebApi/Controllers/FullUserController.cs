﻿using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using prototype_database.Contract;
using prototype_database.Contract.DTOs;

namespace prototype_database.WebApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/user")]
    public class FullUserController : Controller
    {
        private readonly IUserService _service;
        private readonly IRandomIdGenerator _randomIdGenerator;

        public FullUserController(IUserService service, IRandomIdGenerator randomIdGenerator)
        {
            _service = service;
            _randomIdGenerator = randomIdGenerator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_service.GetUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var user = _service.GetUser(id);

            if (user == null)
            {
                return NotFound(id);
            }

            return Json(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO dto)
        {
            var id = _randomIdGenerator.GetId(5);
            string result = null;
            try
            {
                result = _service.Create(dto, id);
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
            var result = _service.Delete(id);
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
