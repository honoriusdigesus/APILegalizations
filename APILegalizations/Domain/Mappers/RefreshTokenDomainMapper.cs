using APILegalizations.Data.Models;
using JWT.Data.Models;

namespace APILegalizations.Domain.Mappers
{
    public class RefreshTokenDomainMapper
    {
        //From RefreshTokenDomain to RefreshToken
        public RefreshToken FromRefreshTokenDomainToRefreshToken(RefreshTokenDomain refreshTokenDomain)
        {
            return new RefreshToken
            {
                Token = refreshTokenDomain.Token,
                Expires = refreshTokenDomain.Expires,
                Created = refreshTokenDomain.Created,
                Revoked = refreshTokenDomain.Revoked,
                UserId = refreshTokenDomain.UserId
            };
        }

        //From RefreshToken to RefreshTokenDomain
        public RefreshTokenDomain FromRefreshTokenToRefreshTokenDomain(RefreshToken refreshToken)
        {
            return new RefreshTokenDomain
            {
                Token = refreshToken.Token,
                Expires = refreshToken.Expires,
                Created = refreshToken.Created,
                Revoked = refreshToken.Revoked,
                UserId = refreshToken.UserId
            };
        }
    }
}
