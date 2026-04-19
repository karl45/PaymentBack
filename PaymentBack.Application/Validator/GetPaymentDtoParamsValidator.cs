using FluentValidation;
using PaymentBack.Application.DTO.GetPaymentsDto;

namespace PaymentBack.Application.Validator
{
    public class GetPaymentDtoParamsValidator : AbstractValidator<GetPaymentsDtoParams>
    {
        public GetPaymentDtoParamsValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Размер страницы должен быть больше нуля")
                .LessThanOrEqualTo(100).WithMessage("Размер страницы не должен превышать 100");

            RuleFor(x => x.PrevId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Предыдущий идентификатор должен быть положительным или ноль");

            RuleFor(x => x.LastId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Последующий идентификатор должен быть положительным или ноль");
        }
    }
}
