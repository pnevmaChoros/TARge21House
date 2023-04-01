using TARge21House.Core.Domain.House;
using TARge21House.Core.Dto;

namespace TARge21House.Core.ServiceInterface
{
	public interface IHousesServices
	{
		Task<House> Create(HouseDto dto);

		Task<House> GetAsync(Guid id);

		Task<House> Delete(Guid id);

		Task<House> Update(HouseDto dto);
	}
}
