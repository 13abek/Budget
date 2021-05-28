using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Budget.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Budget.Controllers.V1
{
    [Route("api/v1/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly BudgetDbContext _context;
        private readonly IMapper _mapper;
        public TransactionsController(BudgetDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("")]
        public IActionResult TransactionsList()
        {
            return Ok("Transaction List");
        }

        [HttpGet]
        [Route("last-transaction")]
        public IActionResult GetLastTransaction([FromQuery]int limit=5)
        {
            return Ok("last Transactions "+limit);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult TransactionById(int id)
        {
            return Ok("Category id " + id);
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateTransaction()
        {
            return Ok("Create Transaction");
        }
        [HttpPut]
        [Route("")]
        public IActionResult EditTransaction()
        {
            return Ok("Edit Transaction");
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            return Ok("Delete Transaction " + id);
        }


    }
}