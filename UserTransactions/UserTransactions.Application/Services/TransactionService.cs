using AutoMapper;
using UserTransactions.Application.DTOs;
using UserTransactions.Application.Interfaces;
using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Enums;
using UserTransactions.Infrastructure.Interfaces;

namespace UserTransactions.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IMapper _mapper;
        private readonly ITransactionValidator _transactionValidator;

        public TransactionService(ITransactionsRepository transactionsRepository, IMapper mapper, ITransactionValidator transactionValidator)
        {
            _transactionsRepository = transactionsRepository;
            _mapper = mapper;
            _transactionValidator = transactionValidator;
        }
        public async Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }
        public async Task<TransactionDTO?> GetTransactionByIdAsync(int id)
        {
            var transaction = await _transactionsRepository.GetByIdAsync(id);
            return _mapper.Map<TransactionDTO>(transaction);
        }
        public async Task<TransactionDTO> AddTransactionAsync(TransactionCreateDTO transactionCreateDto)
        {
            var transactionDto = _mapper.Map<TransactionDTO>(transactionCreateDto);
            transactionDto.CreatedAt = DateTime.Now;

            await _transactionValidator.ValidateTransactionUserAsync(transactionDto);
            _transactionValidator.ValidateTransactionAmount(transactionDto);
            _transactionValidator.ValidateTransactionType(transactionDto);

            var transaction = _mapper.Map<Transaction>(transactionDto);
            var newTransaction = await _transactionsRepository.AddAsync(transaction);

            return _mapper.Map<TransactionDTO>(newTransaction);
        }

        public async Task<decimal> CalculateTotalAmountForUserAsync(string userId)
        {
            // user validation can be done here
            // for simplicity, will return 0 for non existing user

            return await _transactionsRepository.GetTotalAmountForUserAsync(userId);
        }

        public async Task<decimal> CalculateTotalAmountForTransactionTypeAsync(TransactionTypeEnum transactionType)
        {
            // transaction type validation can be done here
            // for simplicity, will return 0 for invalid transaction type

            return await _transactionsRepository.GetTotalAmountForTransactionTypeAsync(transactionType);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsOverTresholdAsync(decimal treshold)
        {
            var transactions = await _transactionsRepository.GetTransactionsOverTresholdAsync(treshold);
            return _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }
    }
}
