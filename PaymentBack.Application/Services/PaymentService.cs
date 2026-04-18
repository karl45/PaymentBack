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

        public Task<CreatePaymentDtoResponse> CreatePaymentAsync(CreatePaymentDtoRequest request, CancellationToken token)
        {
            var paymentEntity = _mapper.Map<PaymentEntity>(request.Payment);
            var resultPayment = _paymentRepository.CreatePaymentAsync(paymentEntity, token).ContinueWith(t => _mapper.Map<PaymentModel>(t.Result), token);
            return resultPayment.ContinueWith(t => new CreatePaymentDtoResponse { Payment = _mapper.Map<PaymentModel>(t.Result) }, token);
        }

        public Task<GetPaymentsDtoResponse> GetAllPayments(GetPaymentsDtoParams @params, CancellationToken token)
        {
            var payments = _paymentRepository.GetAllPayments(@params.PageSize, token, @params.PrevId, @params.LastId).ContinueWith(t => _mapper.Map<List<PaymentModel>>(t.Result), token);
            return payments.ContinueWith(t => new GetPaymentsDtoResponse { Payments = t.Result }, token);
        }

        public Task<GetStatsDtoResponse> GetStatsAsync(CancellationToken token)
        {
            return _paymentRepository.GetStatsAsync(token).ContinueWith(t => new GetStatsDtoResponse { PaymentCommonStats = _mapper.Map<PaymentCommonStatsModel>(t.Result) }, token);
        }

        public Task<GetPayStatsByDayResponse> GetPayStatsByDay(GetPayStatsByDayParams @params, CancellationToken token)
        {
            return _paymentRepository.GetPayStatsByDay(@params.PageSize, token, @params.PrevDate, @params.LastDate).ContinueWith(t => new GetPayStatsByDayResponse { DayStats = _mapper.Map<List<PaymentGroupedByDayStatsModel>>(t.Result) }, token);
        }

    }
}
