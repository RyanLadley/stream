using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.auth.Models.Buisness
{
    public class PasswordIngredients
    {
        public string Password { get; set; }
        public string Passphrase { get; set; }
        public byte[] Salt { get; set; }
    }
}
