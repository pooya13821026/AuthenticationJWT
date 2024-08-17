using Authentication.Application.Common.Interfaces;
using MediatR;


namespace Authentication.Application.Commands.User.Create
{
    public class CreateUserCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public string ConfirmationPassword { get; set; }
    }

    public class CreateUserCommandHandler(IIdentityService identityService) : IRequestHandler<CreateUserCommand, int>
    {
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await identityService.CreateUserAsync(request.UserName, request.Password, request.Email,
                request.FullName, request.PhoneNumber);
            return result.isSucceed ? 1 : 0;
        }
    }
}