using ReelStream.auth.Models.Buisness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReelStream.data.Models.Entities;

namespace ReelStream.auth.Logic.Interfaces
{
    public interface IPasswordManager
    {
        PasswordIngredients HashPassphrase(PasswordIngredients ingredients);
        bool IsValidPassphrase(string passphrase);
        bool VerifyPassword(User storedUser, LoginCredentialsForm credentials);
    }
}
