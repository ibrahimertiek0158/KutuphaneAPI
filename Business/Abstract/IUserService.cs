using Core.Entities.Concrete;
using Core.Utilities.Results;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByMail(string email);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);
        IDataResult<List<OperationClaim>> GetClaimsByUserId(int userId);
        IDataResult<List<OperationClaim>> GetAllClaims();
        IResult AddUserClaim(UserOperationClaim userOperationClaim);
        IResult DeleteUserClaim(UserOperationClaim userOperationClaim);
        IResult UpdateUserClaims(int userId, List<int> operationClaimIds);
    }
}
