using ReelStream.auth.Logic.Interfaces;
using ReelStream.auth.Models.DataTransfer.Form;
using ReelStream.data.Models.Entities;

namespace ReelStream.auth.Models.Buisness
{
    public class NewUser
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }

        private IPasswordManager _passwordManager;

        public NewUser(IPasswordManager _passMan)
        {
            _passwordManager = _passMan;
        }

        public User MapToEntity()
        {
            var user = new User()
            {
                Username = this.Username,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Password = this.Password,
                Salt = this.Salt,
            };

            return user;
        }

        public static NewUser MapFromObject(UserRegistrationForm form, IPasswordManager passwordManager)
        {
            var passwordIngredients = passwordManager.HashPassphrase(new PasswordIngredients { Passphrase = form.Passphrase});
            var newUser = new NewUser(passwordManager)
            {
                Username = form.Username,
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                Password = passwordIngredients.Password,
                Salt = passwordIngredients.Salt,
            };

            return newUser;
        }
    }
}
