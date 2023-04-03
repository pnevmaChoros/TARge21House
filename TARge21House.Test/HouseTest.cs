using System.Diagnostics;
using System.Net;
using TARge21House.Core.Dto;
using TARge21House.Core.ServiceInterface;

namespace TARge21House.Test
{
	public class HouseTest : TestBase
	{

		//testing name convention
		//The name of the method being tested.
		//The scenario under which it's being tested.
		//The expected behavior when the scenario is invoked.


		[Fact]
		public async Task Create_WhenCreateHouse_ReturnsNotNull()
		{
			//arrange
			//expected value
			HouseDto house = new HouseDto();
			house.Id = Guid.NewGuid();
			house.Address = "1";
			house.City = "2";
			house.Floors = 3;
			house.Area = 4;
			house.Price = 5;
			house.CreatedAt = DateTime.Now;
			house.ModifiedAt = DateTime.Now;


			//act
			//an actual value
			var actual = await Svc<IHousesServices>().Create(house);


			//assert
			Assert.NotNull(actual);
		}


		[Fact]
		public async Task GetAsync_WhenCreateAndGetAsyncById_RetrurnsEqual()
		{
			//arrange
			//expected value
			Guid id = Guid.Parse("ab4532fd-b0d2-430a-9a79-c739c92c4c2a");

			HouseDto house = new()
			{
				Id = id,
				Address = "J A1",
				City = "city17",
				Floors = 1,
				Area = 2,
				Price = 3,
				CreatedAt = DateTime.Now,
				ModifiedAt = DateTime.Now,
			};


			//act
			var createHouse = await Svc<IHousesServices>().Create(house);
			var getHouse = await Svc<IHousesServices>().GetAsync((Guid)createHouse.Id);


			//assert
			Assert.Equal(getHouse.Id, createHouse.Id);
		}



		[Fact]
		public async Task GetAsync_WhenPassingWrongId_RetrurnsNull()
		{
			//arrange
			//expected value

			//none existing
			Guid wrongId = Guid.Parse("ab4532fd-b0d2-430a-9a79-c739c92c4c2a");

			//existing form db
			//Guid existingId = Guid.Parse("ef4532fd-b0d2-430a-9a79-c739c92c4c1c");


			//act
			var getHouse = await Svc<IHousesServices>().GetAsync(wrongId);


			//assert
			Assert.Null(getHouse);
		}


		[Fact]
		public async Task Delete_CreateAndAfterDeleteSameObj_ShouldNotEqual()
		{
			Guid id = Guid.Parse("ab4532fd-b0d2-430a-9a79-c739c92c4c2a");

			HouseDto house = new()
			{
				Id = id,
				Address = "J A1",
				City = "city17",
				Floors = 1,
				Area = 2,
				Price = 3,
				CreatedAt = DateTime.Now,
				ModifiedAt = DateTime.Now,
			};

			//creating in db
			var createHouse = await Svc<IHousesServices>().Create(house);
			// deleting in db
			var deleteHouse = await Svc<IHousesServices>().Delete((Guid)createHouse.Id);

			//same deleted obj should not be equal
			Assert.Equal(createHouse, deleteHouse);

		}


		[Fact]
		public async Task Update_UpdateHouse_AnyValueShouldNotEqual()
		{
			HouseDto house = MockHouseData();

			var updateHouse = new HouseDto()
			{
				Address = "aaa",
				City = "bbb",
				Floors = 111,
				Area = 222,
				Price = 333,
				CreatedAt = DateTime.Now,
				ModifiedAt = DateTime.Now,
			};

			var result = await Svc<IHousesServices>().Update(updateHouse);


			Assert.DoesNotMatch(house.Address, updateHouse.Address);
			Assert.NotEqual(house.Price, updateHouse.Price);

		}


		private HouseDto MockHouseData()
		{
			HouseDto house = new()
			{
				Id = Guid.NewGuid(),

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
