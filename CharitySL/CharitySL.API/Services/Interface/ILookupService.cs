using CharitySL.API.Models;

namespace CharitySL.API.Services.Interface
{
	public interface ILookupService
	{
		LookupDataResponse GetLookupData();
		List<string> GetProvincesByCountry(string country);
	}
}
