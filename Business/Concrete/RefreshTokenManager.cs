using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RefreshTokenManager
    {
        private IRefreshTokenDal _refreshTokenDal;

        public RefreshTokenManager(IRefreshTokenDal refreshTokenDal)
        {
            _refreshTokenDal = refreshTokenDal;
        }

        public IResult Add(RefreshToken refreshToken)
        {
            _refreshTokenDal.Add(refreshToken);
            return new SuccessResult("Refresh token kaydedildi.");
        }

        public IResult ValidateRefreshToken(string token)
        {
            var refreshToken = _refreshTokenDal.Get(rt => rt.Token == token);
            if (refreshToken == null)
            {
                return new ErrorResult("Refresh token bulunamadı.");
            }
            if (refreshToken.IsRevoked)
            {
                return new ErrorResult("Refresh token geçersiz.");
            }
            if (refreshToken.ExpiryDate < DateTime.UtcNow)
            {
                return new ErrorResult("Refresh token süresi dolmuş.");
            }
            return new SuccessResult("Refresh token geçerli.");
        }

        public IResult RevokeToken(string token)
        {
            var refreshToken = _refreshTokenDal.GetByToken(token);
            if(refreshToken == null)
            {
                return new ErrorResult("Refresh token bulunamadı.");
            }

            _refreshTokenDal.RevokeToken(refreshToken.Id);
            return new SuccessResult(" Refresh token iptal edildi.");
        }
    }
}
