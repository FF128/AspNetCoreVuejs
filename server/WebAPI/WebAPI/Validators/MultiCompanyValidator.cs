using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class MultiCompanyValidator : AbstractValidator<MultiCompanyDatabase>
    {
        public MultiCompanyValidator()
        {
            RuleFor(x => x.DatabaseCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
