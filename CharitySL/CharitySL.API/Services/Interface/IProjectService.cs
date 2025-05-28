using CharitySL.API.Models;

namespace CharitySL.API.Services.Interface
{
	public interface IProjectService
	{
		Task<IEnumerable<ProjectModel>> GetProjectsAsync(string status = "All");
		ProjectDetailModel GetProjectDetails(int projectId);
		CreateProjectResponse CreateProject(CreateProjectRequest createProjectRequest);
		void UpdateProject(int projectId, UpdateProjectRequest updateProjectRequest);
		void DeleteProject(int projectId);
	}
}
