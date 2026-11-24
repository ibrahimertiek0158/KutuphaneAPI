using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;

        private IHttpContextAccesor _HttpContextAccesor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _HttpContextAccesor = ServiceTool.ServiceProvider.GetService<IHttpContextAccesor>();
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
