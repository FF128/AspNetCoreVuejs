using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class OverallRatingValidator : AbstractValidator<OverallRatings>
    {
        public OverallRatingValidator()
        {
            RuleFor(x => x.OverallRatingsCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
