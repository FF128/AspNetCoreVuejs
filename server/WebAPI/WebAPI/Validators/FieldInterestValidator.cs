using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class FieldInterestValidator : AbstractValidator<FieldOfInterest>
    {
        public FieldInterestValidator()
        {
            RuleFor(x => x.InterestCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
