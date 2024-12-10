using System.Linq;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using SEP490_G87_Vita_Nutrient_System_API.Controllers;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using AutoMapper;
using SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels;
using SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.PageResult;

namespace TestProject_SEP490_G87_Vita_Nutrient_System_API.TanTest.TestAPI.TanTestFuntionUserController
{

    public class TestForgotPassword
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;  // Mock IMapper
        private readonly UsersController _controller;

        public TestForgotPassword()
        {
            // Khởi tạo mock repository
            _mockRepository = new Mock<IUserRepositories>();
            _mockMapper = new Mock<IMapper>();

            // Khởi tạo controller với mock repository
            _controller = new UsersController(_mockMapper.Object);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange: Mock phương thức ForgotPassword trả về true
            _mockRepository.Setup(repo => repo.ForgotPassword("minhtanbeater@gmail.com")).ReturnsAsync(true);

            // Act: Gọi API
            var result = await _controller.APIForgotPassword("minhtanbeater@gmail.com");

            // Assert: Kiểm tra kết quả trả về là OkResult
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);  // Kiểm tra mã trạng thái HTTP
        }

        [Fact]
        public async Task APITestPass2()
        {
            // Arrange: Mock phương thức ForgotPassword trả về false
            _mockRepository.Setup(repo => repo.ForgotPassword("nonexistent@gmail.com")).ReturnsAsync(false);

            // Act: Gọi API
            var result = await _controller.APIForgotPassword("nonexistent@gmail.com");

            // Assert: Kiểm tra kết quả trả về là OkResult
            var okResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, okResult.StatusCode);  // Kiểm tra mã trạng thái HTTP
        }
    }



    public class TestLogin
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly UsersController _controller;
        private IMapper mapper;

        public TestLogin()
        {
            // Mock IUserRepository
            _mockRepository = new Mock<IUserRepositories>();
            // Tạo controller và inject mock repository vào
            _controller = new UsersController(mapper);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var account = "testAccount";
            var password = "testPassword";

            // Giả lập repository trả về null khi không tìm thấy người dùng
            _mockRepository.Setup(repo => repo.GetUserLogin(account, password))
                           .ReturnsAsync((UserLoginRegister)null);

            // Act
            var result = await _controller.APIGetUserLogin(account, password);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserLoginRegister>>(result);
            var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var account = "Admin";
            var password = "Admin";
            var user = new UserLoginRegister { Account = account, Password = password }; // Giả sử UserLoginRegister có Account và Password

            // Giả lập repository trả về một đối tượng người dùng hợp lệ
            _mockRepository.Setup(repo => repo.GetUserLogin(account, password))
                           .ReturnsAsync(user);

            // Act
            var result = await _controller.APIGetUserLogin(account, password);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserLoginRegister>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<UserLoginRegister>(okResult.Value);
            Assert.Equal(account, returnValue.Account);
        }


        [Fact]
        public async Task APITestPass3()
        {
            // Arrange
            var account = "Nutri_1";
            var password = "Nutri_1";
            var user = new UserLoginRegister { Account = account, Password = password }; // Giả sử UserLoginRegister có Account và Password

            // Giả lập repository trả về một đối tượng người dùng hợp lệ
            _mockRepository.Setup(repo => repo.GetUserLogin(account, password))
                           .ReturnsAsync(user);

            // Act
            var result = await _controller.APIGetUserLogin(account, password);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserLoginRegister>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<UserLoginRegister>(okResult.Value);
            Assert.Equal(account, returnValue.Account);
        }



        [Fact]
        public async Task APITestPass4()
        {
            // Arrange
            var account = "Nutri_2";
            var password = "Nutri_2";
            var user = new UserLoginRegister { Account = account, Password = password }; // Giả sử UserLoginRegister có Account và Password

            // Giả lập repository trả về một đối tượng người dùng hợp lệ
            _mockRepository.Setup(repo => repo.GetUserLogin(account, password))
                           .ReturnsAsync(user);

            // Act
            var result = await _controller.APIGetUserLogin(account, password);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserLoginRegister>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<UserLoginRegister>(okResult.Value);
            Assert.Equal(account, returnValue.Account);
        }



        [Fact]
        public async Task APITestPass5()
        {
            // Arrange
            var account = "Nutri_3";
            var password = "Nutri_3";
            var user = new UserLoginRegister { Account = account, Password = password }; // Giả sử UserLoginRegister có Account và Password

            // Giả lập repository trả về một đối tượng người dùng hợp lệ
            _mockRepository.Setup(repo => repo.GetUserLogin(account, password))
                           .ReturnsAsync(user);

            // Act
            var result = await _controller.APIGetUserLogin(account, password);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserLoginRegister>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<UserLoginRegister>(okResult.Value);
            Assert.Equal(account, returnValue.Account);
        }
    }






    public class TestAccountExit
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly UsersController _controller;
        private IMapper mapper;

        public TestAccountExit()
        {
            // Mock IUserRepository
            _mockRepository = new Mock<IUserRepositories>();
            // Tạo controller và inject mock repository vào
            _controller = new UsersController(mapper);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var account = "nonExistingAccount";
            var accGoogle = "nonExistingGoogleAccount";

            // Giả lập repository trả về false khi tài khoản không tồn tại
            _mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
                           .ReturnsAsync(false);

            // Act
            var result = await _controller.APIGetUserByAccount(account, accGoogle);

            // Assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var notFoundResult = Assert.IsType<OkResult>(actionResult.Result);
        }


        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var account = "Nutri_3";
            var accGoogle = "Nutri_3";

            // Giả lập repository trả về false khi tài khoản không tồn tại
            _mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
                           .ReturnsAsync(false);

            // Act
            var result = await _controller.APIGetUserByAccount(account, accGoogle);

            // Assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
        }


        [Fact]
        public async Task APITestPass3()
        {
            // Arrange
            var account = "Nutri_2";
            var accGoogle = "Nutri_2";

            // Giả lập repository trả về false khi tài khoản không tồn tại
            _mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
                           .ReturnsAsync(false);

            // Act
            var result = await _controller.APIGetUserByAccount(account, accGoogle);

            // Assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
        }


        [Fact]
        public async Task APITestPass4()
        {
            // Arrange
            var account = "Admin";
            var accGoogle = "Admin";

            // Giả lập repository trả về false khi tài khoản không tồn tại
            _mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
                           .ReturnsAsync(false);

            // Act
            var result = await _controller.APIGetUserByAccount(account, accGoogle);

            // Assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
        }

    }





    public class TestRegiter
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly UsersController _controller;
        private IMapper mapper;

        public TestRegiter()
        {
            // Mock IUserRepository
            _mockRepository = new Mock<IUserRepositories>();
            // Tạo controller và inject mock repository vào
            _controller = new UsersController(mapper);
        }


        [Fact]
        public async Task APITestPass1()
        {

            UserLoginRegister user = new UserLoginRegister()
            {
                FirstName = "firstName",
                LastName = "lastName",
                Account = "account",
                AccountGoogle = "accountGoogle",
                Password = "password",
                Role = 4,
                IsActive = true,
            };


            // Giả lập repository trả về false khi tài khoản không tồn tại
            //_mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
            //               .ReturnsAsync(false);

            // Act
            var result = await _controller.APIGetRegisterLoginGoogle(user);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserLoginRegister>>(result);
            var notFoundResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        }


    }





    public class TestLoginWithGoogle
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly UsersController _controller;
        private IMapper mapper;

        public TestLoginWithGoogle()
        {
            // Mock IUserRepository
            _mockRepository = new Mock<IUserRepositories>();
            // Tạo controller và inject mock repository vào
            _controller = new UsersController(mapper);
        }



        [Fact]
        public async Task APITestPass1()
        {

            UserLoginRegister user = new UserLoginRegister()
            {
                FirstName = "firstName",
                LastName = "lastName",
                Account = "account",
                AccountGoogle = "minhtanbeater@gmail.com",
                Password = "password",
                Role = 4,
                IsActive = true,
            };


            // Giả lập repository trả về false khi tài khoản không tồn tại
            //_mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
            //               .ReturnsAsync(false);

            // Act
            var result = await _controller.APIGetRegisterLoginGoogle(user);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserLoginRegister>>(result);
            var notFoundResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        }



        [Fact]
        public async Task APITestPass2()
        {

            UserLoginRegister user = new UserLoginRegister()
            {
                FirstName = "firstName",
                LastName = "lastName",
                Account = "account",
                AccountGoogle = "deluudulieu7@gmail.com",
                Password = "password",
                Role = 4,
                IsActive = true,
            };


            // Giả lập repository trả về false khi tài khoản không tồn tại
            //_mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
            //               .ReturnsAsync(false);

            // Act
            var result = await _controller.APIGetRegisterLoginGoogle(user);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserLoginRegister>>(result);
            var notFoundResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        }



        [Fact]
        public async Task APITestPass3()
        {

            UserLoginRegister user = new UserLoginRegister()
            {
                FirstName = "firstName",
                LastName = "lastName",
                Account = "account",
                AccountGoogle = "deluudulieu7@gmail.com",
                Password = "password",
                Role = 4,
                IsActive = true,
            };


            // Giả lập repository trả về false khi tài khoản không tồn tại
            //_mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
            //               .ReturnsAsync(false);

            // Act
            var result = await _controller.APIGetRegisterLoginGoogle(user);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserLoginRegister>>(result);
            var notFoundResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        }


    }



    public class TestGetUserById
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly UsersController _controller;
        private IMapper mapper;

        public TestGetUserById()
        {
            // Mock IUserRepository
            _mockRepository = new Mock<IUserRepositories>();
            // Tạo controller và inject mock repository vào
            _controller = new UsersController(mapper);
        }


        [Fact]
        public async Task APITestPass1()
        {

            int idUser = 1;

            // Giả lập repository trả về false khi tài khoản không tồn tại
            //_mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
            //               .ReturnsAsync(false);

            // Act
            var result = await _controller.GetUserById(idUser);

            // Assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var notFoundResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        }


        [Fact]
        public async Task APITestPass2()
        {

            int idUser = 2;

            // Giả lập repository trả về false khi tài khoản không tồn tại
            //_mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
            //               .ReturnsAsync(false);

            // Act
            var result = await _controller.GetUserById(idUser);

            // Assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var notFoundResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        }
        [Fact]
        public async Task APITestPass3()
        {

            int idUser = 3;

            // Giả lập repository trả về false khi tài khoản không tồn tại
            //_mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
            //               .ReturnsAsync(false);

            // Act
            var result = await _controller.GetUserById(idUser);

            // Assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var notFoundResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        }
        [Fact]
        public async Task APITestPass4()
        {

            int idUser = 4;

            // Giả lập repository trả về false khi tài khoản không tồn tại
            //_mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
            //               .ReturnsAsync(false);

            // Act
            var result = await _controller.GetUserById(idUser);

            // Assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var notFoundResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        }

    }



    //public class TestGetAllUser
    //{
    //    private readonly Mock<IUserRepositories> _mockRepository;
    //    private readonly UsersController _controller;
    //    private IMapper mapper;
    //    private Mock<IMapper> _mockMapper;

    //    public TestGetAllUser()
    //    {
    //        // Mock IUserRepository
    //        _mockRepository = new Mock<IUserRepositories>();
    //        // Tạo controller và inject mock repository vào
    //        _controller = new UsersController(mapper);
    //        _mockMapper = new Mock<IMapper>();
    //    }



    //    //[Fact]
    //    //public async Task APITestPass1()
    //    //{

    //    //    // Arrange
    //    //    _mockRepository.Setup(repo => repo.GetAllUsers())
    //    //                   .Returns(new IQueryable<User>()); // Giả lập trả về danh sách rỗng hoặc có dữ liệu.

    //    //    //_mockMapper.Setup(mapper => mapper.Map<CommonUserResponse>(It.IsAny<User>()))
    //    //    //           .Returns(new CommonUserResponse()); // Giả lập mapper.

    //    //    // Act
    //    //    var result = await _controller.GetAllUsers();

    //    //    // Assert
    //    //    var actionResult = Assert.IsType<ActionResult<List<CommonUserResponse>>>(result);
    //    //    Assert.IsType<OkObjectResult>(actionResult.Result);
    //    //}

    //}




    //public class TestGetUsersByRole
    //{
    //    private readonly Mock<IUserRepositories> _mockRepository;
    //    private readonly UsersController _controller;
    //    private IMapper mapper;

    //    public TestGetUsersByRole()
    //    {
    //        // Mock IUserRepository
    //        _mockRepository = new Mock<IUserRepositories>();
    //        // Tạo controller và inject mock repository vào
    //        _controller = new UsersController(mapper);
    //    }


    //    //[Fact]
    //    //public async Task APITestPass1()
    //    //{

    //    //    int role = 4;
    //    //    // Act
    //    //    var result = await _controller.GetUsersByRole(role);

    //    //    // Assert
    //    //    var actionResult = Assert.IsType<ActionResult<List<CommonUserResponse>>>(result);
    //    //    var notFoundResult = Assert.IsType<OkObjectResult>(actionResult.Result);
    //    //}

    //}



    //public class TestGetUserDetail
    //{
    //    private readonly Mock<IUserRepositories> _mockRepository;
    //    private readonly UsersController _controller;
    //    private IMapper mapper;

    //    public TestGetUserDetail()
    //    {
    //        // Mock IUserRepository
    //        _mockRepository = new Mock<IUserRepositories>();
    //        // Tạo controller và inject mock repository vào
    //        _controller = new UsersController(mapper);
    //    }

    //    //[Fact]
    //    //public async Task APITestPass1()
    //    //{

    //    //    int id = 9999;


    //    //    _mockRepository.Setup(repo => repo.GetUserDetailsInfo(id))
    //    //                   .Returns(new User()); // Giả lập trả về danh sách rỗng hoặc có dữ liệu.

    //    //    // Act
    //    //    var result = await _controller.GetUserDetail(id);

    //    //    // Assert
    //    //    var actionResult = Assert.IsType<ActionResult<dynamic>>(result);
    //    //    var notFoundResult = Assert.IsType<BadRequestResult>(actionResult.Result);
    //    //}
    //}




    public class TestGetOnlyUserDetail
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly UsersController _controller;
        private IMapper mapper;

        public TestGetOnlyUserDetail()
        {
            _mockRepository = new Mock<IUserRepositories>();
            _controller = new UsersController(mapper);
        }

        [Fact]
        public async Task APITestPass4()
        {
            // Arrange
            int userId = 5; // Thử với UserId 5
            var userDetail = new UserDetail
            {
                Id = 1,
                UserId = 5,
                DescribeYourself = null,
                Height = 150,
                Weight = 50,
                Age = 25,
                WantImprove = null,
                UnderlyingDisease = null,
                InforConfirmGood = null,
                InforConfirmBad = null,
                IsPremium = null,
                ActivityLevel = 1.2,
                Calo = 1731,
                TimeUpdate = DateTime.Parse("2024-12-06T04:55:18.100"),
                WeightGoal = 2
            };

            // Giả lập trả về dữ liệu UserDetail cho UserId 5
            _mockRepository.Setup(repo => repo.GetUserDetailByUserIdAsync(userId))
                           .ReturnsAsync(userDetail);

            // Act
            var result = await _controller.GetOnlyUserDetail(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Kiểm tra trả về Ok
            var returnValue = Assert.IsType<UserDetail>(okResult.Value); // Kiểm tra kiểu trả về

            // Kiểm tra các giá trị trong userDetail
            Assert.Equal(userId, returnValue.UserId);
            Assert.Equal((short)170, returnValue.Height);
            Assert.Equal((short)50, returnValue.Weight);
            Assert.Equal((short)25, returnValue.Age);
            Assert.Equal(1.2, returnValue.ActivityLevel);
            Assert.Equal(1731, returnValue.Calo);
            Assert.Equal(2, returnValue.WeightGoal);
        }

        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            int userId = 99999; // Sử dụng UserId không tồn tại

            // Giả lập trả về null khi không tìm thấy UserDetail
            _mockRepository.Setup(repo => repo.GetUserDetailByUserIdAsync(userId))
                           .ReturnsAsync((UserDetail)null);

            // Act
            var result = await _controller.GetOnlyUserDetail(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result); // Kiểm tra trả về NotFound
                                                                              //var errorResponse = Assert.IsType<Dictionary<string, string>>(notFoundResult.Value);

            //Assert.Equal("UserDetail not found", errorResponse["message"]); // Kiểm tra thông báo lỗi
        }
    }







    public class TestGetUserPhysicalStatisticsDTOByUserIdAsync
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly UsersController _controller;
        private IMapper mapper;

        public TestGetUserPhysicalStatisticsDTOByUserIdAsync()
        {
            // Mock IUserRepository
            _mockRepository = new Mock<IUserRepositories>();
            // Tạo controller và inject mock repository vào
            _controller = new UsersController(mapper);
        }


        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            int userId = 5; // Thử với UserId 5
            var userPhysicalStatistics = new UserPhysicalStatisticsDTO
            {
                UserId = userId,
                Gender = null,  // Thông tin không có sẵn trong dữ liệu của bạn
                Height = 170,
                Weight = 50,
                Age = 25,
                ActivityLevel = 1.2,
                UnderlyingDisease = null, // Thông tin không có sẵn trong dữ liệu của bạn
                UnderlyingDiseaseNames = new List<string>(), // Không có tên bệnh lý trong dữ liệu của bạn
                TimeUpdate = DateTime.Parse("2024-12-06T04:55:18.100"),
                WeightGoal = 2
            };

            // Giả lập trả về dữ liệu UserPhysicalStatisticsDTO cho UserId 5
            _mockRepository.Setup(repo => repo.GetUserPhysicalStatisticsDTOByUserIdAsync(userId))
                           .ReturnsAsync(userPhysicalStatistics);

            // Act
            var result = await _controller.GetUserPhysicalStatisticsDTOByUserIdAsync(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Kiểm tra trả về Ok
            var returnValue = Assert.IsType<UserPhysicalStatisticsDTO>(okResult.Value); // Kiểm tra kiểu trả về

            // Kiểm tra các giá trị trong UserPhysicalStatisticsDTO
            Assert.Equal(userId, returnValue.UserId);
            Assert.Equal((short)170, returnValue.Height);
            Assert.Equal((short)50, returnValue.Weight);
            Assert.Equal((short)25, returnValue.Age);
            Assert.Equal(1.2, returnValue.ActivityLevel);
            Assert.Equal(2, returnValue.WeightGoal);
            Assert.Null(returnValue.UnderlyingDisease); // Kiểm tra nếu không có bệnh lý
                                                        //Assert.Empty(returnValue.UnderlyingDiseaseNames); // Kiểm tra nếu da
        }

        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            int userId = 99; // Sử dụng UserId không tồn tại

            // Giả lập trả về null khi không tìm thấy UserDetail
            _mockRepository.Setup(repo => repo.GetUserPhysicalStatisticsDTOByUserIdAsync(userId))
                           .ReturnsAsync((UserPhysicalStatisticsDTO)null);

            // Act
            var result = await _controller.GetUserPhysicalStatisticsDTOByUserIdAsync(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result); // Kiểm tra trả về NotFound
                                                                              //var errorResponse = Assert.IsType<Dictionary<string, string>>(notFoundResult.Value);

            //Assert.Equal("UserDetail not found", errorResponse["message"]); // Kiểm tra thông báo lỗi
        }

    }






    public class TestGetNutritionistPackages
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersController _controller;

        public TestGetNutritionistPackages()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IUserRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new UsersController(_mockMapper.Object);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            short packageId = 2;  // Dùng id là 2 như trong dữ liệu mẫu
            var mockPackage = new ExpertPackage
            {
                Id = 2,
                Name = "Gói hỗ trợ 60 ngày",
                Describe = "Gói Hỗ Trợ 60 Ngày Chuyên Gia Dinh Dưỡng là chương trình cá nhân hóa, cung cấp kế hoạch dinh dưỡng khoa học, thực đơn chi tiết, và hỗ trợ liên tục để giúp bạn đạt mục tiêu sức khỏe như giảm cân, tăng cân hoặc cải thiện tình trạng dinh dưỡng. Với các buổi tư vấn chuyên sâu, đánh giá định kỳ, và hướng dẫn thói quen lành mạnh, gói này phù hợp cho mọi đối tượng mong muốn cải thiện sức khỏe một cách bền vững, hiệu quả và khoa học.",
                Price = 100000,
                Duration = 60
            };

            // Giả lập repository trả về một đối tượng ExpertPackage
            _mockRepository.Setup(repo => repo.GetNutritionistPackages(packageId))
                .Returns(mockPackage);

            // Giả lập mapper trả về ExpertPackageResponse
            var mockResponse = new ExpertPackageResponse
            {
                Id = 2,
                Name = "Gói hỗ trợ 60 ngày",
                Describe = "Gói Hỗ Trợ 60 Ngày Chuyên Gia Dinh Dưỡng là chương trình cá nhân hóa, cung cấp kế hoạch dinh dưỡng khoa học, thực đơn chi tiết, và hỗ trợ liên tục để giúp bạn đạt mục tiêu sức khỏe như giảm cân, tăng cân hoặc cải thiện tình trạng dinh dưỡng. Với các buổi tư vấn chuyên sâu, đánh giá định kỳ, và hướng dẫn thói quen lành mạnh, gói này phù hợp cho mọi đối tượng mong muốn cải thiện sức khỏe một cách bền vững, hiệu quả và khoa học.",
                Price = 100000,
                Duration = 60
            };

            _mockMapper.Setup(mapper => mapper.Map<ExpertPackageResponse>(mockPackage))
                .Returns(mockResponse);

            // Act
            var result = await _controller.GetNutritionistPackages(packageId);

            // Assert
            var okResult = Assert.IsType<ActionResult<ExpertPackageResponse>>(result);

        }




        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            short packageId = 3;  // Dùng id là 2 như trong dữ liệu mẫu
            var mockPackage = new ExpertPackage
            {
                Id = 3,
                Name = "Gói hỗ trợ 360 ngày",
                Describe = "Gói Hỗ Trợ 360 Ngày Chuyên Gia Dinh Dưỡng là chương trình cá nhân hóa, cung cấp kế hoạch dinh dưỡng khoa học, thực đơn chi tiết, và hỗ trợ liên tục để giúp bạn đạt mục tiêu sức khỏe như giảm cân, tăng cân hoặc cải thiện tình trạng dinh dưỡng. Với các buổi tư vấn chuyên sâu, đánh giá định kỳ, và hướng dẫn thói quen lành mạnh, gói này phù hợp cho mọi đối tượng mong muốn cải thiện sức khỏe một cách bền vững, hiệu quả và khoa học.",
                Price = 500000,
                Duration = 360
            };

            // Giả lập repository trả về một đối tượng ExpertPackage
            _mockRepository.Setup(repo => repo.GetNutritionistPackages(packageId))
                .Returns(mockPackage);

            // Giả lập mapper trả về ExpertPackageResponse
            var mockResponse = new ExpertPackageResponse
            {
                Id = 3,
                Name = "Gói hỗ trợ 360 ngày",
                Describe = "Gói Hỗ Trợ 360 Ngày Chuyên Gia Dinh Dưỡng là chương trình cá nhân hóa, cung cấp kế hoạch dinh dưỡng khoa học, thực đơn chi tiết, và hỗ trợ liên tục để giúp bạn đạt mục tiêu sức khỏe như giảm cân, tăng cân hoặc cải thiện tình trạng dinh dưỡng. Với các buổi tư vấn chuyên sâu, đánh giá định kỳ, và hướng dẫn thói quen lành mạnh, gói này phù hợp cho mọi đối tượng mong muốn cải thiện sức khỏe một cách bền vững, hiệu quả và khoa học.",
                Price = 500000,
                Duration = 360
            };

            _mockMapper.Setup(mapper => mapper.Map<ExpertPackageResponse>(mockPackage))
                .Returns(mockResponse);

            // Act
            var result = await _controller.GetNutritionistPackages(packageId);

            // Assert
            var okResult = Assert.IsType<ActionResult<ExpertPackageResponse>>(result);

        }

    }


    public class TestUpdateUserPhysicalStatistics
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockRepository2;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersController _controller;

        public TestUpdateUserPhysicalStatistics()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IUserRepositories>();
            _mockRepository2 = new Mock<IUserDetailsRepository>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new UsersController(_mockMapper.Object);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            UserPhysicalStatisticsDTO invalidData = new UserPhysicalStatisticsDTO
            {
                UserId = 0 // UserId không hợp lệ
            };

            // Act
            var result = await _controller.UpdateUserPhysicalStatistics(invalidData);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async Task APITestPass2()
        {
            UserPhysicalStatisticsDTO validData = new UserPhysicalStatisticsDTO
            {
                UserId = 1,
                Height = 170,
                Weight = 65,
                Age = 30,
                ActivityLevel = 1.2,
                WeightGoal = 80
            };

            // Mock SaveUserDetails gọi thành công mà không làm gì
            //_mockRepository.Setup(repo => repo.SaveUserDetails(validData))
            //    .Returns(Task.CompletedTask);

            _mockRepository2.Setup(repo => repo.SaveUserDetails(validData))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateUserPhysicalStatistics(validData);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

        }

    }



    public class TestUpdateUserWeightGoal
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersController _controller;

        public TestUpdateUserWeightGoal()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IUserRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new UsersController(_mockMapper.Object);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            short id = 2;

            // Giả lập repository trả về null khi không tìm thấy nutritionist
            _mockRepository.Setup(repo => repo.GetNutritionistPackages(id))
                .Returns((ExpertPackage)null);

            // Act
            var result = await _controller.GetNutritionistDetail(id);

            // Assert
            //var badRequestResult = Assert.IsType<ActionResult<ExpertPackageResponse>>(result);
            //Assert.Equal("Nutritionist not found!", badRequestResult.Value);
        }

    }








    public class TestUpdateUserStatus
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersController _controller;

        public TestUpdateUserStatus()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IUserRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new UsersController(_mockMapper.Object);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var request = new UpdateUserStatusRequest(UserId: 9999, Status: true);

            _mockRepository.Setup(repo => repo.GetUserById(request.UserId))
                .Returns((User)null); // Giả lập User không tồn tại

            // Act
            var result = await _controller.UpdateUserStatus(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            //Assert.Equal("User not found!", badRequestResult.Value);

        }

        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var request = new UpdateUserStatusRequest(UserId: 1, Status: true);

            var user = new User
            {
                UserId = request.UserId,
                IsActive = false // Trạng thái ban đầu
            };

            _mockRepository.Setup(repo => repo.GetUserById(request.UserId))
                .Returns(user); // Giả lập User tồn tại

            _mockRepository.Setup(repo => repo.UpdateUser(user))
                .Verifiable(); // Xác minh xem phương thức này có được gọi hay không

            // Act
            var result = await _controller.UpdateUserStatus(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            //Assert.Equal("Update user status successfully!", okResult.Value);

            //// Kiểm tra `UpdateUser` được gọi chính xác với đối tượng `user`
            //Assert.True(user.IsActive); // Trạng thái phải được cập nhật
            //_mockRepository.Verify(repo => repo.UpdateUser(user), Times.Once);
        }

    }



    public class TestUpdateUserInfo
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersController _controller;

        public TestUpdateUserInfo()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IUserRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new UsersController(_mockMapper.Object);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var request = new UpdateUserInfoRequest(
                UserId: 99,
                FirstName: "John",
                LastName: "Doe",
                DOB: new DateTime(1990, 1, 1),
                Gender: true,
                Address: "123 Street",
                Phone: "1234567890"
            );

            _mockRepository.Setup(repo => repo.GetUserById(request.UserId))
                .Returns((User)null); // Giả lập người dùng không tồn tại

            // Act
            var result = await _controller.UpdateUserInfo(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("User not found!", badRequestResult.Value);
        }


        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var request = new UpdateUserInfoRequest(
                UserId: 1,
                FirstName: "John",
                LastName: "Doe",
                DOB: new DateTime(1990, 1, 1),
                Gender: true,
                Address: "123 Street",
                Phone: "1234567890"
            );

            var user = new User
            {
                UserId = request.UserId,
                FirstName = "OldFirstName",
                LastName = "OldLastName",
                Dob = new DateTime(1980, 1, 1),
                Gender = false,
                Address = "Old Address",
                Phone = "0987654321"
            };

            _mockRepository.Setup(repo => repo.GetUserById(request.UserId))
                .Returns(user); // Giả lập người dùng tồn tại

            _mockRepository.Setup(repo => repo.UpdateUser(It.IsAny<User>()))
                .Verifiable(); // Xác minh UpdateUser được gọi

            // Act
            var result = await _controller.UpdateUserInfo(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("Update user status successfully!", okResult.Value);
        }
    }




    public class TestChangePassword
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersController _controller;

        public TestChangePassword()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IUserRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new UsersController(_mockMapper.Object);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var request = new ChangePasswordRequest(1, "OldPassword123", "NewPassword123", "NewPassword123");

            _mockRepository.Setup(repo => repo.GetUserById(request.UserId))
                .Returns((User)null); // Giả lập người dùng không tồn tại

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);

        }


        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var request = new ChangePasswordRequest(1, "OldPassword123", " ", " ");
            var user = new User { UserId = request.UserId, Password = "EncryptedOldPassword" };

            _mockRepository.Setup(repo => repo.GetUserById(request.UserId))
                .Returns(user); // Giả lập người dùng tồn tại

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("New password invalid!", badRequestResult.Value);

        }




        [Fact]
        public async Task APITestPass3()
        {
            // Arrange
            var request = new ChangePasswordRequest(1, "WrongOldPassword", "NewPassword123", "NewPassword123");
            var user = new User { UserId = request.UserId, Password = "EncryptedOldPassword" };

            _mockRepository.Setup(repo => repo.GetUserById(request.UserId))
                .Returns(user); // Giả lập người dùng tồn tại

            _mockRepository.Setup(repo => repo.VerifyPassword(request.OldPassword, user.Password))
                .ReturnsAsync(false); // Giả lập xác minh mật khẩu cũ sai

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Old password wrong!", badRequestResult.Value);

        }


        [Fact]
        public async Task APITestPass4()
        {
            // Arrange
            var request = new ChangePasswordRequest(1, "OldPassword123", "NewPassword123", "MismatchPassword");
            var user = new User { UserId = request.UserId, Password = "EncryptedOldPassword" };

            _mockRepository.Setup(repo => repo.GetUserById(request.UserId))
                .Returns(user); // Giả lập người dùng tồn tại

            _mockRepository.Setup(repo => repo.VerifyPassword(request.OldPassword, user.Password))
                .ReturnsAsync(true); // Giả lập mật khẩu cũ đúng

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);

        }


        [Fact]
        public async Task APITestPass5()
        {
            // Arrange
            var request = new ChangePasswordRequest(1, "Admin", "Admin", "Admin");
            var user = new User { UserId = request.UserId, Password = "Admin" };

            _mockRepository.Setup(repo => repo.GetUserById(request.UserId))
                .Returns(user); // Giả lập người dùng tồn tại

            _mockRepository.Setup(repo => repo.VerifyPassword(request.OldPassword, user.Password))
                .ReturnsAsync(true); // Giả lập mật khẩu cũ đúng

            _mockRepository.Setup(repo => repo.EncryptPassword(request.NewPassword))
                .ReturnsAsync("EncryptedNewPassword"); // Giả lập mã hóa mật khẩu mới

            _mockRepository.Setup(repo => repo.UpdateUser(It.IsAny<User>()))
                .Verifiable(); // Xác minh phương thức UpdateUser được gọi

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            var okResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("New password must contain 6-50 characters!", okResult.Value);

        }
       
    }




    public class TestUpdateUserDetails
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersController _controller;

        public TestUpdateUserDetails()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IUserRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new UsersController(_mockMapper.Object);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var request = new UpdateUserDetailsRequest(
                1,
                null,
                170,
                80,
                25,
                "Gain weight",
                null
            );

            _mockUserDetailsRepository.Setup(repo => repo.GetUserDetail(request.UserId))
                .Returns((UserDetail)null); // Giả lập người dùng không tồn tại

            // Act
            var result = await _controller.UpdateUserDetails(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("User not found!", badRequestResult.Value);
        }



        [Fact]
        public async Task APITestPass2()
        {



            //// Arrange
            var request = new UpdateUserDetailsRequest(
                5,
                "Old description",
                170,
                50,
                25,
                "Gain weight",
                null
            );

            var userDetails = new UserDetail
            {
                UserId = request.UserId,
                DescribeYourself = "Old description",
                Height = 170,
                Weight = 50,
                Age = 25,
                WantImprove = "Gain weight",
                UnderlyingDisease = null
            };

            _mockUserDetailsRepository.Setup(repo => repo.GetUserDetail(request.UserId))
                .Returns(userDetails); // Giả lập người dùng tồn tại

            _mockUserDetailsRepository.Setup(repo => repo.UpdateUserDetails(It.IsAny<UserDetail>()))
                .Verifiable(); // Xác minh phương thức UpdateUserDetails được gọi

            // Act
            var result = await _controller.UpdateUserDetails(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("Update user successfully!", okResult.Value);

            // Kiểm tra các trường trong UserDetail đã được cập nhật
            Assert.Equal(request.Describe, userDetails.DescribeYourself);
            Assert.Equal(request.Height, userDetails.Height);
            Assert.Equal(request.Weight, userDetails.Weight);
            Assert.Equal(request.Age, userDetails.Age);
            Assert.Equal(request.WantImprove, userDetails.WantImprove);
            Assert.Equal(request.UnderlyingDisease, userDetails.UnderlyingDisease);
        }
    }




    public class TestUpdateNutritionistDetails
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersController _controller;

        public TestUpdateNutritionistDetails()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IUserRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new UsersController(_mockMapper.Object);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var request = new UpdateNutritionistDetailsRequest(
                999, "Description", 180, 75, 30
            );

            _mockUserDetailsRepository.Setup(repo => repo.GetNutritionistDetail(request.UserId))
                .Returns((NutritionistDetail)null); // Nutritionist không tồn tại

            // Act
            var result = await _controller.UpdateNutritionistDetails(request);

            // Assert
            var badRequestResult = Assert.IsType<ActionResult<string>>(result);
        }


        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var request = new UpdateNutritionistDetailsRequest(
                2, "Description", 180, 75, 30
            );

            _mockUserDetailsRepository.Setup(repo => repo.GetNutritionistDetail(request.UserId))
                .Returns((NutritionistDetail)null); // Nutritionist không tồn tại

            // Act
            var result = await _controller.UpdateNutritionistDetails(request);

            // Assert
            var badRequestResult = Assert.IsType<ActionResult<string>>(result);
        }


    }


    public class TestSaveCollection
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersController _controller;

        public TestSaveCollection()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IUserRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new UsersController(_mockMapper.Object);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            int userId = 7;
            int foodId = 9;

            // Giả lập repository trả về một user hợp lệ
            _mockRepository.Setup(repo => repo.GetUserById(userId))
                .Returns(new User { UserId = userId });

            // Giả lập phương thức SaveCollection không gặp lỗi
            _mockRepository.Setup(repo => repo.SaveCollection(userId, foodId)).Verifiable();

            // Act
            var result = await _controller.SaveCollection(userId, foodId);

            // Assert
            Assert.IsType<NoContentResult>(result); // Xác minh trả về NoContent
        }

        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            int userId = 999;
            int foodId = 9;

            // Giả lập repository trả về null khi không tìm thấy user
            _mockRepository.Setup(repo => repo.GetUserById(userId))
                .Returns((User)null);

            // Act
            var result = await _controller.SaveCollection(userId, foodId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result); // Kiểm tra trả về kiểu BadRequest
            Assert.Equal("User not found!", badRequestResult.Value); // Xác minh thông báo lỗi
            _mockRepository.Verify(repo => repo.SaveCollection(It.IsAny<int>(), It.IsAny<int>()), Times.Never); // Đảm bảo SaveCollection không được gọi
        }
    }




    /// <summary>
    /// /////////////////////////////
    /// </summary>

    public class Test_ToCopy
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersController _controller;

        public Test_ToCopy()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IUserRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new UsersController(_mockMapper.Object);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            short id = 2;

            // Giả lập repository trả về null khi không tìm thấy nutritionist
            _mockRepository.Setup(repo => repo.GetNutritionistPackages(id))
                .Returns((ExpertPackage)null);

            // Act
            var result = await _controller.GetNutritionistDetail(id);

            // Assert
            //var badRequestResult = Assert.IsType<ActionResult<ExpertPackageResponse>>(result);
            //Assert.Equal("Nutritionist not found!", badRequestResult.Value);
        }

    }

    public class TestGetNutritionistDetail
    {
        private readonly Mock<IUserRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersController _controller;

        public TestGetNutritionistDetail()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IUserRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new UsersController(_mockMapper.Object);
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            int nutritionistId = 2;

            // Giả lập repository trả về null khi không tìm thấy nutritionist
            _mockRepository.Setup(repo => repo.GetNutritionistDetailsInfo(nutritionistId))
                .Returns((User)null);

            // Act
            var result = await _controller.GetNutritionistDetail(nutritionistId);

            // Assert
            var badRequestResult = Assert.IsType<ActionResult<dynamic>>(result);
            //Assert.Equal("Nutritionist not found!", badRequestResult.Value);
        }


        [Fact]
        public async Task APITestPass2()
        {

            int idUser = 4;

            // Giả lập repository trả về false khi tài khoản không tồn tại
            //_mockRepository.Setup(repo => repo.CheckAccountUserNull(account, accGoogle))
            //               .ReturnsAsync(false);

            // Act
            var result = await _controller.GetUserById(idUser);

            // Assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var notFoundResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        }
    }

}