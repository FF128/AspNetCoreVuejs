using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class DesignationDutiesValidator : AbstractValidator<DesignationDuties>
    {
        public DesignationDutiesValidator()
        {
            RuleFor(x => x.DesignationCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
