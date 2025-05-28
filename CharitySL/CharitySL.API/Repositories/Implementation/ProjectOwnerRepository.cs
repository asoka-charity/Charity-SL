using CharitySL.API.Data;
using CharitySL.API.Entity;
using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;

namespace CharitySL.API.Repositories.Implementation
{
	public class ProjectOwnerRepository : BaseRepo, IProjectOwnerRepository
	{
		public ProjectOwnerRepository(AppDbContext context) : base(context) { }

		public ProjectOwnerModel GetProjectOwners(int projectId)
		{
			var projectOwners = _context.ProjectOwners.Where(q => q.ProjectId == projectId && !q.IsDeleted).ToList();

			if (!projectOwners.Any())
				return new ProjectOwnerModel();

			var projectOwnerModel = new ProjectOwnerModel
			{
				ProjectId = projectId,
				ProjectOwnerDetails = new List<ProjectOwnerDetailModel>()
			};

			foreach (var owner in projectOwners)
			{
				var user = _context.Users.Where(u => u.Id == owner.UserId).FirstOrDefault();

				var ownerDetailModel = new ProjectOwnerDetailModel();

				if (user != null)
				{
					ownerDetailModel.OwnerId = user.Id;
					ownerDetailModel.OwnerFirstName = user.FirstName;
					ownerDetailModel.OwnerLastName = user.LastName;
					ownerDetailModel.OwnerEmail = user.Email;
					ownerDetailModel.OwnerGuid = user.Guid;

					projectOwnerModel.ProjectOwnerDetails.Add(ownerDetailModel);
				}
			}

			return projectOwnerModel;
		}

		public void CreateProjectOwners(int projectId, CreateProjectOwnerRequest createProjectOwnerRequest)
		{
			var project = _context.Projects.FirstOrDefault(q => q.Id == projectId && !q.IsDeleted);

			if (project == null)
				throw new InvalidOperationException("Project not found.");

			if (createProjectOwnerRequest.UserIds.Count == 0)
				throw new InvalidOperationException("User Ids not found.");

			foreach (var item in createProjectOwnerRequest.UserIds)
			{
				var projectOwners = _context.ProjectOwners.FirstOrDefault(q => q.ProjectId == projectId && q.UserId == item && !q.IsDeleted);

				if (projectOwners == null)
				{
					var projectOwner = new ProjectOwner
					{
						ProjectId = projectId,
						UserId = item,
						IsDeleted = false,
					};

					_context.ProjectOwners.Add(projectOwner);
				}
			}
		}
	}
}
