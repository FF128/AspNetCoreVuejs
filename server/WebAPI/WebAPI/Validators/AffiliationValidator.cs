using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class AffiliationValidator : AbstractValidator<Affiliations>
    {
        public AffiliationValidator()
        {
            RuleFor(x => x.AffiliationsCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
