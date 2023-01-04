using Microsoft.AspNetCore.Mvc;
using NorthWind.Presenters;
using NorthWind.UseCasesDTOs.CreateOrder;
using NorthWind.UseCasesPorts.CreateOrder;

namespace NorthWind.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController
	{
		private readonly ICreateOrderInputPort InputPort;
		private readonly ICreateOrderOutputPort OutputPort;

		public OrderController(ICreateOrderInputPort inputPort, ICreateOrderOutputPort outputPort)
		{
			InputPort = inputPort;
			OutputPort = outputPort;
		}

		[HttpPost("create-order")]
		public async Task<ActionResult<string>> CreateOrder(
			CreateOrderParams orderparams)
		{
			await InputPort.Handle(orderparams);
			var Presenter = OutputPort as CreateOrderPresenter;
			return Presenter.Content;
		}
	}
}
