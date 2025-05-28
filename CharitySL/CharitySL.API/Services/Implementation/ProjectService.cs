using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;
using CharitySL.API.Services.Interface;

namespace CharitySL.API.Services.Implementation
{
	public class ProjectService : IProjectService
	{
		private readonly IProjectRepository _projectRepository;

		public ProjectService(IProjectRepository projectRepository)
		{
			_projectRepository = projectRepository;
		}

		public async Task<IEnumerable<ProjectModel>> GetProjectsAsync(string status = "All")
		{
			return await _projectRepository.GetProjectsAsync(status);
		}

		public ProjectDetailModel GetProjectDetails(int projectId)
		{
			return _projectRepository.GetProjectDetails(projectId);
		}

		public CreateProjectResponse CreateProject(CreateProjectRequest createProjectRequest)
		{
			var result = _projectRepository.CreateProject(createProjectRequest);
			_projectRepository.SaveChanges();

			return result;
		}

		public void UpdateProject(int projectId, UpdateProjectRequest updateProjectRequest)
		{
			_projectRepository.UpdateProject(projectId, updateProjectRequest);
			_projectRepository.SaveChanges();
		}

		public void DeleteProject(int projectId)
		{
			_projectRepository.DeleteProject(projectId);
			_projectRepository.SaveChanges();
		}
	}
}
