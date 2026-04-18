using FluentValidation;
using PaymentBack.Domain.Models;

namespace PaymentBack.Validator
{
    public class PaymentModelValidator: AbstractValidator<PaymentModel>
    {
          public PaymentModelValidator() {

            RuleFor(x => x.WalletNumber)
            .NotEmpty().WithMessage("Номер кошелька обязателен")
            .Length(8, 20).WithMessage("Длина номера кошелька от 8 до 20 символов");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email обязателен")
                .EmailAddress().WithMessage("Некорректный формат Email");

            RuleFor(x => x.Phone)
                .Length(12)
                .Matches(@"/^\+7\d{3}\d{3}\d{2}\d{2}$/").WithMessage("Некорректный формат номера телефона")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Сумма должна быть больше нуля");

            RuleFor(x => x.CreatedAt)
                .NotEmpty()
                .Must(predicate: date => date <= DateTime.UtcNow && date >= DateTime.UtcNow.AddMinutes(-1))
                .WithMessage("Время обработки запроса первысило 1 минут. Повторите запрос пожалуйста");

        }
    }
}
