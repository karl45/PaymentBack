using FluentValidation;
using PaymentBack.Application.DTO.GetPayStatsByDayDto;

namespace PaymentBack.Application.Validator
{
    public class GetDayStatsByDayParamsDtoValidator: AbstractValidator<GetPayStatsByDayParams>
    {
        public GetDayStatsByDayParamsDtoValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Размер страницы должен быть больше нуля")
                .LessThanOrEqualTo(100).WithMessage("Размер страницы не должен превышать 100");
        }

    }
}
