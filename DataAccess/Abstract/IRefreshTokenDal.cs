using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IRefreshTokenDal : IEntityRepository<RefreshToken>
    {
        RefreshToken GetByToken(string token);
        void RevokeToken(int tokenId);
        void RevokeAllUserTokens(int userId);

    }
}
