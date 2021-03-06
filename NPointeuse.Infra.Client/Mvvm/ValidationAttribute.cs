using System;

namespace NPointeuse.Infra.Client
{
    public abstract class ValidationAttribute : Attribute
    {
        public abstract ValidationResult IsValid(object source, object value);

        public bool QuitOnError { get; protected set; } = true;
    }
}
