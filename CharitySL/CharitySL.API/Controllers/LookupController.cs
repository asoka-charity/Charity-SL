using CharitySL.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CharitySL.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LookupController : ControllerBase
	{
		private readonly ILookupService _lookupService;

		public LookupController(ILookupService lookupService)
		{
			_lookupService = lookupService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllLookups()
		{
			return Ok(_lookupService.GetLookupData());
		}

		[HttpGet("{country}/provinces", Name = "GetProvincesByCountry")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult GetProjectsByUser(string country)
		{
			var provinces = _lookupService.GetProvincesByCountry(country);
			return Ok(provinces);
		}
	}
}
