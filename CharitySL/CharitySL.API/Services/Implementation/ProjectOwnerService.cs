using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;
using CharitySL.API.Services.Interface;

namespace CharitySL.API.Services.Implementation
{
	public class ProjectOwnerService : IProjectOwnerService
	{
		private readonly IProjectOwnerRepository _projectOwnerRepository;

		public ProjectOwnerService(IProjectOwnerRepository projectOwnerRepository)
		{
			_projectOwnerRepository = projectOwnerRepository;
		}

		public ProjectOwnerModel GetProjectOwners(int projectId)
		{
			return _projectOwnerRepository.GetProjectOwners(projectId);
		}

		public void CreateProjectOwners(int projectId, CreateProjectOwnerRequest createProjectOwnerRequest)
		{
			_projectOwnerRepository.CreateProjectOwners(projectId, createProjectOwnerRequest);
			_projectOwnerRepository.SaveChanges();
		}
	}
}
