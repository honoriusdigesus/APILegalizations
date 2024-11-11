namespace APILegalizations.Presenter.Entities
{
    public class UserPresenterReq
    {
        public int IdentityDocument { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
