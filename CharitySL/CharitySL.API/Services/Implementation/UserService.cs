using CharitySL.API.Entity;
using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;
using CharitySL.API.Services.Interface;
using System.Data;

namespace CharitySL.API.Services.Implementation
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<IEnumerable<UserModel>> GetUsersAsync()
		{
			return await _userRepository.GetUsersAsync();
		}

		public UserDetailsModel GetUserDetails(string userId, string role)
		{
			return _userRepository.GetUserDetails(userId, role);
		}

		public List<string> GetUserList()
		{
			return _userRepository.GetUserList();
		}

		public CreateUserResponse CreateUser(CreateUserRequest createUserRequest)
		{
			var result = _userRepository.CreateUser(createUserRequest);
			_userRepository.SaveChanges();

			return result;
		}

		public void UpdateUser(string userId, UpdateUserRequest updateUserRequest)
		{
			_userRepository.UpdateUser(userId, updateUserRequest);
			_userRepository.SaveChanges();
		}

		public void DeleteUser(string userId)
		{
			_userRepository.DeleteUser(userId);
			_userRepository.SaveChanges();
		}

		public void UpdateUserGuids(List<string> userIds)
		{
			_userRepository.UpdateUserGuids(userIds);
			_userRepository.SaveChanges();
		}

		public async Task<IEnumerable<UserModel>> GetUsersByUserTypeAsync(string userType)
		{
			return await _userRepository.GetUsersByUserTypeAsync(userType);
		}

		public UserDetailsModel GetUserDetailsByEmail(string email)
		{
			return _userRepository.GetUserDetailsByEmail(email);
		}

		public UserProjectModel GetProjectsByUser(string guid)
		{
			return _userRepository.GetProjectsByUser(guid);
		}
	}
}
