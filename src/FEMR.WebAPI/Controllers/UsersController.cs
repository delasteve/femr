using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FEMR.Commands;
using FEMR.Core;
using Microsoft.AspNetCore.Mvc;

namespace FEMR.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ICommandProcessor _commandProcessor;

        public UsersController(ICommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        // GET api/users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET api/users/5
        [HttpGet("{userId}")]
        public string Get(Guid userId)
        {
            return "value";
        }

        // POST api/users
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] string email, string password, string firstName,
            string lastName)
        {
            var userId = Guid.NewGuid();
            await _commandProcessor.Process(new CreateUser(userId, email, password, firstName, lastName));

            var responseMessage = new HttpResponseMessage();
            responseMessage.Headers.Location = new Uri("/users/" + userId);
            responseMessage.StatusCode = HttpStatusCode.Created;

            return responseMessage;
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
