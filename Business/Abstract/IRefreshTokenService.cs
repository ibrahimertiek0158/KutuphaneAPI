using Core.Entities.Concrete;
using Core.Utilities.Results;


namespace Business.Abstract
{
    public interface IRefreshTokenService
    {
        IResult Add(RefreshToken refreshToken);
        IResult ValidateRefreshToken(string token);
        IResult RevokeToken(string token);

    }
}
