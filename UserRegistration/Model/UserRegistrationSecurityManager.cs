using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistrationAPI.Model
{
    public class UserRegistrationSecurityManager
    {
        private JWTSettings _settings = null;
        public UserRegistrationSecurityManager(JWTSettings settings)
        {
            _settings = settings;
        }


        public UserRegistrationUserAuth ValidateUser(UserRegistrationUser user)
        {
            UserRegistrationUserAuth ret = new UserRegistrationUserAuth();
            UserRegistrationUser userRegistrationUser = null;

            using (var db = new URDbcontext())
            {
                // Facing issues with DB connectivity. Working on this.
                List<UserRegistrationUser> userRegistrationUsers = RegisteredUsers();

                // Attempt to validate user

                userRegistrationUser = userRegistrationUsers.Where(
                  u => u.UserName.ToLower() == user.UserName.ToLower()
                  && u.Password == user.Password).FirstOrDefault();

                //userRegistrationUser = db.users.Where(
                //  u => u.UserName.ToLower() == user.UserName.ToLower()
                //  && u.Password == user.Password).FirstOrDefault();
            }

            if (userRegistrationUser != null)
            {
                // Build User Security Object
                ret = BuildUserAuthObject(userRegistrationUser);
            }

            return ret;
        }

        private static List<UserRegistrationUser> RegisteredUsers()
        {
            return new List<UserRegistrationUser>()
                {
                    new UserRegistrationUser(){UserId=new Guid(),UserName="UserRegistrationUser2",Password="UserRegistrationUser@2"},
                    new UserRegistrationUser() { UserId=new Guid(),UserName="UserRegistrationUser3",Password="UserRegistrationUser@3" },
                    new UserRegistrationUser(){UserId=new Guid(),UserName="UserRegistrationUser4",Password="UserRegistrationUser@4"},
                    new UserRegistrationUser(){UserId=new Guid(),UserName="UserRegistrationUser5",Password="UserRegistrationUser@5"}

                };
        }

        protected UserRegistrationUserAuth BuildUserAuthObject(UserRegistrationUser userRegistrationUser)
        {
            UserRegistrationUserAuth ret = new UserRegistrationUserAuth();
      

            // Set User Properties
            ret.UserName = userRegistrationUser.UserName;
            ret.IsAuthenticated = true;
            ret.BearerToken = new Guid().ToString();

          //  Set JWT bearer token
            ret.BearerToken = BuildJwtToken(ret);

            return ret;
        }

        //Building the JwtToken 
        protected string BuildJwtToken(UserRegistrationUserAuth userRegistrationUserAuth)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(_settings.Key));


            // Create the JwtSecurityToken object
            var token = new JwtSecurityToken(
              issuer: _settings.Issuer,
              audience: _settings.Audience,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddMinutes(
                  _settings.MinutesToExpiration),
              signingCredentials: new SigningCredentials(key,
                          SecurityAlgorithms.HmacSha256)
            );

            // Create a string representation of the Jwt token
            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }



    }
}


