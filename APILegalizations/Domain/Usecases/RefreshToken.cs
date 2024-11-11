using APILegalizations.Data.Context;
using APILegalizations.Domain.Entities;
using APILegalizations.Domain.Exceptions.Exception;
using APILegalizations.Domain.Mappers;
using JWT.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APILegalizations.Domain.Usecases
{
    public class RefreshToken
    {
        private readonly LegalizationContext _context;
        private readonly UtilsJwt _utilsJwt;
        private readonly UserDomainMapper _userDomainMapper;

        public RefreshToken(LegalizationContext context, UtilsJwt utilsJwt, UserDomainMapper userDomainMapper)
        {
            _context = context;
            _utilsJwt = utilsJwt;
            _userDomainMapper = userDomainMapper;
        }

        public async Task<LoginDomainRes> Refresh([FromBody] string refreshToken)
        {
            var token = await _context.RefreshTokens
                .Include(rt => rt.User)
                .SingleOrDefaultAsync(rt => rt.Token == refreshToken && rt.Expires > DateTime.Now && rt.Revoked == null); // Buscamos el refreshToken en la base de datos

            if (token == null) throw new LoginException("Invalid token");

            // Generar el nuevo JWT
            var newJwtToken = _utilsJwt.GenerateJwtToken(_userDomainMapper.FromUserToDomainRes(token.User));

            // Generar un nuevo refresh token
            var newRefreshToken = _utilsJwt.GenerateRefreshToken(_userDomainMapper.FromUserToDomainRes(token.User));

            // Revocar el refresh token anterior
            token.Revoked = DateTime.Now;

            // Guardar los cambios en la base de datos
            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            return new LoginDomainRes
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken.Token,
                User = _userDomainMapper.FromUserToDomainRes(token.User)
            };
        }
    }
}
