using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterceptions
    {
        private string[] _roles;

        private IHttpContextAccessor _HttpContextAccesor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _HttpContextAccesor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
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
