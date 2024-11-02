using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePrjBase.BL.DTOs.INPUT.SignUp.Login
{
    public class LoginUserInfoQuery
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
