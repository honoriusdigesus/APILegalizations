namespace APILegalizations.Domain.Entities
{
    public class UserDomainRes
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string? Role { get; set; } = null!;
    }
}
