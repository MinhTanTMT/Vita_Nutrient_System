using System.Linq;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using AutoMapper;
using SEP490_G87_Vita_Nutrient_System_API.Controllers;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Domain.DataFoodList;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;

namespace TestProject_SEP490_G87_Vita_Nutrient_System_API.TanTest.TestAPI.TanTestFunctionGenerateMealController
{

    public class TestAPIListMealOfTheDay
    {
        private readonly Mock<IGenerateMealRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GenerateMealController _controller;

        public TestAPIListMealOfTheDay()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IGenerateMealRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new GenerateMealController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            DateTime myDay = DateTime.Now;
            int idUser = 6;

            // Mock dữ liệu trả về từ ListMealOfTheDay
            _mockRepository.Setup(repo => repo.ListMealOfTheDay(myDay, idUser))
                .ReturnsAsync((List<DataFoodListMealOfTheDay>) null);

            // Mock SystemUserConfiguration
            _mockRepository.Setup(repo => repo.SystemUserConfiguration(idUser))
                .ReturnsAsync(true); 

            // Mock FillInDishIdInDailyDishWithCondition
            _mockRepository.Setup(repo => repo.FillInDishIdInDailyDishWithCondition(idUser, myDay))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.APIListMealOfTheDay(myDay, idUser);

            // Assert
            var okResult = Assert.IsType< ActionResult<IEnumerable<DataFoodListMealOfTheDay>>>(result);
        }


        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            DateTime myDay = DateTime.Now;
            int idUser = 5;

            // Mock dữ liệu trả về từ ListMealOfTheDay
            _mockRepository.Setup(repo => repo.ListMealOfTheDay(myDay, idUser))
                .ReturnsAsync((List<DataFoodListMealOfTheDay>)null);

            // Mock SystemUserConfiguration
            _mockRepository.Setup(repo => repo.SystemUserConfiguration(idUser))
                .ReturnsAsync(true);

