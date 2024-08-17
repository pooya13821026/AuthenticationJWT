using Authentication.Application.Common.Interfaces;
using MediatR;


namespace Authentication.Application.Commands.User.Delete
{
    public class DeleteUserCommand : IRequest<int>
    {
        public string Id { get; set; }
    }

    public class DeleteUserCommandHandler(IIdentityService identityService) : IRequestHandler<DeleteUserCommand, int>
    {
        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await identityService.DeleteUserAsync(request.Id);

            return result ? 1 : 0;
        }
    }
}