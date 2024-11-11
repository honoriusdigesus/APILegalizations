using APILegalizations.Data.Context;
using APILegalizations.Data.Models;
using APILegalizations.Domain.Entities;
using APILegalizations.Domain.Exceptions.Exception;
using APILegalizations.Domain.Mappers;
using APILegalizations.Domain.Utils;
using JWT.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace APILegalizations.Domain.Usecases
{
    public class Login
    {
        private readonly Helper _helper;
        private readonly LegalizationContext _context;
        private readonly RefreshTokenDomainMapper _refreshTokenDomainMapper;
        private readonly UtilsJwt _utilsJwt;
        private readonly UserDomainMapper _userDomainMapper;

        public Login(Helper helper, LegalizationContext context, RefreshTokenDomainMapper refreshTokenDomainMapper, UtilsJwt utilsJwt, UserDomainMapper userDomainMapper)
        {
            _helper = helper;
            _context = context;
            _refreshTokenDomainMapper = refreshTokenDomainMapper;
            _utilsJwt = utilsJwt;
            _userDomainMapper = userDomainMapper;
        }

        public async Task<LoginDomainRes> Execute([FromBody] LoginDomainReq loginDomainReq)
        {

            //Buscamos el usuario por el IdentityDocument
            var user = await _context.Users.FirstOrDefaultAsync(x => x.IdentityDocument == loginDomainReq.IdentityDocument);

            //Si el usuario no existe lanzamos una excepción
            if (user == null)
            {
                throw new LoginException("User not found");
            }

            if (user.PasswordHash != _helper.encryptPassword(loginDomainReq.PasswordHash)) {
                throw new LoginException("Password incorrect");
            }

            var token = _utilsJwt.GenerateJwtToken(_userDomainMapper.FromUserToDomainRes(user));
            var refreshToken = _utilsJwt.GenerateRefreshToken(_userDomainMapper.FromUserToDomainRes(user));
            await _context.SaveChangesAsync(); // Guardamos el refreshToken en la base de datos
            var loginResponse = new LoginDomainRes
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                User = _userDomainMapper.FromUserToDomainRes(user)
            };

            return loginResponse;
        }

    }
}
