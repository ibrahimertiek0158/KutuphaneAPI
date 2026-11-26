using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRefreshTokenDal : EfEntityRepositoryBase<RefreshToken, LibraryContext>, IRefreshTokenDal
    {
        public RefreshToken GetByToken(string token)
        {
            using (var context = new LibraryContext())
            {
                return context.RefreshTokens.FirstOrDefault(r => r.Token == token && !r.IsRevoked);
            }
        }

        public void RevokeToken(int tokenId)
        {
            using (var context = new LibraryContext())
            {
                var token = context.RefreshTokens.Find(tokenId);
                if (token != null)
                {
                    token.IsRevoked = true;
                    context.SaveChanges();
                }
            }
        }

        public void RevokeAllUserTokens(int userId)
        {
            using (var context = new LibraryContext())
            {
                var userTokens = context.RefreshTokens
                    .Where(r => r.UserId == userId && !r.IsRevoked)
                    .ToList();

                foreach (var token in userTokens)
                {
                    token.IsRevoked = true;
                }
                context.SaveChanges();
            }
        }
    }
}
