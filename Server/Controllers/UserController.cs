using AutoServiss.Server.Infrastructure;
using AutoServiss.Shared.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoServiss.Server.Controllers
{
    public class UserController : MediatorController
    {
        [HttpGet("users")]
        public async Task<IActionResult> List()
        {
            return Ok(await Mediator.Send(new UserListQuery()));
        }

        [HttpPost("users/login")]
        public async Task<IActionResult> Login([FromBody]LoginQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("users")]
        public async Task<IActionResult> Create([FromBody]UserCreateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("users")]
        public async Task<IActionResult> Edit([FromBody]UserEditCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}