using SportsEcommerce.Models.Dtos.Users.Requests;

namespace SportsEcommerce.Service.Abstracts;

public interface IRoleService
{
  Task<string> AddRoleToUser(AddRoleToUserRequest request);
  Task<List<string>> GetAllRolesByUserId(string userId);
  Task<string> AddRoleAsync(string name);
}
