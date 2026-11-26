using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterceptions
    {
        private string[] _roles;

        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext?.User?.ClaimRoles();
            if (roleClaims != null)
            {
                foreach (var role in _roles)
                {
                    if (roleClaims.Any(r => string.Equals(r, role.Trim(), StringComparison.OrdinalIgnoreCase)))
                    {
                        return;
                    }
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
