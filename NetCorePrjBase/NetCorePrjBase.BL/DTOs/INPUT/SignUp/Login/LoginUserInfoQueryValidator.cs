using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePrjBase.BL.DTOs.INPUT.SignUp.Login
{
    public class LoginUserInfoQueryValidator:FluentValidation.AbstractValidator<LoginUserInfoQuery>
    {
        public LoginUserInfoQueryValidator()
        {
            RuleFor(e => e.UserName)
                .NotEmpty()
                .WithMessage("نام کاربری اجباری است");
            RuleFor(e => e.Password)
                .NotEmpty()
                .WithMessage("رمز اجباری است");
        }
    }
}
