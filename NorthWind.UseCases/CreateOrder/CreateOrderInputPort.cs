using NorthWind.UseCasesDTOs.CreateOrder;
using MediatR;
using NorthWind.UseCases.Common.Ports;

namespace NorthWind.UseCases.CreateOrder
{
	public class CreateOrderInputPort : IInputPort<CreateOrderParams, int>
	{
		public CreateOrderParams RequestData { get; }

		public IOutputPort<int> OutputPort { get; }

		public CreateOrderInputPort(CreateOrderParams requestData, IOutputPort<int> outputPort)
		{
			RequestData = requestData;
			OutputPort = outputPort;
		}
	}
}
