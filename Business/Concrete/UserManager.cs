using Business.BusinessAspects.Autofac;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager
    {

        IUserDal _userDal;
        IOperationClaimDal _operationClaimDal;
        IUserOperationClaimDal _userOperationClaimDal;

        public UserManager(IUserDal userDal, IOperationClaimDal operationClaimDal,IUserOperationClaimDal userOperationClaimDal)
        {
            _userDal = userDal;
            _operationClaimDal = operationClaimDal;
            _userOperationClaimDal = userOperationClaimDal;
        }
        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), "Kullanıcılar listelendi.");
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id));
        }

        public IDataResult<List<OperationClaim>> GetClaimsByUserId(int userId)
        {
            var user = _userDal.Get(u => u.Id == userId);
            if (user == null)
            {
                return new ErrorDataResult<List<OperationClaim>>("Kullanıcı bulunamadı.");
            }
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<List<OperationClaim>> GetAllClaims()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }

        public IResult AddUserClaim(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult("Rol başarıyla atandı.");
        }

        public IResult DeleteUserClaim(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult("Rol başarıyla silindi.");
        }

        [SecuredOperation("admin")]
        public IResult UpdateUserClaims(int userId, List<int> operationClaimIds)
        {
            var existingClaims = _userOperationClaimDal.GetAll(u => u.UserId == userId);
            foreach (var claim in existingClaims)
            {
                _userOperationClaimDal.Delete(claim);
            }

            if (operationClaimIds != null)
            {
                foreach (var claimId in operationClaimIds)
                {
                    _userOperationClaimDal.Add(new UserOperationClaim
                    {
                        UserId = userId,
                        OperationClaimId = claimId
                    });
                }
            }

            return new SuccessResult("Kullanıcı rolleri başarıyla güncellendi.");
        }

    }

}

