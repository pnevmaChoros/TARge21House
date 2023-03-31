﻿using TARge21House.Core.Domain.House;
using TARge21House.Core.Dto;

namespace TARge21House.Core.ServiceInterface
{
	public interface IHousesServices
	{
		Task<House> Add(HouseDto dto);
	}
}
