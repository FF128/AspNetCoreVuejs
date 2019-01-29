﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class ScreenDetailValidator : AbstractValidator<ScreenDetails>
    {
        public ScreenDetailValidator()
        {
            RuleFor(x => x.ScreenDetailsCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}