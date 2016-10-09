using System.Threading.Tasks;
using FEMR.Core;
using FEMR.Queries;
using FEMR.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FEMR.WebAPI
{
    [Route("api/[controller]")]
    public class AuthController
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IPasswordEncryptor _passwordEncryptor;

        public AuthController(IPasswordEncryptor passwordEncryptor, IQueryProcessor queryProcessor)
        {
            _passwordEncryptor = passwordEncryptor;
            _queryProcessor = queryProcessor;
        }

        [HttpPost("token")]
        public async Task<JsonResult> Post([FromBody] AuthPostModel authPostModel)
        {
            var user = await _queryProcessor.Process(new GetAuthUser(authPostModel.Email));
            var verified = _passwordEncryptor.VerifyPassword(authPostModel.Password, user.Password);

            return new JsonResult(verified);
        }
    }
}
