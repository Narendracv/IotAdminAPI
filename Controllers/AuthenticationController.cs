using IotAdminAPI.Authentication;
using IotAdminAPI.Models;
using IotAdminAPI.Services;
using IotAdminAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IotAdminAPI.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationRepository authenticationRepository;
        private readonly IConfiguration configuration;

        public AuthenticationController(ILogger<AuthenticationController> logger,
            IAuthenticationRepository repository,
            IConfiguration configuration)
        {
            _logger = logger;
            authenticationRepository = repository;
            this.configuration = configuration;
        }



        [HttpPost]
        [Route("Token")]
        public async Task<IActionResult> Token(Credentials credentials)
        {
            var user = await authenticationRepository.ValidateCredentials(credentials.UserName,
                credentials.Password);


            if (user == null) return NotFound();

            string tokenSingingKey = this.configuration["TokenSigningKey"];

            AuthenticationResponse token = JwtAuthentication.GenerateToken(user, tokenSingingKey);

            return Ok(token);

        }



    }
}
