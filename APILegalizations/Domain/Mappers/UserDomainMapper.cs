using APILegalizations.Data.Models;
using APILegalizations.Domain.Entities;

namespace APILegalizations.Domain.Mappers
{
    public class UserDomainMapper
    {
        //From UserDomainReq to User
        public User FromDomainToUser(UserDomainReq userDomainReq)
        {
            return new User
            {
                IdentityDocument = userDomainReq.IdentityDocument,
                Username = userDomainReq.Username,
                Email = userDomainReq.Email,
                PasswordHash = userDomainReq.PasswordHash,
                Role = userDomainReq.Role
            };
        }

        //From User to UserDomainRes
        public UserDomainRes FromUserToDomainRes(User user)
        {
            return new UserDomainRes
            {
                UserId = user.UserId,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
