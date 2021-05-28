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
    [Route("api/v1/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly BudgetDbContext _context;
        private readonly IMapper _mapper;
        public AccountsController(BudgetDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IActionResult accountsList()
        {
            return Ok("account List");
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult accountById(int id)
        {
            return Ok("account id " + id);
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateAccount()
        {
            return Ok("Create account");
        }
        [HttpPut]
        [Route("")]
        public IActionResult EditAccount()
        {
            return Ok("Edit account");
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            return Ok("Delete account " + id);
        }

    }
}