using ApiWithAuth.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ApiWithAuth.Controllers
{
    public class AuthController : Controller
    {
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly UsersContext _context;
        private readonly TokenService _tokenService;
        private readonly ApiContext _context;

        public AuthController(TokenService tokenService, ApiContext context)
        {
            //_userManager = userManager;
            //_context = context;
            _tokenService = tokenService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

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

            var u = _context.Users.SingleOrDefault(
                x => x.Username == request.Username /*Email*/ && x.Password == request.Password);

            if (u == null) //invalid account
            {
                return BadRequest("Invalid email or password");
            }
            else
            {
                var userInDb = new IdentityUser() { Email = u.Username, UserName = u.Username, Id = Guid.NewGuid().ToString() };
                var accessToken = _tokenService.CreateToken(userInDb);
                //await _context.SaveChangesAsync();
                return Ok(new AuthResponse
                {
                    Username = u.Username,
                    Token = accessToken,
                 });
            }
            //previous implementation if static account
            //var managedUser = await _userManager.FindByEmailAsync(request.Email);
            //if (request.Email == uname && request.Password == upass)
            //{
            //    var userInDb = new IdentityUser() { Email = uname, UserName = uname, Id = Guid.NewGuid().ToString() };
            //    var accessToken = _tokenService.CreateToken(userInDb);
            //    //await _context.SaveChangesAsync();
            //    return Ok(new AuthResponse
            //    {
            //        Username = uname,
            //        Email = uname,
            //        Token = accessToken,
            //    });

            //}
            //else
            //{
            //    return BadRequest("Bad credentials");
            //}

        }
    }
}
