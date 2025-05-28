using CharitySL.API.Models;

namespace CharitySL.API.Services.Interface
{
	public interface IUserService
	{
		Task<IEnumerable<UserModel>> GetUsersAsync();
		UserDetailsModel GetUserDetails(string userId, string role);
		List<string> GetUserList();
		CreateUserResponse CreateUser(CreateUserRequest createUserRequest);
		void UpdateUser(string userId, UpdateUserRequest updateUserRequest);
		void DeleteUser(string userId);
		void UpdateUserGuids(List<string> userIds);
		Task<IEnumerable<UserModel>> GetUsersByUserTypeAsync(string userType);
		UserDetailsModel GetUserDetailsByEmail(string email);
		UserProjectModel GetProjectsByUser(string guid);
	}
}
