using FluentValidation;
using Lunchorder.Domain.Dtos.Requests;

namespace Lunchorder.Domain.Validators
{
        public class PutBalanceRequestValidator : AbstractValidator<PutBalanceRequest>
        {
            public PutBalanceRequestValidator()
            {
                RuleFor(req => req.Amount).GreaterThan(0);
            }
        }
}
