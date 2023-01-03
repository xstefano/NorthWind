using NorthWind.Entities.POCOEntities;
using NorthWind.Entities.Specifications;

namespace NorthWind.Entities.Interfaces
{
	public interface IOrderRepository
	{
		void Create(Order order);
		IEnumerable<Order> GetOrdersBySpecification(Specification<Order> specification);

	}
}
