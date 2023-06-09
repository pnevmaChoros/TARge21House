﻿using Microsoft.AspNetCore.Mvc;
using TARge21House.Core.Dto;
using TARge21House.Core.ServiceInterface;
using TARge21House.Data;
using TARge21House.Models.House;

namespace TARge21House.Controllers
{
	public class HousesController : Controller
	{
		private readonly TARge21HouseContext _context;
		private readonly IHousesServices _housesServices;

		public HousesController
			(
				TARge21HouseContext context,
				IHousesServices housesServices
			)
		{
			_context = context;
			_housesServices = housesServices;
		}


		[HttpGet]
		public async  Task<IActionResult> Index()
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
		public IActionResult Create()
		{
			HouseCreateUpdateViewModel house = new HouseCreateUpdateViewModel();

			return View("CreateUpdate", house);
		}


		[HttpPost]
		public async Task<IActionResult> Create(HouseCreateUpdateViewModel vm)
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

			var result = await _housesServices.Create(dto);

			if (result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index), vm);
		}


		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var house = await _housesServices.GetAsync(id);

			if (house == null)
			{
				return NotFound();
			}


			var vm = new HouseDetailsViewModel();

			vm.Id = id;

			vm.Address = house.Address;
			vm.City = house.City;
			
			vm.Floors = house.Floors;
			vm.Area = house.Area;
			vm.Price = house.Price;
			
			vm.CreatedAt = house.CreatedAt;
			vm.ModifiedAt = house.ModifiedAt;

			return View(vm);
		}


		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			var house = await _housesServices.GetAsync(id);

			if (house == null)
			{
				return NotFound();
			}

			var vm = new HouseDeleteViewModel();

			vm.Id = id;

			vm.Address = house.Address;
			vm.City = house.City;

			vm.Floors = house.Floors;
			vm.Area = house.Area;
			vm.Price = house.Price;

			vm.CreatedAt = house.CreatedAt;
			vm.ModifiedAt = house.ModifiedAt;

			return View(vm);
		}


		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var houseId = await _housesServices.Delete(id);

            if (houseId == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
        }


		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			var house = await _housesServices.GetAsync(id);

			if(house == null)
			{
				return NotFound();
			}

			var vm = new HouseCreateUpdateViewModel();

			vm.Id = house.Id;
			vm.Address = house.Address;
			vm.City = house.City;
			vm.Floors = house.Floors;
			vm.Area = house.Area;
			vm.Price = house.Price;
			vm.CreatedAt = house.CreatedAt;
			vm.ModifiedAt = house.ModifiedAt;

			return View("CreateUpdate" ,vm);
		}


		[HttpPost]
		public async Task<IActionResult> Update(HouseCreateUpdateViewModel vm)
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
				ModifiedAt = vm.ModifiedAt,
			};

			var result = await _housesServices.Update(dto);

			if(result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index), vm);
		}

	}
}