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

public class UsersRepositoriesTests
{
    private readonly Mock<Sep490G87VitaNutrientSystemContext> _mockContext;
    private readonly UsersRepositories _userRepository;

    public UsersRepositoriesTests()
    {
        _mockContext = new Mock<Sep490G87VitaNutrientSystemContext>();
        _userRepository = new UsersRepositories();
    }
}


public class UsersControllerTests
{
    private readonly Mock<IUserRepositories> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;  // Mock IMapper
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        // Khởi tạo mock repository
        _mockRepository = new Mock<IUserRepositories>();
        _mockMapper = new Mock<IMapper>();

        // Khởi tạo controller với mock repository
        _controller = new UsersController(_mockMapper.Object);
    }

    [Fact]
    public async Task APIForgotPassword_ReturnsOk_WhenForgotPasswordSucceeds()
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
    public async Task APIForgotPassword_ReturnsOk_WhenForgotPasswordFails()
    {
        // Arrange: Mock phương thức ForgotPassword trả về false
        _mockRepository.Setup(repo => repo.ForgotPassword("nonexistent@gmail.com")).ReturnsAsync(false);

        // Act: Gọi API
        var result = await _controller.APIForgotPassword("nonexistent@gmail.com");

        // Assert: Kiểm tra kết quả trả về là OkResult
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(400, okResult.StatusCode);  // Kiểm tra mã trạng thái HTTP
    }
}



public class UserControllerTests
{
    private readonly Mock<IUserRepositories> _mockRepository;
    private readonly UsersController _controller;
    private IMapper mapper;

    public UserControllerTests()
    {
        // Mock IUserRepository
        _mockRepository = new Mock<IUserRepositories>();
        // Tạo controller và inject mock repository vào
        _controller = new UsersController(mapper);
    }

    [Fact]
    public async Task APIGetUserLogin_ReturnsNotFound_WhenUserNotFound()
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
    public async Task APIGetUserLogin_ReturnsOk_WhenUserFound2()
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
    public async Task APIGetUserLogin_ReturnsOk_WhenUserFound3()
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
    public async Task APIGetUserLogin_ReturnsOk_WhenUserFound4()
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
    public async Task APIGetUserLogin_ReturnsOk_WhenUserFound()
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



