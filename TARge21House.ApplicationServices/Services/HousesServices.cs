using Microsoft.EntityFrameworkCore;
using TARge21House.Core.Domain.House;
using TARge21House.Core.Dto;
using TARge21House.Core.ServiceInterface;
using TARge21House.Data;

namespace TARge21House.ApplicationServices.Services
{
	public class HousesServices : IHousesServices
	{
		private readonly TARge21HouseContext _context;


        public HousesServices(TARge21HouseContext context)
        {
            _context = context;
        }


        public async Task<House> Create(HouseDto dto)
        {
            var domain = new House()
            {
                Id = Guid.NewGuid(),
                Address = dto.Address,
                City = dto.City,
                Floors = dto.Floors,
                Area = dto.Area,
                Price = dto.Price,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            await _context.Houses.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }


        public async Task<House> GetAsync(Guid id)
        {
            var result = await _context.Houses
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }
}
