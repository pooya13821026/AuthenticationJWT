using System.ComponentModel.DataAnnotations;
using Authentication.Application.Common.Interfaces;
using MediatR;

namespace Authentication.Application.Commands.User.Update
{
    public class EditUserProfileCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }

    public class EditUserProfileCommandHandler(IIdentityService identityService)
        : IRequestHandler<EditUserProfileCommand, int>
    {
        public async Task<int> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
        {
            var result =
                await identityService.UpdateUserProfile(request.UserId, request.FullName, request.Email,
                    request.PhoneNumber);
            return result ? 1 : 0;
        }
    }
}