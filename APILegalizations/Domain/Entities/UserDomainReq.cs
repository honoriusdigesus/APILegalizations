
namespace APILegalizations.Domain.Entities
{
    public class UserDomainReq
    {
        public int IdentityDocument { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Role { get; set; } = null!;
    }
}
