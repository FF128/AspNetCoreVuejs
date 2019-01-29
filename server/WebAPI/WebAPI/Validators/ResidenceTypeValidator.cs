using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class ResidenceTypeValidator : AbstractValidator<ResidenceType>
    {
        public ResidenceTypeValidator()
        {
            RuleFor(x => x.ResidenceTypeCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
