using CharitySL.API.Data;
using CharitySL.API.Entity;
using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CharitySL.API.Repositories.Implementation
{
	public class ProjectRepository : BaseRepo, IProjectRepository
	{
		public ProjectRepository(AppDbContext context) : base(context) { }

		public async Task<IEnumerable<ProjectModel>> GetProjectsAsync(string status = "All")
		{
			if (status == "PENDING")
			{
				return await _context.Projects
								 .Where(u => !u.IsDeleted && u.ProjectStatus.Name.ToLower() == status.ToLower())
								 .Include(u => u.ProjectStatus)
								 .Include(u => u.ProjectCategory)
								 .Select(q => new ProjectModel()
								 {
									 Id = q.Id,
									 Name = q.Name,
									 Description = q.Description,
									 Status = q.ProjectStatus.Name,
									 Category = q.ProjectCategory.Name,
									 CreatedAt = q.CreatedAt
								 }).ToListAsync();
			}
			return await _context.Projects
								 .Where(u => !u.IsDeleted)
								 .Include(u => u.ProjectStatus)
								 .Include(u => u.ProjectCategory)
								 .Select(q => new ProjectModel()
								 {
									 Id = q.Id,
									 Name = q.Name,
									 Description = q.Description,
									 Status = q.ProjectStatus.Name,
									 Category = q.ProjectCategory.Name,
									 CreatedAt = q.CreatedAt
								 }).ToListAsync();
		}

		public ProjectDetailModel GetProjectDetails(int projectId)
		{
			var project = _context.Projects
				.Include(u => u.ProjectStatus)
				.Include(u => u.ProjectCategory)
				.FirstOrDefault(q => q.Id == projectId && !q.IsDeleted) ?? throw new InvalidOperationException("Project not found.");

			TimeZoneInfo canadaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

			return new ProjectDetailModel()
			{
				Id = project.Id,
				Name = project.Name,
				Description = project.Description,
				Status = project.ProjectStatus?.Name,
				Category = project.ProjectCategory?.Name,
				CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(project.CreatedAt.DateTime, canadaTimeZone).ToString("yyyy/MM/dd hh:mm:ss tt")
			};
		}

		public CreateProjectResponse CreateProject(CreateProjectRequest createProjectRequest)
		{
			if (_context.Projects.Any(u => u.Name == createProjectRequest.Name && !u.IsDeleted))
				throw new InvalidOperationException("Project already exists.");

			ProjectStatus? projectStatus = null;
			ProjectCategory? projectCategory = null;

			if (!string.IsNullOrEmpty(createProjectRequest.Status))
			{
				projectStatus = _context.ProjectStatuses.FirstOrDefault(q => q.Name.ToLower() == createProjectRequest.Status.ToLower());

				if (projectStatus == null)
					throw new InvalidOperationException("Project status not found.");
			}

			if (!string.IsNullOrEmpty(createProjectRequest.Category))
			{
				projectCategory = _context.ProjectCategories.FirstOrDefault(q => q.Name.ToLower() == createProjectRequest.Category.ToLower());

				if (projectCategory == null)
					throw new InvalidOperationException("Project category not found.");
			}

			var project = new Project
			{
				Name = createProjectRequest.Name,
				Description = createProjectRequest.Description,
				ProjectStatusId = projectStatus?.Id,
				ProjectCategoryId = projectCategory?.Id
			};

			_context.Projects.Add(project);

			return new CreateProjectResponse { Id = project.Id };
		}

		public void UpdateProject(int projectId, UpdateProjectRequest updateProjectRequest)
		{
			var project = _context.Projects.FirstOrDefault(q => q.Id == projectId && !q.IsDeleted) ?? throw new InvalidOperationException("Project not found.");

			ProjectStatus? projectStatus = null;
			ProjectCategory? projectCategory = null;

			if (!string.IsNullOrEmpty(updateProjectRequest.Status))
			{
				projectStatus = _context.ProjectStatuses.FirstOrDefault(q => q.Name.ToLower() == updateProjectRequest.Status.ToLower());

				if (projectStatus == null)
					throw new InvalidOperationException("Project status not found.");
			}

			if (!string.IsNullOrEmpty(updateProjectRequest.Category))
			{
				projectCategory = _context.ProjectCategories.FirstOrDefault(q => q.Name.ToLower() == updateProjectRequest.Category.ToLower());

				if (projectCategory == null)
					throw new InvalidOperationException("Project category not found.");
			}

			project.Name = updateProjectRequest.Name;
			project.Description = updateProjectRequest.Description;
			project.ProjectStatusId = projectStatus?.Id;
			project.ProjectCategoryId = projectCategory?.Id;
		}

		public void DeleteProject(int projectId)
		{
			var project = _context.Projects.FirstOrDefault(q => q.Id == projectId && !q.IsDeleted) ?? throw new InvalidOperationException("Project not found.");

			project.IsDeleted = true;
			project.UpdatedAt = DateTimeOffset.UtcNow;
		}
	}
}
