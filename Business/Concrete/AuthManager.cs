using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;

        private ITokenHelper _tokenHelper;
        private IRefreshTokenDal _refreshTokenDal;


        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IRefreshTokenDal refreshTokenDal)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _refreshTokenDal = refreshTokenDal;
        }

        public IDataResult<User> Register(UserForRegister userForRegister, string password)
        {
            //Kullanıcının var olup olmadığını kontrol eder.
            var userExists = _userService.GetByMail(userForRegister.Email);
            if (userExists != null)
            {
                return new ErrorDataResult<User>(null, "Bu email adresi zaten kayıtlı.");
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);


            //kullanıcıyı oluşrutur ve veritabanına ekler.
            var user = new User
            {
                Email = userForRegister.Email,
                FirstName = userForRegister.FirstName,
                LastName = userForRegister.LastName,
                Password = password,
                PhoneNumber = userForRegister.PhoneNumber,
                BirthDate= userForRegister.BirthDate,
                Status = true
            };

            _userService.Add(user);
            return new SuccessDataResult<User>(user, "Kayıt oldu.");
        }

        //kullanıcının emailinden var olup olmadığını kontrol eder ve şifre doğruluğunu sorgular. Eğer kullanıcı yoksa veya şifre yanlışsa hata mesajı döndürür. Eğer her şey doğruysa kullanıcıyı döndürür. 
        public IDataResult<User> Login(UserForLogin userForLogin)
        {
            var userToCheck = _userService.GetByMail(userForLogin.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("Kullanıcı bulunamadı.");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLogin.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Parola hatalı.");
            }
            return new SuccessDataResult<User>(userToCheck, "Giriş başarılı.");
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult("Bu email adresi zaten kayıtlı.");
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, "Hoşgeldiniz" + (user.FirstName + " " + user.LastName));
        }

        public IDataResult<TokenResponse> CreateTokenResponse(User user)
        {
            var operationClaims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, operationClaims);
            var refreshToken = _tokenHelper.CreateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken.Token,
                ExpiryDate = refreshToken.ExpiryDate
            };
            _refreshTokenDal.Add(refreshTokenEntity);
            
            var response = new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return new SuccessDataResult<TokenResponse>(response, $"Hoşgeldiniz {user.FirstName} {user.LastName}");
        }
    }
}


