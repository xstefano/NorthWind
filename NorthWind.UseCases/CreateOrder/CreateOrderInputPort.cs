using NorthWind.UseCasesDTOs.CreateOrder;
using MediatR;

namespace NorthWind.UseCases.CreateOrder
{
	public class CreateOrderInputPort : CreateOrderParams, IRequest<int>
	{

	}
}
