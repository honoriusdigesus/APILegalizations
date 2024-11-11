using APILegalizations.Domain.Entities;
using APILegalizations.Presenter.Entities;

namespace APILegalizations.Presenter.Mappers
{
    public class LoginPresenterMapper
    {
        //From LoginPresenterReq to LoginDomainReq
        public LoginDomainReq FromLoginPresenterReqToLoginDomainReq(LoginPresenterReq loginPresenterReq)
        {
            return new LoginDomainReq
            {
                IdentityDocument = loginPresenterReq.IdentityDocument,
                PasswordHash = loginPresenterReq.PasswordHash
            };
        }


    }
}
