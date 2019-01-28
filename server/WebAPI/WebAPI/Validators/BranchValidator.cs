using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class BranchValidator : AbstractValidator<Branch>
    {
        public BranchValidator()
        {
            RuleFor(x => x.BranchCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
