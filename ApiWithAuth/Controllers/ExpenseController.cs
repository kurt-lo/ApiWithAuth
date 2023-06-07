using ApiWithAuth.DTO;
using ApiWithAuth.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ApiWithAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly ApiContext _context;
        public ExpenseController(TokenService tokenService, ApiContext context)
        {
            //_userManager = userManager;
            //_context = context;
            _tokenService = tokenService;
            _context = context; 
        }

        //LOGIN USER
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            // for static account
            //string uname = "test@gmail.com";
            //string upass = "samplepassword";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var u = _context.UserExpense_tbl.SingleOrDefault(
                x => x.Username == request.Username /*Email*/ && x.Password == request.Password);

            if (u == null) //invalid account
            {
                return BadRequest("Invalid email or password");
            }
            else
            {
                var userInDb = new IdentityUser() { /*Email = u.Email,*/ UserName = u.Username, Id = Guid.NewGuid().ToString() };
                var accessToken = _tokenService.CreateToken(userInDb);
                //await _context.SaveChangesAsync();
                return Ok(new AuthResponse
                {
                    Username = u.Username,
                    Token = accessToken,
                });
            }
        }

        //REGISTER USER
        [HttpPost("register")]
        public async Task<HttpStatusCode> InsertUser(UserExpenseDTO UserExpense)
        {
            var entity = new UserExpense()
            {
                Firstname = UserExpense.Firstname,
                Lastname = UserExpense.Lastname,
                Email = UserExpense.Email,
                Username = UserExpense.Username,
                Password = UserExpense.Password,
            };
            _context.UserExpense_tbl.Add(entity);
            await _context.SaveChangesAsync();
            return HttpStatusCode.Created;
        }
    }
}
