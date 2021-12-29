using IotAdminAPI.Models;
using IotAdminAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IotAdminAPI.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    //[Authorize]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;
        public UsersController(ILogger<UsersController> logger, IUserRepository repository)
        {
            _logger = logger;
            _userRepository = repository;
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult Get()
        {
            var result = _userRepository.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userRepository.GetById(id);

            if (result == null) return NotFound(id);

            _logger.LogInformation($"{id} : user found in the system");
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User userVm)
        {
            var result = await _userRepository.Update(userVm);

            _logger.LogInformation($"{result} : user successfully updated");

            return Ok(result);
        }

        [HttpPost]
        [Route("SaveUser")]
        public async Task<IActionResult> SaveUser(User userVm)
        {
            var result = await _userRepository.Add(userVm);
            _logger.LogInformation($"{result} : user CREATED in the system");

            return Ok(result);

        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userRepository.Delete(id);
            _logger.LogInformation($"{id} : user DELETED from the system");

            return Ok(result);
        }

    }
}
