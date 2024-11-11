using APILegalizations.Domain.Usecases;
using APILegalizations.Presenter.Entities;
using APILegalizations.Presenter.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APILegalizations.Presenter.Controller
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CreateUser _createUser;
        private readonly UserPresenterMapper _userPresenterMapper;

        public UserController(CreateUser createUser, UserPresenterMapper userPresenterMapper)
        {
            _createUser = createUser;
            _userPresenterMapper = userPresenterMapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserPresenterReq userPresenterReq)
        {
            var userDomainReq = _userPresenterMapper.FromPresenterToDomain(userPresenterReq);
            var userDomainRes = await _createUser.Register(userDomainReq);
            return Ok(userDomainRes);
        }
    }
}
