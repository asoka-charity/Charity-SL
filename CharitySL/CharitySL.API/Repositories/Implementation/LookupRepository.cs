using CharitySL.API.Data;
using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;

namespace CharitySL.API.Repositories.Implementation
{
	public class LookupRepository : BaseRepo, ILookupRepository
	{
		public LookupRepository(AppDbContext context) : base(context)
		{
		}

		public LookupDataResponse GetLookupData()
		{
			LookupDataResponse response = new LookupDataResponse
			{
				UserTypes = _context.UserTypes
					.Select(q => new UserTypeModel()
					{
						Id = q.Id,
						Name = q.Name
					})
					.OrderBy(f => f.Name)
					.ToList(),

				ProjectStatuses = _context.ProjectStatuses.
					Select(q => new ProjectStatusModel()
					{
						Id = q.Id,
						Name = q.Name
					})
					.OrderBy(f => f.Name)
					.ToList(),

				ProjectCategories = _context.ProjectCategories
					.Select(q => new ProjectCategoryModel()
					{
						Id = q.Id,
						Name = q.Name
					})
					.OrderBy(f => f.Name)
					.ToList(),

				PaymentStatuses = _context.PaymentStatuses
					.Select(q => new PaymentStatusModel()
					{
						Id = q.Id,
						Status = q.Status
					})
					.OrderBy(f => f.Status)
					.ToList(),

				PaymentMethods = _context.PaymentMethods
					.Select(q => new PaymentMethodModel()
					{
						Id = q.Id,
						MethodName = q.MethodName
					})
					.OrderBy(f => f.MethodName)
					.ToList(),

				Countries = _context.Countries
					.Select(q => q.CountryName)
					.ToList()
			};

			return response;
		}

		public List<string> GetProvincesByCountry(string country)
		{
			var countryContext = _context.Countries.FirstOrDefault(q => q.CountryName.ToLower() == country.ToLower());

			if (countryContext == null)
				throw new InvalidDataException("Country not found");

			var provinces = _context.Provinces.Where(q => q.CountryId == countryContext.Id).OrderBy(q=>q.ProvinceName).Select(q => q.ProvinceName).ToList();

			return provinces;
		}
	}
}
