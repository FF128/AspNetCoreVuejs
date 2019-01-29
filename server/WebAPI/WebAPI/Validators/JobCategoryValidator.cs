using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class JobCategoryValidator : AbstractValidator<JobCategory>
    {
        public JobCategoryValidator()
        {
            RuleFor(x => x.JobCatCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
