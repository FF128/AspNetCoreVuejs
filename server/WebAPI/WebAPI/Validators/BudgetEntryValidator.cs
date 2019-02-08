using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;

namespace WebAPI.Validators
{
    public class BudgetEntryValidator : AbstractValidator<BudgetEntryDto>
    {
        public BudgetEntryValidator()
        {
            RuleFor(x => x.DateRequired)
                .NotEmpty()
                .NotNull()
                .OverridePropertyName("Date Required");

            RuleFor(x => x.DurationFrom)
                .NotEmpty()
                .NotNull()
                .OverridePropertyName("Duration From");

            RuleFor(x => x.DurationTo)
                .NotEmpty()
                .NotNull()
                .OverridePropertyName("Duration To");
        }
    }
}
