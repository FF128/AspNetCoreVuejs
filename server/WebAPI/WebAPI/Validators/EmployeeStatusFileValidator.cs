﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class EmployeeStatusFileValidator : AbstractValidator<EmployeeStatusFile>
    {
        public EmployeeStatusFileValidator()
        {
            RuleFor(x => x.Code)
                  .NotEmpty()
                  .NotNull()
                  .OverridePropertyName("Code")
                  .MaximumLength(10);
        }
    }
}
