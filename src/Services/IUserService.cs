using Dtos;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Services;

public interface IUserService 
{
    public User GetUser(string id);
    public Task<IdentityResult> Register(UserDTO userDTO);
    public IEnumerable<User> GetUsers();

}