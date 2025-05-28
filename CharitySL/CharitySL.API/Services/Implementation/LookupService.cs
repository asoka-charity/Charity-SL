using CharitySL.API.Models;
using CharitySL.API.Repositories.Interface;
using CharitySL.API.Services.Interface;

namespace CharitySL.API.Services.Implementation
{
	public class LookupService : ILookupService
	{
		private readonly ILookupRepository _lookupRepository;

		public LookupService(ILookupRepository lookupRepository)
		{
			_lookupRepository = lookupRepository;
		}

		public LookupDataResponse GetLookupData()
		{
			return _lookupRepository.GetLookupData();
		}

		public List<string> GetProvincesByCountry(string country)
		{
			return _lookupRepository.GetProvincesByCountry(country);
		}
	}
}
