using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ReelStream.auth.Logic.Interfaces;
using ReelStream.auth.Models.Buisness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ReelStream.data.Models.Entities;

namespace ReelStream.auth.Logic
{
    public class PasswordManager : IPasswordManager
    {
        
        /// <summary>
        /// Hashes the provided password. If salt is porivided, this is used; 
        /// otherwise randomly generated salt is used;
        /// Based off of microsoft Document here: 
        /// https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing
        /// </summary>
        public PasswordIngredients HashPassphrase(PasswordIngredients ingredients)
        {
            if(ingredients.Salt == null)
            {
                ingredients.Salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                    rng.GetBytes(ingredients.Salt);
            }

            ingredients.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: ingredients.Passphrase,
                    salt: ingredients.Salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

            return ingredients;
        }
        
        /// <summary>
        /// TODO: Make conditions for what is and isnt a valid password
        /// </summary>
        /// <param name="passphrase"></param>
        /// <returns></returns>
        public bool IsValidPassphrase(string passphrase)
        {
            return true;
        }

        public bool VerifyPassword(User storedUser, LoginCredentialsForm credentials)
        {
            PasswordIngredients credentialIngredients = new PasswordIngredients() { Passphrase = credentials.Passphrase, Salt = storedUser.Salt };
            credentialIngredients = HashPassphrase(credentialIngredients);

            return storedUser.Password == credentialIngredients.Password;
        }
    }
}
