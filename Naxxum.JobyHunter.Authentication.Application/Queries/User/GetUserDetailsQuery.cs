using Authentication.Application.Common.Interfaces;
using Authentication.Application.DTOs;
using MediatR;

namespace Authentication.Application.Queries.User
{
    public class GetUserDetailsQuery : IRequest<UserResponseDTO>
    {
        public string UserId { get; set; }
    }

    public class GetUserDetailsQueryHandler(IIdentityService identityService)
        : IRequestHandler<GetUserDetailsQuery, UserResponseDTO>
    {
        public async Task<UserResponseDTO> Handle(GetUserDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var (userId, fullName, userName, email) = await identityService.GetUserDetailsAsync(request.UserId);
            return new UserResponseDTO()
                { Id = userId, FullName = fullName, UserName = userName, Email = email };
        }
    }
}