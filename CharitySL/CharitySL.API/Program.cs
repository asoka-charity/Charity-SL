using CharitySL.API.Data;
using CharitySL.API.Entity;
using CharitySL.API.Repositories.Implementation;
using CharitySL.API.Repositories.Interface;
using CharitySL.API.Services.Implementation;
using CharitySL.API.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<IProjectOwnerService, ProjectOwnerService>();
builder.Services.AddScoped<IProjectContributionService, ProjectContributionService>();
builder.Services.AddScoped<IGroupService, GroupService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<ILookupRepository, LookupRepository>();
builder.Services.AddScoped<IProjectOwnerRepository, ProjectOwnerRepository>();
builder.Services.AddScoped<IProjectContributionRepository, ProjectContributionRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
{
	policy.WithOrigins("https://charity-sl.runasp.net", "https://localhost:7119")
	.AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));

var app = builder.Build();

app.UseCors("CorsPolicy");

app.MapOpenApi();

app.UseSwaggerUI(options =>
{
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/openapi/v1.json", "api");
		c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
	});
});

app.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
