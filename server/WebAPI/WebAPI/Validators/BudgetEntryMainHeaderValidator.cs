using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.BudgetEntryModel;

namespace WebAPI.Validators
{
    public class BudgetEntryMainHeaderValidator : AbstractValidator<BudgetEntryMainHeader>
    {
        public BudgetEntryMainHeaderValidator()
        {
            RuleFor(x => x.DateRequired)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.DurationFrom)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.DurationTo)
                .NotEmpty()
                .NotNull();
        }
    }
}
