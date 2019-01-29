using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class PreEmpReqValidator: AbstractValidator<PreEmpReq>
    {
        public PreEmpReqValidator()
        {
            RuleFor(x => x.PreEmpReqCode)
                 .NotEmpty()
                 .NotNull()
                 .OverridePropertyName("Code")
                 .MaximumLength(10);
        }
    }
}
