using CharitySL.API.Models;
using CharitySL.API.Repositories.Implementation;
using CharitySL.API.Repositories.Interface;
using CharitySL.API.Services.Interface;

namespace CharitySL.API.Services.Implementation
{
	public class ProjectContributionService : IProjectContributionService
	{
		private readonly IProjectContributionRepository _projectContributionRepository;

		public ProjectContributionService(IProjectContributionRepository projectContributionRepository)
		{
			_projectContributionRepository = projectContributionRepository;
		}

		public IEnumerable<DonationModel> GetProjectContributions(int projectId)
		{
			return _projectContributionRepository.GetProjectContributions(projectId);
		}

		public DonationDetailModel GetProjectContributionDetails(int id)
		{
			return _projectContributionRepository.GetProjectContributionDetails(id);
		}

		public CreateProjectContributionResponse CreateProjectContributions(int projectId, CreateProjectContributionRequest createProjectContributionRequest, string role = "USER")
		{
			var result = _projectContributionRepository.CreateProjectContributions(projectId, createProjectContributionRequest, role);
			_projectContributionRepository.SaveChanges();

			return result;
		}

		public void UpdateProjectContribution(int id, UpdateProjectContribution updateRequest)
		{
			_projectContributionRepository.UpdateProjectContribution(id, updateRequest);
			_projectContributionRepository.SaveChanges();
		}

		public void DeleteProjectContribution(int id)
		{
			_projectContributionRepository.DeleteProjectContribution(id);
			_projectContributionRepository.SaveChanges();
		}
	}
}
