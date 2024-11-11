namespace APILegalizations.Presenter.Entities
{
    public class LoginPresenterReq
    {
        public int IdentityDocument { get; set; }
        public string PasswordHash { get; set; } = null!;
    }
}
