using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.auth.Models.Buisness
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
