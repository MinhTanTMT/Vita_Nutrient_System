using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SEP490_G87_Vita_Nutrient_System_API.Controllers;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestProject_SEP490_G87_Vita_Nutrient_System_API.TanTest.TestAPI.TanTestFuntionBankPaymentController
{




    public class TestAPIGetAllRecentTransactions
    {
        private readonly Mock<IBankPaymentRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BankPaymentController _controller;

        public TestAPIGetAllRecentTransactions()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IBankPaymentRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new BankPaymentController();
        }

        [Fact]
        public async Task APITestPass1()
        {

            // Mock repository để trả về danh sách giao dịch giả định
            _mockRepository.Setup(repo => repo.GetAllRecentTransactions())
                           .ReturnsAsync((IEnumerable<Transaction>)(null));

            // Act
            var result = await _controller.APIGetAllRecentTransactions();

            // Assert
            var okResult = Assert.IsType<ActionResult<Transaction>>(result); // Kiểm tra trả về OkObjectResult
        }


        [Fact]
        public async Task APITestPass2()
        {

            // Mock repository để trả về danh sách giao dịch giả định
            _mockRepository.Setup(repo => repo.GetAllRecentTransactions())
                           .ReturnsAsync((IEnumerable<Transaction>)(null));

            // Act
            var result = await _controller.APIGetAllRecentTransactions();

            // Assert
            var okResult = Assert.IsType<ActionResult<Transaction>>(result); // Kiểm tra trả về OkObjectResult
        }


    }


    public class TestAPIGetQRPayDefaultSystem
    {
        private readonly Mock<IBankPaymentRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BankPaymentController _controller;

        public TestAPIGetQRPayDefaultSystem()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IBankPaymentRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new BankPaymentController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            int? idBankInformation = null; // Tham số idBankInformation có thể null
            decimal amount = 100000;
            string content = "Payment for Order #123";

            // Mock repository để trả về null nếu không thể tạo QR
            _mockRepository.Setup(repo => repo.GetQRPayImage(idBankInformation, amount, content))
                           .ReturnsAsync((string)null);

            // Act
            var result = await _controller.APIGetQRPayDefaultSystem(idBankInformation, amount, content);

            // Assert
            var notFoundResult = Assert.IsType<ActionResult<string>>(result); 
        }

        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            int? idBankInformation = null; // Tham số idBankInformation có thể null
            decimal amount = 200;
            string content = "sdfgsdgdsgdgdfg";

            // Mock repository để trả về null nếu không thể tạo QR
            _mockRepository.Setup(repo => repo.GetQRPayImage(idBankInformation, amount, content))
                           .ReturnsAsync((string)null);

            // Act
            var result = await _controller.APIGetQRPayDefaultSystem(idBankInformation, amount, content);

            // Assert
            var notFoundResult = Assert.IsType<ActionResult<string>>(result); // Kiểm tra trả về NotFoundResult
        }


        [Fact]
        public async Task APITestPass3()
        {
            // Arrange
            int? idBankInformation = null; // Tham số idBankInformation có thể null
            decimal amount = 1000000;
            string content = "214213423525";

            // Mock repository để trả về null nếu không thể tạo QR
            _mockRepository.Setup(repo => repo.GetQRPayImage(idBankInformation, amount, content))
                           .ReturnsAsync((string)null);

            // Act
            var result = await _controller.APIGetQRPayDefaultSystem(idBankInformation, amount, content);

            // Assert
            var notFoundResult = Assert.IsType<ActionResult<string>>(result); // Kiểm tra trả về NotFoundResult
        }

    }



    public class TestAPICheckQRPaySuccessful
    {
        private readonly Mock<IBankPaymentRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BankPaymentController _controller;

        public TestAPICheckQRPaySuccessful()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IBankPaymentRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new BankPaymentController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            string accountNumber = "1234567890";
            int limit = 1000;
            string content = "Payment for Order #123";
            decimal amountIn = 150.75m;

            // Mock repository để trả về true (thành công)
            _mockRepository.Setup(repo => repo.CheckQRPaySuccessfulByContent(accountNumber, limit, content, amountIn))
                           .ReturnsAsync(true);

            // Act
            var result = await _controller.APICheckQRPaySuccessful(accountNumber, limit, content, amountIn);

            // Assert
            var okResult = Assert.IsType<ActionResult<dynamic>>(result); // Kiểm tra trả về OkObjectResult
        }



        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            string accountNumber = "1234567890";
            int limit = 1000;
            string content = "Payment for Order #123";
            decimal amountIn = 150.75m;

            // Mock repository để trả về false (thất bại)
            _mockRepository.Setup(repo => repo.CheckQRPaySuccessfulByContent(accountNumber, limit, content, amountIn))
                           .ReturnsAsync(false);

            // Act
            var result = await _controller.APICheckQRPaySuccessful(accountNumber, limit, content, amountIn);

            // Assert
            var badRequestResult = Assert.IsType<ActionResult<dynamic>>(result); // Kiểm tra trả về BadRequestObjectResult
        }


    }



    public class TestModifyDataTransactionsSystem
    {
        private readonly Mock<IBankPaymentRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BankPaymentController _controller;

        public TestModifyDataTransactionsSystem()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IBankPaymentRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new BankPaymentController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            var transactionsSystem = new TransactionsSystemDTO
            {
                Id = 1,
                UserPayId = 1,
                PayeeId = 2,
                Apitransactions = 12345,
                BankBrandName = "ABC Bank",
                AccountNumber = "123456789",
                TransactionDate = DateTime.Now,
                AmountOut = 1000,
                AmountIn = 2000,
                Accumulated = 3000,
                TransactionContent = "Payment for goods",
                ReferenceNumber = "REF123",
                Code = "TXN123",
                SubAccount = "SubAccount1",
                BankAccountId = 789,
                Status = true
            };

            // Mock repository để trả về TransactionsSystemDTO đã sửa đổi
            _mockRepository.Setup(repo => repo.ModifyDataTransactionsSystem(transactionsSystem))
                           .ReturnsAsync(transactionsSystem);

            // Act
            var result = await _controller.APIModifyDataTransactionsSystem(transactionsSystem);

            // Assert
            var okResult = Assert.IsType<ActionResult<TransactionsSystemDTO>>(result); // Kiểm tra trả về OkObjectResult
        }



        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            var transactionsSystem = new TransactionsSystemDTO
            {
                Id = 1,
                UserPayId = 1,
                PayeeId = 3,
                Apitransactions = 12345,
                BankBrandName = "MBBank",
                AccountNumber = "0569000899",
                TransactionDate = DateTime.Now,
                AmountOut = 1000,
                AmountIn = 2000,
                Accumulated = 3000,
                TransactionContent = "ghfghfgh",
                ReferenceNumber = "tg5549girejg8945",
                Code = "fdhgdrhfghnfxdj",
                SubAccount = "SubAccount1",
                BankAccountId = 790,
                Status = true
            };

            // Mock repository để trả về TransactionsSystemDTO đã sửa đổi
            _mockRepository.Setup(repo => repo.ModifyDataTransactionsSystem(transactionsSystem))
                           .ReturnsAsync(transactionsSystem);

            // Act
            var result = await _controller.APIModifyDataTransactionsSystem(transactionsSystem);

            // Assert
            var okResult = Assert.IsType<ActionResult<TransactionsSystemDTO>>(result); // Kiểm tra trả về OkObjectResult
        }

    }



    public class TestAPIGetAllTransactionsSystemOfMonth
    {
        private readonly Mock<IBankPaymentRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BankPaymentController _controller;

        public TestAPIGetAllTransactionsSystemOfMonth()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IBankPaymentRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new BankPaymentController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            int month = 10;
            int year = 2024;
            int userMainId = 1;


            // Mock repository để trả về danh sách giao dịch
            _mockRepository.Setup(repo => repo.GetAllTransactionsSystemOfMonth(month, year, userMainId))
                           .ReturnsAsync((IEnumerable<TransactionsSystem>)(null));
            // Act
            var result = await _controller.APIGetAllTransactionsSystemOfMonth(month, year, userMainId);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<TransactionsSystem>>>(result); // Kiểm tra trả về OkObjectResult

        }



        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            int month = 12;
            int year = 2024;
            int userMainId = 1;


            // Mock repository để trả về danh sách giao dịch
            _mockRepository.Setup(repo => repo.GetAllTransactionsSystemOfMonth(month, year, userMainId))
                           .ReturnsAsync((IEnumerable<TransactionsSystem>)(null));
            // Act
            var result = await _controller.APIGetAllTransactionsSystemOfMonth(month, year, userMainId);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<TransactionsSystem>>>(result);
        }

    }


    public class TestAPIGetAllTransactionsSystemForGraphData
    {
        private readonly Mock<IBankPaymentRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BankPaymentController _controller;

        public TestAPIGetAllTransactionsSystemForGraphData()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IBankPaymentRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new BankPaymentController();
        }

        [Fact]
        public async Task APITestPass1()
        {
            // Arrange
            int year = 2024;
            int userMainId = 1;

            // Giả lập dữ liệu graph (mảng 2 chiều Decimal)
            var graphData = new Decimal[][]
            {
            new Decimal[] { 1000.5m, 2000.0m, 3000.1m },
            new Decimal[] { 1500.2m, 2500.5m, 3500.3m }
            };

            // Mock repository để trả về dữ liệu graph
            _mockRepository.Setup(repo => repo.GetAllTransactionsSystemForGraphData(year, userMainId))
                           .ReturnsAsync(graphData);

            // Act
            var result = await _controller.APIGetAllTransactionsSystemForGraphData(year, userMainId);

            // Assert
            var okResult = Assert.IsType<ActionResult<dynamic>>(result); // Kiểm tra trả về OkObjectResult
        }

        [Fact]
        public async Task APITestPass2()
        {
            // Arrange
            int year = 2024;
            int userMainId = 1;

            // Giả lập dữ liệu graph là mảng rỗng
            var graphData = new Decimal[0][];

            // Mock repository để trả về dữ liệu graph rỗng
            _mockRepository.Setup(repo => repo.GetAllTransactionsSystemForGraphData(year, userMainId))
                           .ReturnsAsync(graphData);

            // Act
            var result = await _controller.APIGetAllTransactionsSystemForGraphData(year, userMainId);

            // Assert
            var okResult = Assert.IsType<ActionResult<dynamic>>(result); // Kiểm tra trả về OkObjectResult

        }



        [Fact]
        public async Task APITestPass3()
        {
            // Arrange
            int year = 2024;
            int userMainId = 999;

            // Giả lập dữ liệu graph trả về null
            Decimal[][] graphData = null;

            // Mock repository để trả về null
            _mockRepository.Setup(repo => repo.GetAllTransactionsSystemForGraphData(year, userMainId))
                           .ReturnsAsync(graphData);

            // Act
            var result = await _controller.APIGetAllTransactionsSystemForGraphData(year, userMainId);

            // Assert
            var okResult = Assert.IsType<ActionResult<dynamic>>(result); // Kiểm tra trả về OkObjectResult

        }

    }

    public class TestAPIGetAllNutritionistServices
    {
        private readonly Mock<IBankPaymentRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BankPaymentController _controller;

        public TestAPIGetAllNutritionistServices()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IBankPaymentRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new BankPaymentController();
        }

        [Fact]
        public async Task APITestPass1()
        {

            // Mock repository để trả về dịch vụ dinh dưỡng
            _mockRepository.Setup(repo => repo.GetAllNutritionistServices())
                           .ReturnsAsync((IEnumerable<ExpertPackageDTO>)(null));

            // Act
            var result = await _controller.APIGetAllNutritionistServices();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<ExpertPackage>>>(result);

        }


    }



    /// <summary>
    /// ///////////////////
    /// </summary>


    public class Test_ToCopy
    {
        private readonly Mock<IBankPaymentRepositories> _mockRepository;
        private readonly Mock<IUserDetailsRepository> _mockUserDetailsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BankPaymentController _controller;

        public Test_ToCopy()
        {
            // Mock IUserRepositories, IUserDetailsRepository và IMapper
            _mockRepository = new Mock<IBankPaymentRepositories>();
            _mockUserDetailsRepository = new Mock<IUserDetailsRepository>();
            _mockMapper = new Mock<IMapper>();

            // Truyền mock vào controller
            _controller = new BankPaymentController();
        }

        [Fact]
        public async Task APITestPass1()
        {

        }

    }
}
