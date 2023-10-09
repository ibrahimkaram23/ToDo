using Microsoft.EntityFrameworkCore;

namespace ToDo.Domain.Models
{
    [Owned]
    public class RefreshToken : BaseModelWithId
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}
