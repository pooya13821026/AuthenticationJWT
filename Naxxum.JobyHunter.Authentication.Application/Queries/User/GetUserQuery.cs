using Authentication.Application.Common.Interfaces;
using Authentication.Application.DTOs;
using MediatR;

namespace Authentication.Application.Queries.User
{
    public class GetUserQuery : IRequest<List<UserResponseDTO>>
    {
    }

    public class GetUserQueryHandler(IIdentityService identityService)
        : IRequestHandler<GetUserQuery, List<UserResponseDTO>>
    {
        public async Task<List<UserResponseDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var users = await identityService.GetAllUsersAsync();
            return users.Select(x => new UserResponseDTO()
            {
                Id = x.id,
                FullName = x.fullName,
                UserName = x.userName,
                Email = x.email,
                PhoneNumber = x.phoneNumber
            }).ToList();
        }
    }
}