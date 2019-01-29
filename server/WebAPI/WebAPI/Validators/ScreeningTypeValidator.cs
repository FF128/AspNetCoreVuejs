using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class ScreeningTypeValidator : AbstractValidator<ScreenType>
    {
        public ScreeningTypeValidator()
        {
            RuleFor(x => x.ScreenTypeCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
