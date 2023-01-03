using MediatR;
using NorthWind.Entities.Exceptions;
using NorthWind.Entities.Interfaces;
using NorthWind.Entities.POCOEntities;

namespace NorthWind.UseCases.CreateOrder
{
	public class CreateOrderInteractor : IRequestHandler<CreateOrderInputPort, int>
	{
		private readonly IOrderRepository OrderRepository;
		private readonly IOrderDetailRepository OrderDetailRepository;
		private readonly IUnitOfWork UnitOfWork;

		public CreateOrderInteractor(IOrderRepository orderRepository,
			IOrderDetailRepository orderDetailRepository,
			IUnitOfWork unitOfWork) =>
			(OrderRepository, OrderDetailRepository, UnitOfWork) =
			(orderRepository, orderDetailRepository, unitOfWork);

		public async Task<int> Handle(CreateOrderInputPort request, 
			CancellationToken cancellationToken)
		{
			Order Order = new Order
			{
				CustomerId = request.CustomerId,
				OrderDate = DateTime.Now,
				ShipAddress = request.ShipAddress,
				ShipCity = request.ShipCity,
				ShipCountry = request.ShipCountry,
				ShipPostalCode = request.ShipPostalCode,
				ShippingType = Entities.Enums.ShippingType.Road,
				DiscountType = Entities.Enums.DiscountType.Percentage,
				DiscountAmount = 10
			};

			OrderRepository.Create(Order);

			foreach(var Item in request.OrderDetails)
			{
				OrderDetailRepository.Create(
					new OrderDetail
					{
						Order = Order,
						ProductId = Item.ProductId,
						UnitPrice = Item.UnitPrice,
						Quantity = Item.Quantity
					});
			}

			try
			{
				await UnitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new GeneralException("Error when creating the order.",
					ex.Message);
			}
			return Order.Id;
		}
	}
}
