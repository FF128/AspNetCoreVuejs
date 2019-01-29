﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class LevelsOfEmployeeValidator : AbstractValidator<LevelsOfEmployee>
    {
        public LevelsOfEmployeeValidator()
        {
            RuleFor(x => x.LOECode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
