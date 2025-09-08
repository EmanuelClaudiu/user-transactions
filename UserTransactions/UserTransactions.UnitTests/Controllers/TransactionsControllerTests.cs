using AutoMapper;
using Moq;
using UserTransactions.API.Controllers;
using UserTransactions.Application.DTOs;
using UserTransactions.Application.Interfaces;
using UserTransactions.Application.Services;
using UserTransactions.Application.Validators;
using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Enums;
using UserTransactions.Domain.Exceptions;
using UserTransactions.Infrastructure.Interfaces;
using UserTransactions.UnitTests.Models.Common;
using UserTransactions.UnitTests.Models.DTOs;
using UserTransactions.UnitTests.Models.Entities;

namespace UserTransactions.UnitTests.Controllers
{
    public class TransactionsControllerTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<ITransactionsRepository> _mockTransactionsRepository;
        private Mock<IUserService> _mockUserService;

        [OneTimeSetUp]
        public void OneTimeStandardSetup()
        {
            _mockMapper = new Mock<IMapper>();
            _mockTransactionsRepository = new Mock<ITransactionsRepository>();
            _mockUserService = new Mock<IUserService>();

            _mockTransactionsRepository.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Returns(Task.FromResult(TransactionMother.Simple()));
            _mockMapper.Setup(x => x.Map<TransactionDTO>(It.IsAny<TransactionCreateDTO>())).Returns(TransactionDtoMother.Simple());
            _mockMapper.Setup(x => x.Map<Transaction>(It.IsAny<TransactionDTO>())).Returns(TransactionMother.Simple());

            _mockUserService.Setup(x => x.GetUserByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(UserDtoMother.Simple()));
        }

        private void SimulateAutomapperForAddMethod(TransactionCreateDTO transactionCreateDto)
        {
            var transactionDto = TransactionDtoMother
                                        .Simple()
                                        .WithValues(transactionCreateDto);
            var transaction = TransactionMother
                                    .Simple()
                                    .WithValues(transactionDto);

            _mockMapper.Setup(x => x.Map<TransactionDTO>(It.IsAny<TransactionCreateDTO>())).Returns(transactionDto);
            _mockMapper.Setup(x => x.Map<Transaction>(It.IsAny<TransactionDTO>())).Returns(transaction);
            _mockMapper.Setup(x => x.Map<TransactionDTO>(It.IsAny<Transaction>())).Returns(transactionDto);
        }
        private TransactionCreateDTO SetupPropertiesForValidAddTestRun()
        {
            var validUserId = GetRandom.RandomString(20);
            var validTransactionAmount = GetRandom.RandomDecimal(0.01m, 10000m);
            var validTransactionType = GetRandom.RandomTransactionType();

            var validUserDto = UserDtoMother.Simple().WithUserId(validUserId);
            _mockUserService
                .Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(validUserDto));

            var validTransactionCreateDto = TransactionCreateDtoMother
                                                .Simple()
                                                .WithUserId(validUserId)
                                                .WithAmount(validTransactionAmount)
                                                .WithTransactionType(validTransactionType);
            SimulateAutomapperForAddMethod(validTransactionCreateDto);

            var validTransactionDto = TransactionDtoMother
                                        .Simple()
                                        .WithValues(validTransactionCreateDto);
            var validTransaction = TransactionMother
                                    .Simple()
                                    .WithValues(validTransactionDto);

            _mockTransactionsRepository.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Returns(Task.FromResult(validTransaction));

            return validTransactionCreateDto;
        }

        [Test]
        public void Given_a_valid_Transaction_Add_Should_Not_Throw()
        {
            // ARRANGE
            var validTransactionCreateDto = SetupPropertiesForValidAddTestRun();

            // ACT + ASSERT
            var sut = new TransactionsController(new TransactionService(
                    _mockTransactionsRepository.Object,
                    _mockMapper.Object,
                    new TransactionValidator(_mockUserService.Object)
                ));

            Assert.DoesNotThrowAsync(async () => await sut.Add(validTransactionCreateDto));
        }

        [Test]
        public void Given_Transaction_with_non_existing_user_Add_Should_Throw_UserNotFoundException()
        {
            // ARRANGE
            var validTransactionCreateDto = SetupPropertiesForValidAddTestRun();
            
            var invalidTransactionCreateDto = validTransactionCreateDto;
            var invalidUserId = "invalid";
            invalidTransactionCreateDto.UserId = invalidUserId;
            SimulateAutomapperForAddMethod(invalidTransactionCreateDto);

            _mockUserService
                .Setup(x => x.GetUserByIdAsync(invalidUserId))
                .Returns(Task.FromResult<UserDTO?>(null));

            // ACT + ASSERT
            var sut = new TransactionsController(new TransactionService(
                    _mockTransactionsRepository.Object,
                    _mockMapper.Object,
                    new TransactionValidator(_mockUserService.Object)
                ));

            Assert.ThrowsAsync<UserNotFoundException>(async () => await sut.Add(invalidTransactionCreateDto));
        }

        [Test]
        public void Given_Transaction_with_amount_too_low_Add_Should_Throw_InvalidTransactionObjectException()
        {
            // ARRANGE
            var validTransactionCreateDto = SetupPropertiesForValidAddTestRun();

            var invalidTransactionCreateDto = validTransactionCreateDto;
            var invalidAmount = 0;
            invalidTransactionCreateDto.Amount = invalidAmount;
            SimulateAutomapperForAddMethod(invalidTransactionCreateDto);

            // ACT + ASSERT
            var sut = new TransactionsController(new TransactionService(
                    _mockTransactionsRepository.Object,
                    _mockMapper.Object,
                    new TransactionValidator(_mockUserService.Object)
                ));

            Assert.ThrowsAsync<InvalidTransactionObjectException>(async () => await sut.Add(invalidTransactionCreateDto));
        }

        [Test]
        public void Given_Transaction_with_invalid_type_Add_Should_Throw_InvalidTransactionObjectException()
        {
            // ARRANGE
            var validTransactionCreateDto = SetupPropertiesForValidAddTestRun();

            var invalidTransactionCreateDto = validTransactionCreateDto;
            var invalidTransactionType = (TransactionTypeEnum)100;
            invalidTransactionCreateDto.TransactionType = invalidTransactionType;
            SimulateAutomapperForAddMethod(invalidTransactionCreateDto);

            // ACT + ASSERT
            var sut = new TransactionsController(new TransactionService(
                    _mockTransactionsRepository.Object,
                    _mockMapper.Object,
                    new TransactionValidator(_mockUserService.Object)
                ));

            Assert.ThrowsAsync<InvalidTransactionObjectException>(async () => await sut.Add(invalidTransactionCreateDto));
        }
    }
}
