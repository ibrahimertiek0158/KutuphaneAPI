using Azure.Core;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegister userForRegisterDto, string password);
        IDataResult<User> Login(UserForLogin userForLoginDto);
        IResult UserExists(string email);

               //Bunlar daha sonra kullanılacak. Şimdilik Yorum satırı yaptım.
        //IDataResult<AccessToken> CreateAccessToken(User user);
        //IDataResult<TokenResponse> CreateTokenResponse(User user);


    }
}
