namespace APILegalizations.Domain.Entities
{
    public class LoginDomainReq
    {
        public int IdentityDocument { get; set; }
        public string PasswordHash { get; set; } = null!;
    }
}
