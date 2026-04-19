using FluentValidation;

namespace PaymentBack.Application.Validator
{
    public class PaymentModelValidator: AbstractValidator<PaymentModel>
    {
          public PaymentModelValidator() {

            RuleFor(x => x.WalletNumber)
            .NotEmpty().WithMessage("Номер кошелька обязателен")
            .Length(8, 20).WithMessage("Длина номера кошелька от 8 до 20 символов");

            RuleFor(x => x.Account)
                .GreaterThan(0)
                .WithMessage("Номер аккаунта обязателен");
            
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email обязателен")
                .EmailAddress().WithMessage("Некорректный формат Email");

            RuleFor(x => x.Phone)
                .Length(12)
                .Matches(@"^\+7\d{10}$")
                .WithMessage("Некорректный формат номера телефона")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Сумма должна быть больше нуля");

        }
    }
}
