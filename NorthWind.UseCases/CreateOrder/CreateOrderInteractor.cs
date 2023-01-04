using FluentValidation;
using NorthWind.Entities.Exceptions;
using NorthWind.Entities.Interfaces;
using NorthWind.Entities.POCOEntities;
using NorthWind.UseCases.Common.Validators;
using NorthWind.UseCasesDTOs.CreateOrder;
using NorthWind.UseCasesPorts.CreateOrder;

namespace NorthWind.UseCases.CreateOrder
{
	public class CreateOrderInteractor : ICreateOrderInputPort
	{
		private readonly IOrderRepository OrderRepository;
		private readonly IOrderDetailRepository OrderDetailRepository;
		private readonly IUnitOfWork UnitOfWork;
		private readonly ICreateOrderOutputPort OutputPort;
		private readonly IEnumerable<IValidator<CreateOrderParams>> Validators;

		public CreateOrderInteractor(IOrderRepository orderRepository,
			IOrderDetailRepository orderDetailRepository,
			IUnitOfWork unitOfWork,
			ICreateOrderOutputPort outputPort,
			IEnumerable<IValidator<CreateOrderParams>> validators) =>
			(OrderRepository, OrderDetailRepository, UnitOfWork, OutputPort, Validators) =
			(orderRepository, orderDetailRepository, unitOfWork, outputPort, validators);

		public async Task Handle(CreateOrderParams order)
		{
			await Validator<CreateOrderParams>.Validate(order, Validators);


			Order Order = new Order
			{
				CustomerId = order.CustomerId,
				OrderDate = DateTime.Now,
				ShipAddress = order.ShipAddress,
				ShipCity = order.ShipCity,
				ShipCountry = order.ShipCountry,
				ShipPostalCode = order.ShipPostalCode,
				ShippingType = Entities.Enums.ShippingType.Road,
				DiscountType = Entities.Enums.DiscountType.Percentage,
				DiscountAmount = 10
			};

			OrderRepository.Create(Order);

			foreach (var Item in order.OrderDetails)
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
			await OutputPort.Handle(Order.Id);
		}
	}
}
