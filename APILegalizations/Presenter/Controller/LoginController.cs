using APILegalizations.Domain.Usecases;
using APILegalizations.Presenter.Entities;
using APILegalizations.Presenter.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APILegalizations.Presenter.Controller
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginPresenterMapper _loginPresenterMapper;
        private readonly Login _login;
        private readonly RefreshToken _refreshToken;

        public LoginController(LoginPresenterMapper loginPresenterMapper, Login login, RefreshToken refreshToken)
        {
            _loginPresenterMapper = loginPresenterMapper;
            _login = login;
            _refreshToken = refreshToken;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Login([FromBody] LoginPresenterReq loginPresenterReq)
        {
            var loginDomainReq = _loginPresenterMapper.FromLoginPresenterReqToLoginDomainReq(loginPresenterReq);
            var loginResponse = await _login.Execute(loginDomainReq);
            return Ok(loginResponse);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var loginResponse = await _refreshToken.Refresh(refreshToken);
            return Ok(loginResponse);
        }

        }
}
