using Microsoft.AspNetCore.Mvc;
using MiddlewareNewZealand.Api.Controllers;
using MiddlewareNewZealand.Api.Models.ViewModels;
using MiddlewareNewZealand.Api.Services.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MiddlewareNewZealand.UnitTests.Controllers
{
    public class CompaniesControllerTests
    {
        [Fact]
        public async Task Given_A_Known_Comapny_Id_Is_Passed_Then_An_OK_Result_Should_Returned_With_The_Comapny_Object()
        {
            //Arrange
            var companyId = 1;

            Mock<ICompanyService> mockCompanyService = new Mock<ICompanyService>(MockBehavior.Strict);
            mockCompanyService.Setup(x => x.GetByCompanyId(companyId))
                .ReturnsAsync(new Company());

            //Act
            CompaniesController controller = new CompaniesController(mockCompanyService.Object);
            var respone = await controller.GetByCompanyId(companyId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(respone);
            Assert.IsType<Company>(okResult.Value);
        }

        [Fact]
        public async Task Given_A_UnKnownn_Comapny_Id_Is_Passed_Then_An_Not_Found_Result_Should_Returned()
        {
            //Arrange
            var companyId = 1;

            Mock<ICompanyService> mockCompanyService = new Mock<ICompanyService>(MockBehavior.Strict);
            mockCompanyService.Setup(x => x.GetByCompanyId(companyId))
                .ReturnsAsync((Company)null);

            //Act
            CompaniesController controller = new CompaniesController(mockCompanyService.Object);
            var respone = await controller.GetByCompanyId(companyId);

            //Assert
            Assert.IsType<NotFoundResult>(respone);
        }
    }
}
