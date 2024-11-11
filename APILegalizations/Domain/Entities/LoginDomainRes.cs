namespace APILegalizations.Domain.Entities
{
    public class LoginDomainRes
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; }
        public UserDomainRes User { get; set; } = null!;
    }
}
