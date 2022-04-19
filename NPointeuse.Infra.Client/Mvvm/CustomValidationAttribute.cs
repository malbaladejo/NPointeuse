using System;
using System.Reflection;

namespace NPointeuse.Infra.Client
{
    public class CustomValidationAttribute : ValidationAttribute
    {
        private readonly string isValid;

        public CustomValidationAttribute(string isValidMethodName)
        {
            this.isValid = isValidMethodName;
        }

        public override ValidationResult IsValid(object source, object value)
        {
            var method = this.GetMethod(source);

            return InvokeMethod(source, value, method);
        }

        private MethodInfo GetMethod(object source)
        {
            var method = source.GetType().GetMethod(this.isValid, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (method == null)
            {
                throw new InvalidOperationException($"{source.GetType()} does not contains method {this.isValid}.");
            }

            return method;
        }

        private ValidationResult InvokeMethod(object source, object value, MethodInfo method)
        {
            try
            {
                var result = method.Invoke(source, new[] { value });
                return (ValidationResult)result;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error during execution of method {this.isValid} on {source.GetType()}.", e);
            }
        }
    }

}
