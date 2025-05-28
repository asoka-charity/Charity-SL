using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;
using CharitySL.API.Services.Interface;

namespace CharitySL.API.Services.Implementation
{
	public class GroupService : IGroupService
	{
		private readonly IGroupRepository _groupRepository;

		public GroupService(IGroupRepository groupRepository)
		{
			_groupRepository = groupRepository;
		}

		public async Task<IEnumerable<GroupModel>> GetGroupsAsync()
		{
			return await _groupRepository.GetGroupsAsync();
		}

		public List<string> GetGroupNames()
		{
			return _groupRepository.GetGroupNames();
		}

		public GroupDetailsModel GetGroupDetails(int groupId)
		{
			return _groupRepository.GetGroupDetails(groupId);
		}

		public async Task<IEnumerable<AssignedUserModel>> GetAssignedUsers(int groupId)
		{
			return await _groupRepository.GetAssignedUsers(groupId);
		}

		public CreateGroupResponse CreateGroup(CreateGroupRequest createGroupRequest)
		{
			var result = _groupRepository.CreateGroup(createGroupRequest);
			_groupRepository.SaveChanges();

			return result;
		}

		public void UpdateGroup(int groupId, UpdateGroupRequest updateGroupRequest)
		{
			_groupRepository.UpdateGroup(groupId, updateGroupRequest);
			_groupRepository.SaveChanges();
		}

		public void DeleteGroup(int groupId)
		{
			_groupRepository.DeleteGroup(groupId);
			_groupRepository.SaveChanges();
		}
	}
}
