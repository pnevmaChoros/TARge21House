using Microsoft.AspNetCore.Mvc;
using TARge21House.Data;
using TARge21House.Models.House;

namespace TARge21House.Controllers
{
	public class HosuesController : Controller
	{
		private readonly TARge21HouseContext _context;

        public HosuesController
			(
				TARge21HouseContext context
			)
        {
            _context = context;
        }

        public IActionResult Index()
		{
			var result = _context.Houses
				.OrderByDescending(h => h.CreatedAt)
				.Select(x => new HouseIndexViewModel
				{
					Id = x.Id,
					Address = x.Address,
					City = x.City,

					Floors = x.Floors,
					Area = x.Area,
					Price = x.Price,

					CreatedAt = x.CreatedAt,
					ModifiedAt = x.ModifiedAt
				});

			return View(result);
		}
	}
}
