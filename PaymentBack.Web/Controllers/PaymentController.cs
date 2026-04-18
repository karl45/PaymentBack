using Microsoft.AspNetCore.Mvc;
using PaymentBack.Application.DTO.CreatePaymentDto;
using PaymentBack.Application.DTO.GetPaymentsDto;
using PaymentBack.Application.DTO.GetPayStatsByDayDto;
using PaymentBack.Application.Services;

namespace PaymentBack.Controllers
{
    [ApiController]
    [Route("payments")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IResult> CreatePayment([FromBody] CreatePaymentDtoRequest request)
        {
            var payment = await _paymentService.CreatePaymentAsync(request, HttpContext.RequestAborted);
            return Results.Created("payments", payment);
        }

        [HttpGet]
        public async Task<IResult> GetPayments([FromQuery] GetPaymentsDtoParams @params)
        {
            var response = await _paymentService.GetAllPayments(@params, HttpContext.RequestAborted);
            return response.Payments.Count > 0 ? Results.Ok(response) : Results.NoContent();
        }

        [HttpGet("stats")]
        public async Task<IResult> GetStats()
        {
            var response = await _paymentService.GetStatsAsync(HttpContext.RequestAborted);
            return response.PaymentCommonStats.PaymentCount == 0 ? Results.NoContent() : Results.Ok(response);
        }

        [HttpGet("day-stats")]
        public async Task<IResult> GetByDayStats([FromQuery] GetPayStatsByDayParams @params)
        {
            var dayStats = await _paymentService.GetPayStatsByDay(@params, HttpContext.RequestAborted);

            if (dayStats.DayStats.Count == 0)
                return Results.NoContent();
            else
                return Results.Ok(dayStats);
        }
    }

}
