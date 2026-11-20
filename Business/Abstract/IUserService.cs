using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByMail(string email);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);

        IResult AddUserClaim(UserOperationClaim userOperationClaim);
        IResult DeleteUserClaim(UserOperationClaim userOperationClaim);
        IResult UpdateUserClaim(UserOperationClaim userOperationClaim);



    }
}
