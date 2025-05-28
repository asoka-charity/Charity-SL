using CharitySL.API.Models;

namespace CharitySL.API.Repositories.Interface
{
	public interface ILookupRepository
	{
		LookupDataResponse GetLookupData();
		List<string> GetProvincesByCountry(string country);
	}
}