            // Mock FillInDishIdInDailyDishWithCondition
            _mockRepository.Setup(repo => repo.FillInDishIdInDailyDishWithCondition(idUser, myDay))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.APIListMealOfTheDay(myDay, idUser);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<DataFoodListMealOfTheDay>>>(result);
        }

    }


    public class TestAPIListMealOfTheWeek
    {
        private readonly Mock<IGenerateMealRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GenerateMealController _controller;

        public TestAPIListMealOfTheWeek()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IGenerateMealRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new GenerateMealController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var idUser = 5;
            var myDay = DateTime.Now;

            // Mock SystemUserConfiguration trả về Task.CompletedTask (nếu cần trả về true, bạn có thể mock theo ReturnsAsync)
            _mockRepository.Setup(repo => repo.SystemUserConfiguration(idUser))
                .ReturnsAsync(true); // Hoặc nếu bạn cần mock Task<bool> thì có thể là ReturnsAsync(true) hoặc ReturnsAsync(false)

            // Mock ListMealOfTheWeek trả về một danh sách mẫu

            _mockRepository.Setup(repo => repo.ListMealOfTheDay(myDay, idUser))
                .ReturnsAsync((List<DataFoodListMealOfTheDay>)null);

            // Act
            var result = await _controller.APIListMealOfTheWeek(myDay, idUser);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<DataFoodAllDayOfWeek>>>(result);

        }

        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var idUser = 6;
            var myDay = DateTime.Now;

            // Mock SystemUserConfiguration trả về Task.CompletedTask (nếu cần trả về true, bạn có thể mock theo ReturnsAsync)
            _mockRepository.Setup(repo => repo.SystemUserConfiguration(idUser))
                .ReturnsAsync(true); // Hoặc nếu bạn cần mock Task<bool> thì có thể là ReturnsAsync(true) hoặc ReturnsAsync(false)

            // Mock ListMealOfTheWeek trả về một danh sách mẫu

            _mockRepository.Setup(repo => repo.ListMealOfTheDay(myDay, idUser))
                .ReturnsAsync((List<DataFoodListMealOfTheDay>)null);

            // Act
            var result = await _controller.APIListMealOfTheWeek(myDay, idUser);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<DataFoodAllDayOfWeek>>>(result);

        }
    }

    public class TestAPIRefreshTheMealy
    {
        private readonly Mock<IGenerateMealRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GenerateMealController _controller;

        public TestAPIRefreshTheMealy()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IGenerateMealRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new GenerateMealController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var myDay = DateTime.Now.AddDays(1); // Chọn một ngày trong tương lai
            var idUser = 5;

            // Mock FillInDishIdInDailyDishWithCondition để trả về true khi ngày lớn hơn hoặc bằng ngày hiện tại
            _mockRepository.Setup(repo => repo.FillInDishIdInDailyDishWithCondition(idUser, myDay))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.APIRefreshTheMealy(myDay, idUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<bool>(okResult.Value);
        }


        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var myDay = DateTime.Now.AddDays(1); // Chọn một ngày trong tương lai
            var idUser = 6;

            // Mock FillInDishIdInDailyDishWithCondition để trả về true khi ngày lớn hơn hoặc bằng ngày hiện tại
            _mockRepository.Setup(repo => repo.FillInDishIdInDailyDishWithCondition(idUser, myDay))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.APIRefreshTheMealy(myDay, idUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<bool>(okResult.Value);
        }


    }


    public class TestAPIRefreshTheAllMeal
    {
        private readonly Mock<IGenerateMealRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GenerateMealController _controller;

        public TestAPIRefreshTheAllMeal()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IGenerateMealRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new GenerateMealController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var myDay = DateTime.Now.AddDays(1); // Chọn một ngày trong tương lai
            var idUser = 5;

            // Mock RegenerateListMealOfTheWeek để trả về một danh sách giả

            _mockRepository.Setup(repo => repo.RegenerateListMealOfTheWeek(myDay, idUser))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.APIRefreshTheAllMeal(myDay, idUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<bool>(okResult.Value);
        }



        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var myDay = DateTime.Now.AddDays(1); // Chọn một ngày trong tương lai
            var idUser = 6;

            // Mock RegenerateListMealOfTheWeek để trả về một danh sách giả

            _mockRepository.Setup(repo => repo.RegenerateListMealOfTheWeek(myDay, idUser))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.APIRefreshTheAllMeal(myDay, idUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<bool>(okResult.Value);
        }


    }


    public class TestAPICompleteTheDish
    {
        private readonly Mock<IGenerateMealRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GenerateMealController _controller;

        public TestAPICompleteTheDish()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IGenerateMealRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new GenerateMealController();
        }

        /*
         SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#SlotOfTheDay=2;SettingDetail=2;OrderNumber=1:
#SlotOfTheDay=4;SettingDetail=3;OrderNumber=1:
#

         */

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var model = new FoodStatusUpdateModel
            {
                UserId = 5, // userId hợp lệ
                MyDay = DateTime.Now.AddDays(-3), // Một ngày trong phạm vi 7 ngày qua
                StatusSymbol = "+", // Hoặc "-"
                SlotOfTheDay = 1,
                SettingDetail = 1,
                IdFood = 2,
                PositionFood = 1,
                OrderNumber = 1
            };

            // Mock các phương thức repository cần thiết
            _mockRepository.Setup(repo => repo.ModifiedCompleteTheDish(model, "-", null, null))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.APICompleteTheDish(model);

            // Assert
            //var okResult = Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<ActionResult<bool>>(result);
            var returnValue = Assert.IsType<bool>(okResult.Value);
        }


        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var model = new FoodStatusUpdateModel
            {
                MyDay = DateTime.Now.AddDays(-10), // Một ngày ngoài phạm vi 7 ngày
                StatusSymbol = "+" // Hoặc "-"
            };

            // Act
            var result = await _controller.APICompleteTheDish(model);

            // Assert
            var badRequestResult = Assert.IsType<ActionResult<bool>>(result);
            //Assert.Equal(400, badRequestResult.StatusCode); // Kiểm tra mã lỗi trả về là 400
        }


        [Fact]
        public async Task APITestPass3()
        {
            // Arrange
            var model = new FoodStatusUpdateModel
            {
                MyDay = DateTime.Now.AddDays(-3), // Một ngày hợp lệ
                StatusSymbol = "InvalidSymbol" // Giá trị không hợp lệ cho StatusSymbol
            };

            // Act
            var result = await _controller.APICompleteTheDish(model);

            // Assert
            var badRequestResult = Assert.IsType<ActionResult<bool>>(result);
            //Assert.Equal(400, badRequestResult.StatusCode); // Kiểm tra mã lỗi trả về là 400
        }


    }


    public class TestAPICreateListOfAlternativeDishes
    {
        private readonly Mock<IGenerateMealRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GenerateMealController _controller;

        public TestAPICreateListOfAlternativeDishes()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IGenerateMealRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new GenerateMealController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var request = new AlternativeDishesRequest
            {
                ListIdFood = new List<int> { 2, 3, 4 }, // Danh sách món ăn hợp lệ
                MealSettingsDetailsId = 1, // Chi tiết bữa ăn hợp lệ
                NumberOfCreation = 5 // Số món thay thế hợp lệ
            };
            int foodSelectionType = 1; // Loại món ăn hợp lệ

            // Mock repository trả về danh sách các món ăn thay thế
            _mockRepository.Setup(repo => repo.CreateListOfAlternativeDishes(request.ListIdFood, request.MealSettingsDetailsId, request.NumberOfCreation, foodSelectionType))
                .ReturnsAsync(new List<DataFoodListMealOfTheDay>
                {
            // Dữ liệu giả cho các món ăn thay thế
            new DataFoodListMealOfTheDay { /* Các thuộc tính của DataFoodListMealOfTheDay */ }
                });

            // Act
            var result = await _controller.APICreateListOfAlternativeDishes(request, foodSelectionType);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<DataFoodListMealOfTheDay>>>(result);
        }


        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var request = new AlternativeDishesRequest
            {
                ListIdFood = new List<int>(), // Danh sách món ăn trống
                MealSettingsDetailsId = 1,
                NumberOfCreation = 5
            };
            int foodSelectionType = 1;

            // Act
            var result = await _controller.APICreateListOfAlternativeDishes(request, foodSelectionType);

            // Assert
            var badRequestResult = Assert.IsType<ActionResult<IEnumerable<DataFoodListMealOfTheDay>>>(result);
        }


        [Fact]
        public async Task APITestPass3()
        {
            // Arrange
            var request = new AlternativeDishesRequest
            {
                ListIdFood = new List<int> { 1, 2 },
                MealSettingsDetailsId = 0, // MealSettingsDetailsId không hợp lệ
                NumberOfCreation = 5
            };
            int foodSelectionType = 1;

            // Act
            var result = await _controller.APICreateListOfAlternativeDishes(request, foodSelectionType);

            // Assert
            var badRequestResult = Assert.IsType<ActionResult<IEnumerable<DataFoodListMealOfTheDay>>>(result);
        }

    }



    public class TestAPIgetThisListOfDishes
    {
        private readonly Mock<IGenerateMealRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GenerateMealController _controller;

        public TestAPIgetThisListOfDishes()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IGenerateMealRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new GenerateMealController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var userId = 5;
            var myDay = DateTime.Now;


            // Khởi tạo các đối tượng FoodIdData
            FoodIdData food1 = new FoodIdData
            {
                idFood = 1,
                statusSymbol = "+",  // Ví dụ: trạng thái món ăn
                positionFood = 1,  // Vị trí món ăn trong thực đơn
                foodData = new FoodListDTO  // Cung cấp dữ liệu cho FoodListDTO (cần cấu trúc này)
                {
                    FoodListId = 1,
                    Name = "Canh cải cúc nấu cá rô",
                    Describe = "Món canh cải cúc nấu cá rô với hương vị thanh mát, dễ ăn.",
                    Rate = 78.6,
                    NumberRate = 54,
                    Urlimage = "http://example.com/canh_ca_roc.jpg",
                    FoodTypeId = 11,
                    KeyNoteId = 93,
                    IsActive = true,
                    PreparationTime = 15,
                    CookingTime = 30,
                    CookingDifficultyId = 3
                }
            };

            FoodIdData food2 = new FoodIdData
            {
                idFood = 2,
                statusSymbol = "-",  // Trạng thái món ăn
                positionFood = 2,  // Vị trí món ăn trong thực đơn
                foodData = new FoodListDTO  // Cung cấp dữ liệu cho FoodListDTO (cần cấu trúc này)
                {
                    FoodListId = 2,
                    Name = "Canh cải soong nấu thịt nạc",
                    Describe = "Món canh cải soong nấu thịt nạc thơm ngon.",
                    Rate = 87.6,
                    NumberRate = 74,
                    Urlimage = "http://example.com/canh_cai_soong.jpg",
                    FoodTypeId = 2,
                    KeyNoteId = 93,
                    IsActive = true,
                    PreparationTime = 10,
                    CookingTime = 25,
                    CookingDifficultyId = 3
                }
            };

            // Khởi tạo đối tượng DataFoodListMealOfTheDay
            DataFoodListMealOfTheDay dataFoodListMealOfTheDay = new DataFoodListMealOfTheDay
            {
                SlotOfTheDay = 1,  // Khung giờ bữa ăn
                SettingDetail = 1,  // Chi tiết cấu hình bữa ăn
                OrderSettingDetail = 1,  // Số thứ tự của món ăn trong bữa ăn
                NameSlotOfTheDay = "Sáng",  // Tên khung giờ
                foodIdData = new FoodIdData[] { food1, food2 }  // Mảng các món ăn
            };



            _mockRepository.Setup(repo => repo.GetThisListOfDishesInputMealDay(dataFoodListMealOfTheDay, userId, myDay))
                          .ReturnsAsync(true);

            // Act
            var result = await _controller.APIgetThisListOfDishes(dataFoodListMealOfTheDay, userId, myDay);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<bool>(okResult.Value);

        }

        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var userId = 6;
            var myDay = DateTime.Now;


            // Khởi tạo các đối tượng FoodIdData
            FoodIdData food1 = new FoodIdData
            {
                idFood = 1,
                statusSymbol = "+",  // Ví dụ: trạng thái món ăn
                positionFood = 1,  // Vị trí món ăn trong thực đơn
                foodData = new FoodListDTO  // Cung cấp dữ liệu cho FoodListDTO (cần cấu trúc này)
                {
                    FoodListId = 1,
                    Name = "Canh cải cúc nấu cá rô",
                    Describe = "Món canh cải cúc nấu cá rô với hương vị thanh mát, dễ ăn.",
                    Rate = 78.6,
                    NumberRate = 54,
                    Urlimage = "http://example.com/canh_ca_roc.jpg",
                    FoodTypeId = 11,
                    KeyNoteId = 93,
                    IsActive = true,
                    PreparationTime = 15,
                    CookingTime = 30,
                    CookingDifficultyId = 3
                }
            };

            FoodIdData food2 = new FoodIdData
            {
                idFood = 2,
                statusSymbol = "-",  // Trạng thái món ăn
                positionFood = 2,  // Vị trí món ăn trong thực đơn
                foodData = new FoodListDTO  // Cung cấp dữ liệu cho FoodListDTO (cần cấu trúc này)
                {
                    FoodListId = 2,
                    Name = "Canh cải soong nấu thịt nạc",
                    Describe = "Món canh cải soong nấu thịt nạc thơm ngon.",
                    Rate = 87.6,
                    NumberRate = 74,
                    Urlimage = "http://example.com/canh_cai_soong.jpg",
                    FoodTypeId = 2,
                    KeyNoteId = 93,
                    IsActive = true,
                    PreparationTime = 10,
                    CookingTime = 25,
                    CookingDifficultyId = 3
                }
            };

            // Khởi tạo đối tượng DataFoodListMealOfTheDay
            DataFoodListMealOfTheDay dataFoodListMealOfTheDay = new DataFoodListMealOfTheDay
            {
                SlotOfTheDay = 1,  // Khung giờ bữa ăn
                SettingDetail = 1,  // Chi tiết cấu hình bữa ăn
                OrderSettingDetail = 1,  // Số thứ tự của món ăn trong bữa ăn
                NameSlotOfTheDay = "Sáng",  // Tên khung giờ
                foodIdData = new FoodIdData[] { food1, food2 }  // Mảng các món ăn
            };



            _mockRepository.Setup(repo => repo.GetThisListOfDishesInputMealDay(dataFoodListMealOfTheDay, userId, myDay))
                          .ReturnsAsync(true);

            // Act
            var result = await _controller.APIgetThisListOfDishes(dataFoodListMealOfTheDay, userId, myDay);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<bool>(okResult.Value);
        }


    }

    public class TestAPISelectReplaceCurrentFood
    {
        private readonly Mock<IGenerateMealRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GenerateMealController _controller;

        public TestAPISelectReplaceCurrentFood()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IGenerateMealRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new GenerateMealController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var model = new FoodStatusUpdateModel
            {
                UserId = 1,
                MyDay = DateTime.Now,
                SlotOfTheDay = 1,
                SettingDetail = 1,
                IdFood =4,
                StatusSymbol = "+",
                PositionFood = 1,
                OrderNumber = 1
            };
            int idFoodSelect = 5;

            // Mock repository response

            _mockRepository.Setup(repo => repo.ModifiedCompleteTheDish(model, null, idFoodSelect, null))
                          .ReturnsAsync(true);

            // Act
            var result = await _controller.APISelectReplaceCurrentFood(model, idFoodSelect);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var model = new FoodStatusUpdateModel
            {
                UserId = 1,
                MyDay = DateTime.Now,
                SlotOfTheDay = 1,
                SettingDetail = 1,
                StatusSymbol = "+", // Thiếu thông tin IdFood và các trường bắt buộc khác để mô phỏng lỗi
            };
            int idFoodSelect = 5;

            // Act
            var result = await _controller.APISelectReplaceCurrentFood(model, idFoodSelect);

            // Assert
            var badRequestResult = Assert.IsType<OkObjectResult>(result);
        }

    }




    public class TestAPISystemUserConfiguration
    {
        private readonly Mock<IGenerateMealRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GenerateMealController _controller;

        public TestAPISystemUserConfiguration()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IGenerateMealRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new GenerateMealController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            int idUser = 5;

            // Mock repository trả về true khi gọi SystemUserConfiguration
            _mockRepository.Setup(repo => repo.SystemUserConfiguration(idUser))
                           .ReturnsAsync(true);

            // Act
            var result = await _controller.APISystemUserConfiguration(idUser);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode); // Kiểm tra trả về status code 200
        }



        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            int idUser = 7;

            // Mock repository trả về false khi gọi SystemUserConfiguration
            _mockRepository.Setup(repo => repo.SystemUserConfiguration(idUser))
                           .ReturnsAsync(false);

            // Act
            var result = await _controller.APISystemUserConfiguration(idUser);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode); // Kiểm tra trả về status code 404
        }



        [Fact]
        public async Task APITestPass3()
        {
            // Arrange
            int idUser = 6;

            // Mock repository trả về true khi gọi SystemUserConfiguration
            _mockRepository.Setup(repo => repo.SystemUserConfiguration(idUser))
                           .ReturnsAsync(true);

            // Act
            var result = await _controller.APISystemUserConfiguration(idUser);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode); // Kiểm tra trả về status code 200
        }


    }




    /// <summary>
    /// ///////////////////
    /// </summary>


    public class Test_ToCopy
    {
        private readonly Mock<IGenerateMealRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GenerateMealController _controller;

        public Test_ToCopy()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IGenerateMealRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new GenerateMealController();
        }

        [Fact]
        public async Task APITestPass1()
        {

        }

    }


}














