using Dtos;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Services;

public interface IUserService 
{
    public Task<IdentityResult> Register(UserDTO userDTO);

}