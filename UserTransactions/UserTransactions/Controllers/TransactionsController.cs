using Microsoft.AspNetCore.Mvc;
using UserTransactions.Application.DTOs;
using UserTransactions.Application.Interfaces;
using UserTransactions.Domain.Enums;

namespace UserTransactions.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetAll()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return transactions.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO?>> GetById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            return transaction;
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDTO>> Add(TransactionCreateDTO transactionCreateDto)
        {
            var transaction = await _transactionService.AddTransactionAsync(transactionCreateDto);
            return transaction;
        }

        [HttpGet("CalculateTransactionsAmountForUser/{id}")]
        public async Task<ActionResult<decimal>> CalculateTransactionsAmountForUser(string id)
        {
            return await _transactionService.CalculateTotalAmountForUserAsync(id);
        }

        [HttpGet("CalculateTransactionsAmountForType/{transactionType}")]
        public async Task<ActionResult<decimal>> CalculateTransactionsAmountForUser(TransactionTypeEnum transactionType)
        {
            return await _transactionService.CalculateTotalAmountForTransactionTypeAsync(transactionType);
        }

        [HttpGet("GetTransactionsOverTreshold/{treshold}")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsOverTreshold(decimal treshold)
        {
            var transactions = await _transactionService.GetTransactionsOverTresholdAsync(treshold);
            return transactions.ToList();
        }
    }
}
