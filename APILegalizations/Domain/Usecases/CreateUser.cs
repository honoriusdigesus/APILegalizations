using APILegalizations.Data.Context;
using APILegalizations.Domain.Entities;
using APILegalizations.Domain.Mappers;
using APILegalizations.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace APILegalizations.Domain.Usecases
{
    public class CreateUser
    {
        private readonly LegalizationContext _context;
        private readonly Helper _helper;
        private readonly UserDomainMapper _userDomainMapper;

        public CreateUser(LegalizationContext context, Helper helper, UserDomainMapper userDomainMapper)
        {
            _context = context;
            _helper = helper;
            _userDomainMapper = userDomainMapper;
        }

        public async Task<UserDomainRes> Register(UserDomainReq userDomainReq)
        {
            var user = _userDomainMapper.FromDomainToUser(userDomainReq);
            user.Role = "User";
            user.PasswordHash = _helper.encryptPassword(userDomainReq.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _userDomainMapper.FromUserToDomainRes(user);
        }
    }
}
