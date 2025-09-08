using Microsoft.AspNetCore.Mvc;
using UserTransactions.Application.DTOs;
using UserTransactions.Application.Interfaces;
using UserTransactions.Application.Services;

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
        public async Task<ActionResult<TransactionDTO>> GetById(int id)
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
    }
}
