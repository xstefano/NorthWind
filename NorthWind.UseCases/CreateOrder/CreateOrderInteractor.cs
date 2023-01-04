using MediatR;
using NorthWind.Entities.Exceptions;
using NorthWind.Entities.Interfaces;
using NorthWind.Entities.POCOEntities;

namespace NorthWind.UseCases.CreateOrder
{
	public class CreateOrderInteractor : AsyncRequestHandler<CreateOrderInputPort>
	{
		private readonly IOrderRepository OrderRepository;
		private readonly IOrderDetailRepository OrderDetailRepository;
		private readonly IUnitOfWork UnitOfWork;

		public CreateOrderInteractor(IOrderRepository orderRepository,
			IOrderDetailRepository orderDetailRepository,
			IUnitOfWork unitOfWork) =>
			(OrderRepository, OrderDetailRepository, UnitOfWork) =
			(orderRepository, orderDetailRepository, unitOfWork);

		protected async override Task Handle(CreateOrderInputPort request, 
			CancellationToken cancellationToken)
		{
			Order Order = new Order
			{
				CustomerId = request.RequestData.CustomerId,
				OrderDate = DateTime.Now,
				ShipAddress = request.RequestData.ShipAddress,
				ShipCity = request.RequestData.ShipCity,
				ShipCountry = request.RequestData.ShipCountry,
				ShipPostalCode = request.RequestData.ShipPostalCode,
				ShippingType = Entities.Enums.ShippingType.Road,
				DiscountType = Entities.Enums.DiscountType.Percentage,
				DiscountAmount = 10
			};

			OrderRepository.Create(Order);

			foreach(var Item in request.RequestData.OrderDetails)
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
			request.OutputPort.Handle(Order.Id);
		}
	}
}
