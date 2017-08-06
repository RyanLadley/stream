using ReelStream.auth.Enum;
using ReelStream.auth.Logic.Interfaces;
using ReelStream.auth.Models.Buisness;
using ReelStream.auth.Models.DataTransfer.Form;
using ReelStream.data.Models.Entities;
using ReelStream.data.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.auth.Logic
{
    public class UserRegistrar : IUserRegistrar
    {
        private IPasswordManager _passwordManager;
        private IUserRepository _userRepository;

        public UserRegistrar(IUserRepository userRepo, IPasswordManager passMan)
        {
            _passwordManager = passMan;
            _userRepository = userRepo;
        }

        public AuthStatus RegisterUser(UserRegistrationForm registration)
        {
            if (!_passwordManager.IsValidPassphrase(registration.Passphrase))
                return AuthStatus.InvalidPassword;

            var newUser = NewUser.MapFromObject(registration, _passwordManager);

            var user = newUser.MapToEntity();
            user = _userRepository.Add(user);
            
            return AuthStatus.Good;
        }

        public User ValidateLogin(LoginCredentialsForm credentials)
        {
            var storedUser = _userRepository.GetFromUsername(credentials.Username);

            if (storedUser != null && _passwordManager.VerifyPassword(storedUser, credentials))
               return storedUser;
            
            return null;
        }
        
    }
}
