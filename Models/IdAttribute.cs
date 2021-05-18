using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Models
{
    public class IdAttribute : ValidationAttribute
    {
        private readonly Type _entityType;

        public IdAttribute(Type entityType)
        {
            _entityType = entityType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var checker = validationContext.GetRequiredService<Func<Type, object, Task<bool>>>();

            return checker(_entityType, value).Result
                ? ValidationResult.Success
                : new ValidationResult($"{_entityType.Name} with Id: {value} is not found");
        }
    }
}