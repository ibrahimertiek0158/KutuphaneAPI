using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    internal interface IOperationClaimDal: IEntityRepository<OperationClaim>
    {
    }
}
