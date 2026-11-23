using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class RefreshToken : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}
