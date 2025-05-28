using CharitySL.API.Data;
using CharitySL.API.Entity;
using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CharitySL.API.Repositories.Implementation
{
	public class GroupRepository : BaseRepo, IGroupRepository
	{
		public GroupRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<IEnumerable<GroupModel>> GetGroupsAsync()
		{
			return await _context.Groups
								 .Where(u => !u.IsDeleted)
								 .Select(q => new GroupModel()
								 {
									 Id = q.Id,
									 Name = q.Name,
									 Description = q.Description,
									 CreatedAt = q.CreatedAt
								 }).ToListAsync();
		}

		public List<string> GetGroupNames()
		{
			var groups = _context.Groups.Where(q => !q.IsDeleted).OrderBy(q => q.Name).Select(q => q.Name).ToList();

			return groups;
		}

		public GroupDetailsModel GetGroupDetails(int groupId)
		{
			var group = _context.Groups
						.Include(g => g.UserGroups) // Include user-group relationships
						.ThenInclude(ug => ug.User) // Include user details
						.FirstOrDefault(q => q.Id == groupId && !q.IsDeleted)
						?? throw new InvalidOperationException("Group not found.");

			var userGroups = group.UserGroups.Where(ug => !ug.IsDeleted && !ug.User.IsDeleted);

			TimeZoneInfo canadaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

			return new GroupDetailsModel()
			{
				Id = group.Id,
				Name = group.Name,
				Description = group.Description,
				CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(group.CreatedAt.DateTime, canadaTimeZone).ToString("yyyy/MM/dd hh:mm:ss tt"),
				AssignedUserIds = userGroups.Select(ug => ug.User.Id).ToList(),
				AssignedUsers = userGroups.Select(ug => $"{ug.User.FirstName} {ug.User.LastName}").ToList()
			};
		}

		public async Task<IEnumerable<AssignedUserModel>> GetAssignedUsers(int groupId)
		{
			var group = _context.Groups
						.Include(g => g.UserGroups) // Include user-group relationships
						.ThenInclude(ug => ug.User) // Include user details
						.FirstOrDefault(q => q.Id == groupId && !q.IsDeleted)
						?? throw new InvalidOperationException("Group not found.");

			var userGroups = group.UserGroups.Where(ug => !ug.IsDeleted && !ug.User.IsDeleted && !string.IsNullOrEmpty(ug.User.Email));

			var assignedUsers = userGroups
								.Select(ug => new AssignedUserModel
								{
									UserId = ug.User.Id,
									Name = ug.User.FirstName + " " + ug.User?.LastName,
									Email = ug.User?.Email
								}).ToList();

			return assignedUsers;
		}

		public CreateGroupResponse CreateGroup(CreateGroupRequest createGroupRequest)
		{
			if (_context.Groups.Any(u => u.Name == createGroupRequest.Name && !u.IsDeleted))
				throw new InvalidOperationException("Group already exists.");

			var group = new Group
			{
				Name = createGroupRequest.Name,
				Description = createGroupRequest.Description,
			};

			_context.Groups.Add(group);
			_context.SaveChanges();

			if(createGroupRequest.AssignedUsers != null)
			{
				foreach (var item in createGroupRequest.AssignedUsers)
				{
					var userGroup = new UserGroup
					{
						UserId = item,
						GroupId = group.Id,
					};

					_context.UserGroups.Add(userGroup);
				}
			}

			return new CreateGroupResponse { Id = group.Id };
		}

		public void UpdateGroup(int groupId, UpdateGroupRequest updateGroupRequest)
		{
			var group = _context.Groups.Include(u => u.UserGroups).FirstOrDefault(q => q.Id == groupId && !q.IsDeleted) ?? throw new InvalidOperationException("Group not found.");

			group.Name = updateGroupRequest.Name;
			group.Description = updateGroupRequest.Description;

			if (updateGroupRequest.AssignedUserIds != null)
			{
				var existingUserIds = group.UserGroups.Select(ug => ug.UserId).ToList();
				var newUserIds = updateGroupRequest.AssignedUserIds.ToList();

				var usersToRemove = group.UserGroups.Where(ug => !newUserIds.Contains(ug.UserId) && !ug.IsDeleted).ToList();
				foreach (var user in usersToRemove)
				{
					group.IsDeleted = true;
					group.UpdatedAt = DateTimeOffset.Now;
				}

				var usersToAdd = newUserIds.Except(existingUserIds);
				foreach (var userId in usersToAdd)
				{
					_context.UserGroups.Add(new UserGroup
					{
						UserId = userId,
						GroupId = groupId,
						IsDeleted = false,
						CreatedAt = DateTimeOffset.Now
					});
				}
			}
		}

		public void DeleteGroup(int groupId)
		{
			var group = _context.Groups.FirstOrDefault(q => q.Id == groupId && !q.IsDeleted) ?? throw new InvalidOperationException("Group not found.");

			group.IsDeleted = true;
			group.UpdatedAt = DateTimeOffset.UtcNow;
		}
	}
}
