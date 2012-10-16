using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Web.Mvc;

namespace Sandbox.MVC.Lib.Validators
{
    public class SqlServerDateAttribute : ValidationAttribute, IClientValidatable
    {
        private static readonly DateTime maxValue = SqlDateTime.MaxValue.Value;
        private static readonly DateTime minValue = SqlDateTime.MinValue.Value;

        public override string FormatErrorMessage(string name)
        {
            return string.Format("The {0} field is not a valid date.", name);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return null;

            var dateTime = Convert.ToDateTime(value);

            if (dateTime < minValue)
                return new ValidationResult(ErrorMessage);

            if (dateTime > maxValue)
                return new ValidationResult(ErrorMessage);

            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.DisplayName),
                ValidationType = "sqlserverdate"
            };

            rule.ValidationParameters.Add("min", minValue);
            rule.ValidationParameters.Add("max", maxValue);

            yield return rule;
        }
    }
}