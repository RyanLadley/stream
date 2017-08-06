using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.auth.Settings
{
    public class TokenOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expiration { get; set; }
        public string SecurityKey { get; set; }

        private SymmetricSecurityKey _signingKey;
        public SymmetricSecurityKey SigningKey
        {
            get
            {
                if(_signingKey == null)
                    _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));
                return _signingKey;
            }
        }
    }
}
