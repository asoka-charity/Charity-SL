using CharitySL.API.Models;

namespace CharitySL.API.Repositories.Interface
{
	public interface IProjectOwnerRepository : IRepo
	{
		ProjectOwnerModel GetProjectOwners(int projectId);
		void CreateProjectOwners(int projectId, CreateProjectOwnerRequest createProjectOwnerRequest);
	}
}
