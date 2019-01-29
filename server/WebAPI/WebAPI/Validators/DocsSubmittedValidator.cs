using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class DocsSubmittedValidator : AbstractValidator<DocSubmitted>
    {
        public DocsSubmittedValidator()
        {
            RuleFor(x => x.DocCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
