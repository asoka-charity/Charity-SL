using CharitySL.API.Data;
using CharitySL.API.Entity;
using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CharitySL.API.Repositories.Implementation
{
	public class UserRepository : BaseRepo, IUserRepository
	{
		public UserRepository(AppDbContext context) : base(context) { }

		public async Task<IEnumerable<UserModel>> GetUsersAsync()
		{
			return await _context.Users
								 .Where(u => u.Role == "USER" && !u.IsDeleted)
								 .Select(q => new UserModel()
								 {
									 Id = q.Id.ToString(),
									 Email = q.Email,
									 FirstName = q.FirstName,
									 LastName = q.LastName,
									 PhoneNumber = q.PhoneNumber,
									 WhatsAppNumber = q.WhatsAppNumber,
									 CreatedAt = q.CreatedAt
								 }).ToListAsync();
		}

		public UserDetailsModel GetUserDetails(string userId, string role)
		{
			var user = _context.Users.FirstOrDefault(q => q.Id == userId && !q.IsDeleted);

			if (role == "USER")
				user = _context.Users.FirstOrDefault(q => q.Guid == userId && !q.IsDeleted);

			if(user == null)
				throw new InvalidOperationException("User not found.");

			var userGroups = _context.UserGroups.Where(q => q.UserId == user.Id && !q.IsDeleted).Select(q => q.GroupId).ToList();

			var groupNames = _context.Groups.Where(q => userGroups.Contains(q.Id)).Select(q => q.Name).ToList();

			TimeZoneInfo canadaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

			return new UserDetailsModel()
			{
				Id = user.Id,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNumber = user.PhoneNumber,
				WhatsAppNumber = user.WhatsAppNumber,
				Street = user.Street,
				City = user.City,
				Country = _context.Countries.FirstOrDefault(q => q.Id == user.CountryId)?.CountryName,
				Province = _context.Provinces.FirstOrDefault(q => q.Id == user.ProvinceId)?.ProvinceName,
				PostalCode = user.PostalCode,
				Guid = user.Guid,
				Description = user.Description,
				UserType = _context.UserTypes.FirstOrDefault(q => q.Id == user.UserTypeId)?.Name,
				IsActive = user.IsActive,
				IntroducedBy = user.IntroducedBy,
				AssignedGroups = groupNames,
				GroupIds = userGroups,
				CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(user.CreatedAt.DateTime, canadaTimeZone).ToString("yyyy/MM/dd hh:mm:ss tt")
			};
		}

		public List<string> GetUserList()
		{
			return _context.Users.Where(u => !u.IsDeleted).Select(q => q.FirstName + " " + q.LastName).ToList();
		}

		public CreateUserResponse CreateUser(CreateUserRequest createUserRequest)
		{
			if (!string.IsNullOrEmpty(createUserRequest.Email))
			{
				if (_context.Users.Any(u => u.Email == createUserRequest.Email && !u.IsDeleted))
					throw new InvalidOperationException("User already exists.");
			}

			UserType? userType = null;

			if (!string.IsNullOrEmpty(createUserRequest.UserType))
			{
				userType = _context.UserTypes.FirstOrDefault(q => q.Name.ToLower() == createUserRequest.UserType.ToLower());

				if (userType == null)
					throw new InvalidOperationException("User type not found.");
			}

			Country? country = null;
			Province? province = null;

			if (!string.IsNullOrEmpty(createUserRequest.Country))
				country = _context.Countries.FirstOrDefault(q => q.CountryName.ToLower() == createUserRequest.Country.ToLower()) ?? throw new InvalidDataException("Country not found");

			if (!string.IsNullOrEmpty(createUserRequest.Province))
				province = _context.Provinces.FirstOrDefault(q => q.ProvinceName.ToLower() == createUserRequest.Province.ToLower()) ?? throw new InvalidDataException("Province not found");

			var user = new User
			{
				Role = "USER",
				FirstName = createUserRequest.FirstName,
				LastName = createUserRequest.LastName,
				Email = createUserRequest.Email,
				PhoneNumber = createUserRequest.PhoneNumber,
				WhatsAppNumber = createUserRequest.WhatsAppNumber,
				Street = createUserRequest.Street,
				City = createUserRequest.City,
				CountryId = country?.Id,
				ProvinceId = province?.Id,
				PostalCode = createUserRequest.PostalCode,
				Description = createUserRequest.Description,
				UserTypeId = userType?.Id,
				IsActive = createUserRequest.IsActive,
				IntroducedBy = createUserRequest.IntroducedBy,
			};

			_context.Users.Add(user);
			_context.SaveChanges();

			var roleId = _context.Roles.FirstOrDefault(q => q.Name == "USER")?.Id ?? throw new InvalidOperationException("Role not found.");
			
			_context.UserRoles.Add(new IdentityUserRole<string> { UserId = user.Id, RoleId = roleId });

			if(createUserRequest.GroupList != null)
			{
				foreach (var item in createUserRequest.GroupList)
				{
					var group = _context.Groups.FirstOrDefault(q => q.Id == item && !q.IsDeleted);
					if (group != null)
					{
						var userGroup = new UserGroup
						{
							UserId = user.Id,
							GroupId = group.Id
						};

						_context.UserGroups.Add(userGroup);
					}
				}
			}

			return new CreateUserResponse { Id = user.Id };
		}

		public void UpdateUser(string userId, UpdateUserRequest updateUserRequest)
		{
			var user = _context.Users
						.Include(u => u.UserGroups) // Ensure we load existing user groups
						.FirstOrDefault(q => q.Id == userId && !q.IsDeleted)
						?? throw new InvalidOperationException("User not found.");

			UserType? userType = null;

			if (!string.IsNullOrEmpty(updateUserRequest.UserType))
			{
				userType = _context.UserTypes.FirstOrDefault(q => q.Name.ToLower() == updateUserRequest.UserType.ToLower());

				if (userType == null)
					throw new InvalidOperationException("User type not found.");
			}

			Country? country = null;
			Province? province = null;

			if (!string.IsNullOrEmpty(updateUserRequest.Country))
				country = _context.Countries.FirstOrDefault(q => q.CountryName.ToLower() == updateUserRequest.Country.ToLower()) ?? throw new InvalidDataException("Country not found");

			if (!string.IsNullOrEmpty(updateUserRequest.Province))
				province = _context.Provinces.FirstOrDefault(q => q.ProvinceName.ToLower() == updateUserRequest.Province.ToLower()) ?? throw new InvalidDataException("Province not found");

			user.FirstName = updateUserRequest.FirstName;
			user.LastName = updateUserRequest.LastName;
			user.Email = updateUserRequest.Email;
			user.PhoneNumber = updateUserRequest.PhoneNumber;
			user.WhatsAppNumber = updateUserRequest.WhatsAppNumber;
			user.Street = updateUserRequest.Street;
			user.City = updateUserRequest.City;
			user.CountryId = country?.Id;
			user.ProvinceId = province?.Id;
			user.PostalCode = updateUserRequest.PostalCode;
			user.Description = updateUserRequest.Description;
			user.UserTypeId = userType?.Id;
			user.IsActive = updateUserRequest.IsActive;
			user.UpdatedAt = DateTimeOffset.Now;

			if (updateUserRequest.GroupIds != null)
			{
				var existingGroupIds = user.UserGroups.Select(ug => ug.GroupId).ToList();
				var newGroupIds = updateUserRequest.GroupIds.ToList();

				// Find groups to mark as deleted (previously assigned but not in the new list)
				var groupsToRemove = user.UserGroups.Where(ug => !newGroupIds.Contains(ug.GroupId) && !ug.IsDeleted).ToList();
				foreach (var group in groupsToRemove)
				{
					group.IsDeleted = true;
					group.UpdatedAt = DateTimeOffset.Now;
				}

				// Find groups to add
				var groupsToAdd = newGroupIds.Except(existingGroupIds);
				foreach (var groupId in groupsToAdd)
				{
					_context.UserGroups.Add(new UserGroup
					{
						UserId = user.Id,
						GroupId = groupId,
						IsDeleted = false,
						CreatedAt = DateTimeOffset.Now
					});
				}
			}
		}

		public void DeleteUser(string userId)
		{
			var user = _context.Users.FirstOrDefault(q => q.Id == userId && !q.IsDeleted) ?? throw new InvalidOperationException("User not found.");

			user.IsDeleted = true;
			user.UpdatedAt = DateTimeOffset.Now;
		}

		public void UpdateUserGuids(List<string> userIds)
		{
			if (userIds == null || userIds.Count == 0)
				throw new InvalidOperationException("User Guid IDs cannot be null or empty.");

			var users = _context.Users.Where(u => userIds.Contains(u.Guid)).ToList();

			foreach (var user in users)
			{
				var newGuid = System.Guid.NewGuid().ToString();
				
				while (_context.Users.Any(u => u.Guid == newGuid))
					newGuid = System.Guid.NewGuid().ToString();

				user.Guid = newGuid;
			}
		}

		public async Task<IEnumerable<UserModel>> GetUsersByUserTypeAsync(string userType)
		{
			var userTypeModel = _context.UserTypes.FirstOrDefault(q => q.Name.ToLower() == userType.ToLower());

			if (userTypeModel == null)
				throw new InvalidOperationException("User type not found");

			return await _context.Users.Where(q => q.UserTypeId == userTypeModel.Id)
										.Select(q => new UserModel()
										{
											Id = q.Id.ToString(),
											Email = q.Email,
											FirstName = q.FirstName,
											LastName = q.LastName,
											PhoneNumber = q.PhoneNumber,
											WhatsAppNumber = q.WhatsAppNumber,
											CreatedAt = q.CreatedAt
										}).ToListAsync();
		}

		public UserDetailsModel GetUserDetailsByEmail(string email)
		{
			var user = _context.Users.FirstOrDefault(q => q.Email == email && !q.IsDeleted) ?? throw new InvalidDataException("User not found");

			return GetUserDetails(user.Id, "");
		}

		public UserProjectModel GetProjectsByUser(string guid)
		{
			var user = _context.Users.FirstOrDefault(q => q.Guid == guid) ?? throw new InvalidOperationException("User not found");

			var projectList = _context.ProjectOwners.Where(q => q.UserId == user.Id).Select(q => q.ProjectId).ToList();

			var userProjectModel = new UserProjectModel
			{
				Guid = guid,
				ProjectsList = new List<ProjectModel>()
			};

			foreach (var project in projectList)
			{
				var projectData = _context.Projects.Where(q => q.Id == project)
													 .Include(u => u.ProjectStatus)
													 .Include(u => u.ProjectCategory).FirstOrDefault();

				if (projectData != null)
				{
					var userProject = new ProjectModel()
					{
						Id = projectData.Id,
						Name = projectData.Name,
						Description = projectData.Description,
						Status = projectData.ProjectStatus?.Name,
						Category = projectData.ProjectCategory?.Name,
						CreatedAt = projectData.CreatedAt
					};

					userProjectModel.ProjectsList.Add(userProject);
				}
			}

			return userProjectModel;
		}
	}
}
