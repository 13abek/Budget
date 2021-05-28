using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Budget.Resources.Auth;
using Budget.Data;
using Budget.Data.Entities;
using AutoMapper;

namespace Budget.Controllers.V1
{
    [Route("api/v1/auth")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly BudgetDbContext _context;
        private readonly IMapper _mapper;
        public AuthController(BudgetDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginResource loginResource)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = _context.Users.FirstOrDefault(u => u.Email == loginResource.Email);

            if (user!=null && CryptoHelper.Crypto.VerifyHashedPassword(user.Password, loginResource.Password))
            {
                UserToken userToken = new UserToken  // yeni token yaradildi.
                {
                    UserId = user.id,
                    Token = Guid.NewGuid().ToString(),
                    AddedDate = DateTime.Now
                };
                // 2. way
                //UserToken userToken = new UserToken()
                //userToken.id = user.id;
                //userToken.Token = Guid.NewGuid().ToString();
                //userToken.AddedDate = DateTime.Now;

                _context.UserTokens.Add(userToken); 
                _context.SaveChanges();

                                                                
                user.Tokens = new List<UserToken>();    //user-e yeni token elav olundu 
                user.Tokens.Add(userToken);



                var userResource = _mapper.Map<User, UserResource>(user);

                return Ok(userResource);
            }
            return NotFound();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]RegisterResource registerResource)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (_context.Users.Any(u => u.Email == registerResource.Email)) 
            {

                ModelState.AddModelError("Email", "This email already used!");

                return Conflict(ModelState);
                
            }
            var user = _mapper.Map<RegisterResource, User>(registerResource);



            //user token create
            user.Tokens = new  List<UserToken>();
            user.Tokens.Add(new UserToken
            {
                UserId = user.id,
                Token = Guid.NewGuid().ToString(),
                AddedDate = DateTime.Now
            });


            _context.Users.Add(user);
            _context.SaveChanges();


            var userResource = _mapper.Map<User, UserResource>(user);

            return Ok(userResource);
        }
    
    }
}