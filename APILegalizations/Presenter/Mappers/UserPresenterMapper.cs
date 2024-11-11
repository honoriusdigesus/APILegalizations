using APILegalizations.Domain.Entities;
using APILegalizations.Presenter.Entities;

namespace APILegalizations.Presenter.Mappers
{
    public class UserPresenterMapper
    {
        //From UserPresenterReq to UserDomainReq
        public UserDomainReq FromPresenterToDomain(UserPresenterReq userPresenterReq)
        {
            return new UserDomainReq
            {
                IdentityDocument = userPresenterReq.IdentityDocument,
                Username = userPresenterReq.Username,
                Email = userPresenterReq.Email,
                PasswordHash = userPresenterReq.PasswordHash
            };
        }

        //From UserDomainReq to UserPresenterReq
        public UserPresenterReq FromDomainToPresenter(UserDomainReq userDomainReq)
        {
            return new UserPresenterReq
            {
                IdentityDocument = userDomainReq.IdentityDocument,
                Username = userDomainReq.Username,
                Email = userDomainReq.Email,
                PasswordHash = userDomainReq.PasswordHash
            };
        }
    }
}
