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
        Assert.Equal(200, okResult.StatusCode);  // Kiểm tra mã trạng thái HTTP
    }
}