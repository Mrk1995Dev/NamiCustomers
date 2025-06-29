

using NamiCustomers.API.Controllers.v1;

namespace NamiCustomers.API.Tests.v1
{
    public class SAMPLEControllerTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public SAMPLEControllerTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public void AliveTest()
        {
            _testOutputHelper.WriteLine($".Net Core Web API Version 1");
        }

        [Fact]
        public async Task Calculate_ReturnsOkResult()
        {
            //// Arrange
            //var mockService = new Mock<ISAMPLEService>();
            //var controller = new SAMPLEController(mockService.Object);

            //var datatype = "json";
            //var SAMPLEDTO = new SAMPLEDTO { Field = "FieldName" };

            //// Act
            //var result = controller.Alive();

            //// Assert
            //Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Alive_ReturnsOkResult()
        {
            //// Arrange
            //var mockBinder = new Mock<IModelBinder>();
            //var mockService = new Mock<ISAMPLEService>();
            //var mockFactory = new Mock<ModelBinderFactory>();
            ////  mockFactory.Setup(f => f.CreateBinder(It.IsAny<ModelBinderFactoryContext>())).Returns(mockBinder.Object);

            //var mockHttpContext = new Mock<HttpContext>();
            //mockHttpContext.SetupGet(c => c.RequestServices).Returns(Mock.Of<IServiceProvider>());

            //var controllerContext = new ControllerContext()
            //{
            //    HttpContext = mockHttpContext.Object
            //};

            //var controller = new SAMPLEController(mockService.Object)
            //{
            //    ControllerContext = controllerContext
            //};

            //var SAMPLEDTO = new SAMPLEDTO { Field = "FieldName" };
            //// Act
            //var result = controller.Alive();

            //// Assert
            //Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Update_WithValidSAMPLEDTO_ReturnsOkResult()
        {
            //// Arrange
            //var mockService = new Mock<ISAMPLEService>();
            //var controller = new SAMPLEController(mockService.Object);

            //var SAMPLEDTO = new SAMPLEDTO { Field = "FieldName" };
            //// Act
            //var result = controller.Update(SAMPLEDTO);

            //// Assert
            //Assert.IsType<Task<SAMPLEDTO>>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsOkResult()
        {
            //// Arrange
            //var mockService = new Mock<ISAMPLEService>();
            //var controller = new SAMPLEController(mockService.Object);

            //var id = 3; // Specify a valid ID

            //// Act
            //var result = controller.Delete(id);

            //// Assert
            //Assert.IsType<Task<int>>(result);
        }


        [Fact]
        public async Task GetRangeAsync_WithValidFilter_ReturnsOkResult()
        {
            //// Arrange
            //var mockService = new Mock<ISAMPLEService>();
            //var controller = new SAMPLEController(mockService.Object);

            //var filter = new SAMPLEFilterDTO { Field = "Filter" };

            //// Act
            //var result = await controller.GetRangeAsync(filter);

            //// Assert
            //Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsOkResult()
        {
            //// Arrange
            //var mockService = new Mock<ISAMPLEService>();
            //var controller = new SAMPLEController(mockService.Object);

            //var id = 123; // Specify a valid ID

            //// Act
            //var result = await controller.Get(id);

            //// Assert
            //Assert.IsType<OkObjectResult>(result);
        }

    }
}
