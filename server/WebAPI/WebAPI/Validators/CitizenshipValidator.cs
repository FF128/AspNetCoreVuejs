﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class CitizenshipValidator : AbstractValidator<Citizenship>
    {
        public CitizenshipValidator()
        {
            RuleFor(x => x.CitiCode)
                .NotEmpty()
                .NotNull()
                .OverridePropertyName("Code")
                .MaximumLength(10);
                
        }
    }
}
