using Authentication.Application.Commands.User.Create;
using Authentication.Application.Commands.User.Delete;
using Authentication.Application.Commands.User.Update;
using Authentication.Application.Common.Exceptions;
using Authentication.Application.DTOs;
using Authentication.Application.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> CreateUser(CreateUserCommand command)
        {
            return Ok(await mediator.Send(command));
        }


        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<UserResponseDTO>))]
        public async Task<IActionResult> GetAllUserAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                throw new BadRequestException("You Are Not Authenticated");
            }

            return Ok(await mediator.Send(new GetUserQuery()));
        }

        [HttpDelete("Delete/{userId}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await mediator.Send(new DeleteUserCommand() { Id = userId });
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("GetUserDetails/{userId}")]
        [ProducesDefaultResponseType(typeof(UserResponseDTO))]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await mediator.Send(new GetUserDetailsQuery() { UserId = userId });
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("GetUserDetailsByUserName/{userName}")]
        [ProducesDefaultResponseType(typeof(UserResponseDTO))]
        public async Task<IActionResult> GetUserDetailsByUserName(string userName)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await mediator.Send(new GetUserDetailsByUserNameQuery() { UserName = userName });
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("EditUserProfile/{userId}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> EditUserProfile(string userId, string fullName, string email, string phone)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await mediator.Send(new EditUserProfileCommand()
                {
                    UserId = userId,
                    FullName = fullName,
                    PhoneNumber = phone,
                    Email = email,
                });
                return Ok(result);
            }

            return BadRequest();
        }
    }
}