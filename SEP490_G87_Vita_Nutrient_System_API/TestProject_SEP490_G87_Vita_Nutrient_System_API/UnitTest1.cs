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





}