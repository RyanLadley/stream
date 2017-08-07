using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.auth.Models.DataTransfer.Response
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public UserResponse User{ get; set; }
    }
}
