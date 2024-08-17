using Authentication.Application.Common.Interfaces;
using Authentication.Application.DTOs;
using MediatR;


namespace Authentication.Application.Queries.User
{
    public class GetUserDetailsByUserNameQuery : IRequest<UserResponseDTO>
    {
        public string UserName { get; set; }
    }

    public class
        GetUserDetailsByUserNameQueryHandler(IIdentityService identityService)
        : IRequestHandler<GetUserDetailsByUserNameQuery, UserResponseDTO>
    {
        public async Task<UserResponseDTO> Handle(GetUserDetailsByUserNameQuery request,
            CancellationToken cancellationToken)
        {
            var (userId, fullName, userName, email) =
                await identityService.GetUserDetailsByUserNameAsync(request.UserName);
            return new UserResponseDTO()
                { Id = userId, FullName = fullName, UserName = userName, Email = email };
        }
    }
}