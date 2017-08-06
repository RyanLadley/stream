using ReelStream.auth.Enum;
using ReelStream.auth.Models.Buisness;
using ReelStream.auth.Models.DataTransfer.Form;
using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.auth.Logic.Interfaces
{
    public interface IUserRegistrar
    {
        AuthStatus RegisterUser(UserRegistrationForm registration);
        User ValidateLogin(LoginCredentialsForm credentials);
    }
}
