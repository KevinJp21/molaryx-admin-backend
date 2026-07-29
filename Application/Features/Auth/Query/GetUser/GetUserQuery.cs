using Application.Common.Mediator.Interfaces;
using Application.DTOs.Users;

namespace Application.Features.Auth.Query.GetUser
{
    public class GetUserQuery : IRequest<UserDto>{}
}