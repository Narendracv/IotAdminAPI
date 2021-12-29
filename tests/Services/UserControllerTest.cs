using IotAdminAPI.Controllers;
using IotAdminAPI.Models;
using IotAdminAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test_IoTAdminAPI.Services
{

    public class UserControllerTest
    {
        public Mock<IUserRepository> mock = new Mock<IUserRepository>();
        public Mock<ILogger<UsersController>> mockLogger = new Mock<ILogger<UsersController>>();

        [Fact]
        public async void GetUserById()
        {
            mock.Setup(_ => _.GetById(1))
.ReturnsAsync(new User() { Id = 1, Name = "Narendra" });
            UsersController usr = new UsersController(mockLogger.Object, mock.Object);
            var result = await usr.GetById(1);
            OkObjectResult apiResp = (OkObjectResult)result;
            User respUser = (User)apiResp.Value;
            Assert.Equal(apiResp.StatusCode, ((int?)HttpStatusCode.OK));

            Assert.Equal(respUser.Id, 1);
        }
        [Fact]
        public async void GetUserByIdNotFound()
        {
            User userNotFound = null;
            mock.Setup(a => a.GetById(222)).ReturnsAsync(userNotFound) ;
            UsersController usr = new UsersController(mockLogger.Object, mock.Object);
            var result = await usr.GetById(222);
            NotFoundObjectResult apiResp = (NotFoundObjectResult)result;
            
            Assert.Equal(apiResp.StatusCode, ((int?)HttpStatusCode.NotFound));

        }
    }
}
