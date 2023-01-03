using NorthWind.Entities.Enums;

namespace NorthWind.Entities.POCOEntities
{
	public class Order
	{
		public int Id { get; set; }
		public string CustomerId { get; set; } = string.Empty;
		public DateTime OrderDate { get; set; }
		public string ShipAddress { get; set; } = string.Empty;
		public string ShipCity { get; set; } = string.Empty;
		public string ShipCountry { get; set; } = string.Empty;
		public string ShipPostalCode { get; set; } = string.Empty;

		public DiscountType DiscountType { get; set; }
		public double DiscountAmount { get; set; }
		public ShippingType ShippingType { get; set; }
	}
}
