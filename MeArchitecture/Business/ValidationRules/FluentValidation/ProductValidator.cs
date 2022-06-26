﻿using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(c => c.ProductName)
                .NotEmpty().WithMessage("The area is not empty")
                .MinimumLength(2).WithMessage("Please minimum {MinLength} length. Preview : {TotalLength}");
        }
    }
}
