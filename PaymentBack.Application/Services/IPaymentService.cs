using PaymentBack.Application.DTO.CreatePaymentDto;
using PaymentBack.Application.DTO.GetPaymentsDto;
using PaymentBack.Application.DTO.GetPayStatsByDay;
using PaymentBack.Domain.Models;

namespace PaymentBack.Application.Services
{
    public interface IPaymentService
    {
       Task<CreatePaymentDtoResponse> CreatePaymentAsync(CreatePaymentDtoRequest request, CancellationToken token);

       Task<GetPaymentsDtoResponse> GetAllPayments(GetPaymentsDtoParams @params, CancellationToken token);

       Task<PaymentCommonStatsModel> GetStatsAsync(CancellationToken token);

       Task<GetPayStatsByDayResponse> GetPayStatsByDay(GetPayStatsByDayParams @params, CancellationToken token);

    }
}
