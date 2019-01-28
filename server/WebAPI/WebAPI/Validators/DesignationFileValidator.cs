using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class DesignationFileValidator : AbstractValidator<DesignationFile>
    {
        public DesignationFileValidator()
        {
            RuleFor(x => x.DesignationFileCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
