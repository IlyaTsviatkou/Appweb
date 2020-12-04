using Appweb.Controllers;
using Appweb.Domain.Core;
using Appweb.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class HomeControllerTests
    {
        ILogger<HomeController> logger;
        UserManager<User> userManager;
        ApplicationContext appContext;
      /*  [Fact]
        public void IndexViewDataMessage()
        {
            // Arrange
            HomeController controller = new HomeController(logger, userManager, appContext);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("Hello world!", result?.ViewData["Message"]);
        }*/

        [Fact]
        public void IndexViewResultNotNull()
        {
            var mock = new Mock<UserManager<User>>();
            var mock2 = new Mock<ApplicationContext>();
            mock2.Setup(repo => repo.Collections.ToList()).Returns(GetTestCollections());
            // Arrange
            HomeController controller = new HomeController(mock.Object,mock2.Object);
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }
        private List<Collection> GetTestCollections()
        {
            var users = new List<Collection>
            {
            };
            return users;
        }

        /*  [Fact]
          public void IndexViewNameEqualIndex()
          {
              // Arrange
              HomeController controller = new HomeController(logger, userManager, appContext);
              // Act
              ViewResult result = controller.Index() as ViewResult;
              // Assert
              Assert.Equal("Index", result?.ViewName);
          }*/
    }
}
