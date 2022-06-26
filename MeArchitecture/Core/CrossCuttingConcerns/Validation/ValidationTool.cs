using Core.Entities.Abstract;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool<T> where T : class, IEntity, new()
    {
        public static void Validate(IValidator<T> validator, T entity)
        {
            var result = validator.Validate(entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
