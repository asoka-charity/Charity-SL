using CharitySL.API.Models;

namespace CharitySL.API.Services.Interface
{
	public interface IGroupService
	{
		Task<IEnumerable<GroupModel>> GetGroupsAsync();
		List<string> GetGroupNames();
		GroupDetailsModel GetGroupDetails(int groupId);
		Task<IEnumerable<AssignedUserModel>> GetAssignedUsers(int groupId);
		CreateGroupResponse CreateGroup(CreateGroupRequest createGroupRequest);
		void UpdateGroup(int groupId, UpdateGroupRequest updateGroupRequest);
		void DeleteGroup(int groupId);
	}
}
