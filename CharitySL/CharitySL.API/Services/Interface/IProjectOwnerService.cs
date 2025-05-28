using CharitySL.API.Models;

namespace CharitySL.API.Services.Interface
{
	public interface IProjectOwnerService
	{
		ProjectOwnerModel GetProjectOwners(int projectId);
		void CreateProjectOwners(int projectId, CreateProjectOwnerRequest createProjectOwnerRequest);
	}
}
