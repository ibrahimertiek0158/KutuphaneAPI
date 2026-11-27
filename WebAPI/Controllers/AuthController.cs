using Business.Abstract;
using DataAccess.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
    public class AuthController
    {
        private IAuthService _authService;
        private IRefreshTokenDal _refreshTokenDal;
        
        public AuthController(IAuthService authService, IRefreshTokenDal refreshTokenDal)
        {
            _authService = authService;
            _refreshTokenDal = refreshTokenDal;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLogin userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return new BadRequestObjectResult(userToLogin.Message);
            }
            var result = _authService.CreateTokenResponse(userToLogin.Data);
            if (result.Success)
            {
                return new OkObjectResult(result.Data);
            }
            return new BadRequestObjectResult(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegister userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return new BadRequestObjectResult(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateTokenResponse(registerResult.Data);
            if (result.Success)
            {
                return new OkObjectResult(result.Data);

            }
            return new BadRequestObjectResult(result.Message);
        }

        public class RefreshTokenRequest
        {
            public string RefreshToken { get; set; }
        }

        public class LogoutRequest
        {
            public string RefreshToken { get; set; }    
        }
    }
}
