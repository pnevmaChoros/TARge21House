using TARge21House.Core.Dto;
using TARge21House.Core.ServiceInterface;

namespace TARge21House.Test
{
	public class HouseTest : TestBase
	{


		[Fact]
		public async Task Should_DelteByIdHouse_WhenDeleteHouse()
		{
			Guid id = Guid.NewGuid();

			HouseDto house = MockHouseData();
			var addHouse = await Svc<IHousesServices>().Create(house);

			var result = await Svc<IHousesServices>().Delete((Guid)addHouse.Id);

			Assert.Equal(result.Id, addHouse.Id);

		}




		private HouseDto MockHouseData()
		{
			HouseDto house = new()
			{
				Address = "J A1",
				City = "city17",

				Floors = 1,
				Area = 2,
				Price = 3,

				CreatedAt = DateTime.Now,
				ModifiedAt = DateTime.Now,
			};

			return house;
		}

	}


}
