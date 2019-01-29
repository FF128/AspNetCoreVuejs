using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class DutiesAndResponsibilitiesValidator : AbstractValidator<DutiesAndResponsibilities>
    {
        public DutiesAndResponsibilitiesValidator()
        {
            RuleFor(x => x.DutiesResponsibilitiesCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
