using CharitySL.API.Models;

namespace CharitySL.API.Repositories.Interface
{
	public interface IProjectContributionRepository : IRepo
	{
		IEnumerable<DonationModel> GetProjectContributions(int projectId);
		DonationDetailModel GetProjectContributionDetails(int id);
		CreateProjectContributionResponse CreateProjectContributions(int projectId, CreateProjectContributionRequest createProjectContributionRequest, string role = "USER");
		void UpdateProjectContribution(int id, UpdateProjectContribution updateRequest);
		void DeleteProjectContribution(int id);
	}
}
