namespace TARge21House.Models.House
{
	public class HouseIndexViewModel
	{
		public Guid? Id { get; set; }

		public string Address { get; set; }
		public string City { get; set; }

		public int Floors { get; set; }
		public int Area { get; set; }
		public int Price { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
	}
}
