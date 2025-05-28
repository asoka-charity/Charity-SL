using CharitySL.API.Data;
using CharitySL.API.Entity;
using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;

namespace CharitySL.API.Repositories.Implementation
{
	public class ProjectContributionRepository : BaseRepo, IProjectContributionRepository
	{
		public ProjectContributionRepository(AppDbContext context) : base(context)
		{
		}

		public IEnumerable<DonationModel> GetProjectContributions(int projectId)
		{
			var project = _context.Projects.FirstOrDefault(q => q.Id == projectId && !q.IsDeleted) ?? throw new InvalidOperationException("Project not found.");

			var contributions = (from poc in _context.ProjectContributions
								 join po in _context.ProjectOwners on poc.ProjectOwnerId equals po.Id
								 join p in _context.Projects on po.ProjectId equals p.Id
								 join u in _context.Users on po.UserId equals u.Id
								 join pm in _context.PaymentMethods on poc.PaymentMethodId equals pm.Id
								 join ps in _context.PaymentStatuses on poc.PaymentStatusId equals ps.Id
								 where po.ProjectId == projectId && !poc.IsDeleted && !po.IsDeleted && !p.IsDeleted && !u.IsDeleted
								 select new DonationModel
								 {
									 Id = poc.Id,
									 DonatedUserName = u.FirstName + " " + u.LastName,
									 DonatedUserEmail = u.Email,
									 DonatedAmount = poc.Amount,
									 ActualAmount = poc.ActualAmount,
									 DonationMethod = pm.MethodName,
									 DonationStatus = ps.Status,
									 PaidDate = poc.PaidDate.Value.LocalDateTime,
									 DonationReference = poc.PaymentReference,
									 DonationNote = poc.AdditionalNote
								 }).ToList();

			return contributions;
		}

		public DonationDetailModel GetProjectContributionDetails(int id)
		{
			TimeZoneInfo canadaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

			var donation = (from poc in _context.ProjectContributions
								 join po in _context.ProjectOwners on poc.ProjectOwnerId equals po.Id
								 join p in _context.Projects on po.ProjectId equals p.Id
								 join u in _context.Users on po.UserId equals u.Id
								 join pm in _context.PaymentMethods on poc.PaymentMethodId equals pm.Id
								 join ps in _context.PaymentStatuses on poc.PaymentStatusId equals ps.Id
								 where poc.Id == id && !poc.IsDeleted && !po.IsDeleted && !p.IsDeleted && !u.IsDeleted
								 select new DonationDetailModel
									{
										Id = poc.Id,
										DonatedUser = new UserModel { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email},
										DonatedProject = new ProjectModel { Id = p.Id, Name = p.Name },
										DonatedAmount = poc.Amount,
										ActualAmount = poc.ActualAmount,
										DonationMethod = pm.MethodName,
										DonationStatus = ps.Status,
										PaidDate = poc.PaidDate.Value.LocalDateTime,
										DonationReference = poc.PaymentReference,
										DonationNote = poc.AdditionalNote,
										DonatedOn = TimeZoneInfo.ConvertTimeFromUtc(poc.CreatedAt.DateTime, canadaTimeZone).ToString("yyyy/MM/dd hh:mm:ss tt")
									}).FirstOrDefault();

			return donation;
		}

		public CreateProjectContributionResponse CreateProjectContributions(int projectId, CreateProjectContributionRequest createProjectContributionRequest, string role = "USER")
		{
			var project = _context.Projects.FirstOrDefault(q => q.Id == projectId && !q.IsDeleted) ?? throw new InvalidOperationException("Project not found.");

			var projectOwner = _context.ProjectOwners.FirstOrDefault(q => q.ProjectId == projectId && q.UserId == createProjectContributionRequest.UserId && !q.IsDeleted);

			if (role == "USER")
			{
				var user = _context.Users.FirstOrDefault(q => q.Guid == createProjectContributionRequest.UserId && !q.IsDeleted);
				projectOwner = _context.ProjectOwners.FirstOrDefault(q => q.ProjectId == projectId && q.UserId == user.Id && !q.IsDeleted);
			}
			
			if (projectOwner == null)
			{
				projectOwner = new ProjectOwner
				{
					ProjectId = projectId,
					UserId = createProjectContributionRequest.UserId,
					IsDeleted = false,
				};

				_context.ProjectOwners.Add(projectOwner);
				_context.SaveChanges();
			}
			
			var projectContribution = new ProjectContribution
			{
				ProjectOwnerId = projectOwner.Id,
				Amount = createProjectContributionRequest.Amount,
				PaidDate = createProjectContributionRequest.PaidDate,
				PaymentStatusId = _context.PaymentStatuses.FirstOrDefault(q => q.Status.ToLower().Equals(createProjectContributionRequest.PaymentStatus.ToLower())).Id,
				PaymentMethodId = _context.PaymentMethods.FirstOrDefault(q => q.MethodName.ToLower().Equals(createProjectContributionRequest.PaymentMethod.ToLower())).Id,
				AdditionalNote = createProjectContributionRequest.AdditionalNote,
				PaymentReference = createProjectContributionRequest.PaymentReference,
			};

			_context.ProjectContributions.Add(projectContribution);

			return new CreateProjectContributionResponse { Id = projectContribution.Id };
		}

		public void UpdateProjectContribution(int id, UpdateProjectContribution updateRequest)
		{
			var donation = _context.ProjectContributions.FirstOrDefault(q => q.Id == id && !q.IsDeleted) ?? throw new InvalidOperationException("Project contribution not found.");

			if (updateRequest.ActualAmount == updateRequest.DonatedAmount)
			{
				if (updateRequest.DonationStatus.ToUpper() == "PENDING")
					throw new InvalidOperationException("Please select correct status.");
			}

			donation.ActualAmount = updateRequest.ActualAmount;
			donation.PaymentMethodId = _context.PaymentMethods.FirstOrDefault(q => q.MethodName.ToLower().Equals(updateRequest.DonationMethod.ToLower())).Id;
			donation.PaymentStatusId = _context.PaymentStatuses.FirstOrDefault(q => q.Status.ToLower().Equals(updateRequest.DonationStatus.ToLower())).Id;
			donation.AdditionalNote = updateRequest?.DonationNote;
			donation.PaymentReference = updateRequest?.DonationReference;
			donation.PaidDate = updateRequest?.PaidDate;
		}

		public void DeleteProjectContribution(int id)
		{
			var project = _context.ProjectContributions.FirstOrDefault(q => q.Id == id && !q.IsDeleted) ?? throw new InvalidOperationException("Project contribution not found.");

			project.IsDeleted = true;
			project.UpdatedAt = DateTimeOffset.UtcNow;
		}
	}
}
