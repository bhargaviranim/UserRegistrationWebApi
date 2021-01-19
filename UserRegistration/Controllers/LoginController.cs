using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRegistrationAPI.Model;

namespace UserRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseApiController
    {
        private JWTSettings _settings;
        public LoginController(JWTSettings settings)
        {
            _settings = settings;
        }
   
        [HttpPost]
        public IActionResult Login([FromBody]UserRegistrationUser userRegistrationUser)
        {
            IActionResult ret = null;
            UserRegistrationUserAuth auth = new UserRegistrationUserAuth();
                UserRegistrationSecurityManager mgr = new UserRegistrationSecurityManager(_settings);
            auth = mgr.ValidateUser(userRegistrationUser);
            if (auth.IsAuthenticated)
            {
                ret = StatusCode(StatusCodes.Status200OK, auth);
            }
            else
            {
                ret = StatusCode(StatusCodes.Status404NotFound,
                                 "Invalid User Name/Password.");
            }

            return ret;
        }


    }
}
