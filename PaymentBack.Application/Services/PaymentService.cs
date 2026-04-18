using AutoMapper;
using PaymentBack.Application.DTO.CreatePaymentDto;
using PaymentBack.Application.DTO.GetPaymentsDto;
using PaymentBack.Application.DTO.GetPayStatsByDayDto;
using PaymentBack.Application.DTO.GetStatsDto;
using PaymentBack.Domain.Entities;
using PaymentBack.Domain.Models;
using PaymentBack.Infrastructure.Repositories;

namespace PaymentBack.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<CreatePaymentDtoResponse> CreatePaymentAsync(CreatePaymentDtoRequest request, CancellationToken token)
        {
            var paymentEntity = _mapper.Map<PaymentEntity>(request.Payment);
            var resultPayment = await _paymentRepository.CreatePaymentAsync(paymentEntity, token);
            return new CreatePaymentDtoResponse { Payment = _mapper.Map<PaymentModel>(resultPayment) };
        }

        public async Task<GetPaymentsDtoResponse> GetAllPayments(GetPaymentsDtoParams @params, CancellationToken token)
        {
            var payments = await _paymentRepository.GetAllPayments(@params.PageSize, token, @params.PrevId, @params.LastId);
            return new GetPaymentsDtoResponse { Payments = _mapper.Map<List<PaymentModel>>(payments) };
        }

        public async Task<GetStatsDtoResponse> GetStatsAsync(CancellationToken token)
        {
            var stats = await _paymentRepository.GetStatsAsync(token);
            return new GetStatsDtoResponse { PaymentCommonStats = _mapper.Map<PaymentCommonStatsModel>(stats) };
        }

        public async Task<GetPayStatsByDayResponse> GetPayStatsByDay(GetPayStatsByDayParams @params, CancellationToken token)
        {
            var dayStats = await _paymentRepository.GetPayStatsByDay(@params.PageSize, token, @params.PrevDate, @params.LastDate);
            return new GetPayStatsByDayResponse { DayStats = _mapper.Map<List<PaymentGroupedByDayStatsModel>>(dayStats) };
        }

    }
}
