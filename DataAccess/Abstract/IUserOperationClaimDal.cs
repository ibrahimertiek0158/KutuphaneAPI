using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    internal interface IUserOperationClaimDal : IEntityRepository<UserOperationClaim>
    {
    }
}
