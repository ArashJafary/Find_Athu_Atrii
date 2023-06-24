using Find_AthuMethod.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Find_AthuMethod.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {

        private readonly Jwtservices _jwtService;

        public UserController(Jwtservices jwtservices)
        {
            _jwtService = jwtservices;
        }

        [Authorize]
        [HttpPost(Name = "GetToken")]
        public string GetToken(User user)
        {
            var Token=_jwtService.CreateToken(user);
            return Token;
        }

        [AllowAnonymous]
        [HttpGet(Name = "Get")]
        public string[] Get()
        {
            return FindAthuService.Find();
        }


        [HttpGet(Name = "GetSecret")]
        [Authorize]
        public string GetSecret()
        {
            return "546546";
        }
    }
}