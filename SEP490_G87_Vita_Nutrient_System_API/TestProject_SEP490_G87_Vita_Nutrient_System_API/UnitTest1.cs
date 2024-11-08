using System.Linq;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using System.Collections.Generic;

public class UsersRepositoriesTests
{
    private readonly Mock<Sep490G87VitaNutrientSystemContext> _mockContext;
    private readonly UsersRepositories _userRepository;

    public UsersRepositoriesTests()
    {
        _mockContext = new Mock<Sep490G87VitaNutrientSystemContext>();
        _userRepository = new UsersRepositories();
    }

    [Fact]
    public void GetUserLogin_ValidUser_ReturnsUserLoginInfo()
    {
        // Arrange
        var mockUsers = new List<User>
        {
            new User { UserId = 1, Account = "test", Password = "password", IsActive = true, FirstName = "Test", LastName = "User", Urlimage = "test.jpg", RoleNavigation = new Role { RoleName = "User" } }
        }.AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(mockUsers.Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(mockUsers.Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(mockUsers.ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(mockUsers.GetEnumerator());

        _mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        // Act
        var result = _userRepository.GetUserLogin("test", "password");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test User", result.FullName);
        Assert.Equal("test.jpg", result.Urlimage);
    }

    [Fact]
    public void CheckExitAccountUser_ExistingAccount_ReturnsFalse()
    {
        // Arrange
        var mockUsers = new List<User>
        {
            new User { UserId = 1, Account = "existingAccount" }
        }.AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(mockUsers.Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(mockUsers.Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(mockUsers.ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(mockUsers.GetEnumerator());

        _mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        // Act
        var result = _userRepository.CheckExitAccountUser("existingAccount");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CheckExitAccountUser_NonExistingAccount_ReturnsTrue()
    {
        // Arrange
        var mockUsers = new List<User>().AsQueryable();

        var mockSet = new Mock<DbSet<User>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(mockUsers.Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(mockUsers.Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(mockUsers.ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(mockUsers.GetEnumerator());

        _mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        // Act
        var result = _userRepository.CheckExitAccountUser("nonExistingAccount");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetUserRegister_NewUser_ReturnsUser()
    {
        // Arrange
        var user = new User { UserId = 1, Account = "newUser", Password = "password" };

        var mockSet = new Mock<DbSet<User>>();
        _mockContext.Setup(c => c.Users).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChanges()).Returns(1);

        // Act
        var result = _userRepository.GetUserRegister(user);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("newUser", result.Account);
    }

    [Fact]
    public void GetUserById_ValidId_ReturnsUser()
    {
        // Arrange
        var user = new User { UserId = 1, Account = "testUser" };

        var mockSet = new Mock<DbSet<User>>();
        mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(user);
        _mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        // Act
        var result = _userRepository.GetUserById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("testUser", result.Account);
    }

    [Fact]
    public void GetUserById_InvalidId_ReturnsNull()
    {
        // Arrange
        var mockSet = new Mock<DbSet<User>>();
        mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns((User)null);
        _mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        // Act
        var result = _userRepository.GetUserById(99);

        // Assert
        Assert.Null(result);
    }
}