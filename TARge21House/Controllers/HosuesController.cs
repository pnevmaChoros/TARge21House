using Microsoft.AspNetCore.Mvc;
using TARge21House.Core.Dto;
using TARge21House.Core.ServiceInterface;
using TARge21House.Data;
using TARge21House.Models.House;

namespace TARge21House.Controllers
{
	public class HosuesController : Controller
	{
		private readonly TARge21HouseContext _context;
		private readonly IHousesServices _housesServices;

        public HosuesController
			(
				TARge21HouseContext context,
				IHousesServices housesServices
			)
        {
            _context = context;
			_housesServices = housesServices;
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


		[HttpGet]
		public IActionResult Add()
		{
			HouseEditViewModel house = new HouseEditViewModel();

			return View("Edit", house);
		}


		[HttpPost]
		public async Task<IActionResult> Add(HouseEditViewModel vm)
		{
			var dto = new HouseDto()
			{
				Id = vm.Id,
				Address = vm.Address,
				City = vm.City,
				Floors = vm.Floors,
				Area = vm.Area,
				Price = vm.Price,
				CreatedAt = vm.CreatedAt,
				ModifiedAt = vm.ModifiedAt
			};

			var result = await _housesServices.Add(dto);

			if(result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index), vm);
		}

	}
}
