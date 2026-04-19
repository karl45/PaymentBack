using FluentValidation;
using PaymentBack.Application.DTO.CreatePaymentDto;

namespace PaymentBack.Application.Validator
{
    public class CreatePaymentDtoRequestValidator: AbstractValidator<CreatePaymentDtoRequest>
    {
        public CreatePaymentDtoRequestValidator()
        {
            RuleFor(x => x.Payment)
            .NotNull()
            .SetValidator(new PaymentModelValidator());
        }
    }
}
