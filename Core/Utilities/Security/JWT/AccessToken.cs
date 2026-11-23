using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class TokenResponse
    {
        public AccessToken AccessToken { get; set; }
        public RefreshTokenDto RefreshToken { get; set; }
    }
}
