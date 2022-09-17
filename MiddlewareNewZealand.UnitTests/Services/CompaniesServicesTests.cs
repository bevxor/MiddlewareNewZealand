using Microsoft.AspNetCore.Mvc;
using MiddlewareNewZealand.Api.Controllers;
using MiddlewareNewZealand.Api.Models.ViewModels;
using MiddlewareNewZealand.Api.Repositories.Interfaces;
using MiddlewareNewZealand.Api.Services;
using MiddlewareNewZealand.Api.Services.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;

namespace MiddlewareNewZealand.UnitTests.Services
{
    public class CompaniesServicesTests
    {
        [Fact]
        public async Task Given_A_Known_Comapny_Id_Is_Passed_Then_Correct_Methods_Should_Returned_With_The_Comapny_Object()
        {
            //Arrange
            var companyId = 1;
            var clientResponse = new Api.Models.Clients.Company();
            var mappperResponse = new Api.Models.ViewModels.Company();

            var mockMiddlewareNewZealandClient = new Mock<IMiddlewareNewZealandClient>(MockBehavior.Strict);
            mockMiddlewareNewZealandClient.Setup(x => x.GetByCompanyId(companyId))
                .ReturnsAsync(clientResponse);

            var mockMapper = new Mock<IMapper>(MockBehavior.Strict);
            mockMapper.Setup(x => x.Map<Api.Models.ViewModels.Company>(clientResponse))
                .Returns(mappperResponse);

            //Act
            var companyService = new CompanyService(mockMiddlewareNewZealandClient.Object, mockMapper.Object);
            var respone = await companyService.GetByCompanyId(companyId);

            //Assert
            Assert.IsType<Api.Models.ViewModels.Company>(respone);
            mockMiddlewareNewZealandClient.Verify(x => x.GetByCompanyId(companyId), Times.Once);
            mockMapper.Verify(x => x.Map<Api.Models.ViewModels.Company>(clientResponse), Times.Once);
        }

        [Fact]
        public async Task Given_A_UnKnownn_Comapny_Id_Is_Passed_Then_Null_Should_Returned()
        {
            //Arrange
            var companyId = 1;
            var clientResponse = (Api.Models.Clients.Company)null;
            var mappperResponse = new Api.Models.ViewModels.Company();

            var mockMiddlewareNewZealandClient = new Mock<IMiddlewareNewZealandClient>(MockBehavior.Strict);
            mockMiddlewareNewZealandClient.Setup(x => x.GetByCompanyId(companyId))
                .ReturnsAsync(clientResponse);

            var mockMapper = new Mock<IMapper>(MockBehavior.Strict);
            mockMapper.Setup(x => x.Map<Api.Models.ViewModels.Company>(clientResponse))
                .Returns(mappperResponse);

            //Act
            var companyService = new CompanyService(mockMiddlewareNewZealandClient.Object, mockMapper.Object);
            var respone = await companyService.GetByCompanyId(companyId);

            //Assert
            Assert.Null(respone);
            mockMiddlewareNewZealandClient.Verify(x => x.GetByCompanyId(companyId), Times.Once);
            mockMapper.Verify(x => x.Map<Api.Models.ViewModels.Company>(clientResponse), Times.Never);
        }
    }
}
