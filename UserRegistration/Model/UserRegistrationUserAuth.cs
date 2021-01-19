using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistrationAPI.Model
{
    public class UserRegistrationUserAuth
    {
        public UserRegistrationUserAuth() : base()
        {
            UserName = "Not authorized";
            BearerToken = string.Empty;
        }
        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }
    }

}
