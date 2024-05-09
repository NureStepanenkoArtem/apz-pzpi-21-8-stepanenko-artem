using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureAndObserve.Core.Domain.Entities;
using SecureAndObserve.Core.ServiceContracts;
using SecureAndObserve.Infrastructure.DbContext;

namespace SecureAndObserve.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private IOrdersService _ordersService;

        public OrdersApiController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rank>>> OrdersExcel()
        {
            MemoryStream memoryStream = await _ordersService.GetOrdersExcel();
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "orders.xlsx");
        }
    }
}
