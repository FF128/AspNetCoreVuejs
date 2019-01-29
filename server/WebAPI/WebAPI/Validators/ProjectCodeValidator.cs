using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class ProjectCodeValidator : AbstractValidator<ProjectCodeModel>
    {
        public ProjectCodeValidator()
        {
            RuleFor(x => x.ProjectCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
