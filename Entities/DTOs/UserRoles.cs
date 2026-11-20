using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class UserRoles : IDto
    {
        public int UserId { get; set; }

        public List<int> OpearationClaimIds { get; set; }
    }
}
