using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NPointeuse.Infra.Client
{
    public class ValidationIssue
    {
        public ValidationIssue(string message, ValidationSeverity severity) : this(null, message, severity)
        {
        }

        public ValidationIssue(string propertyName, string message, ValidationSeverity severity)
        {
            PropertyName = propertyName;
            Message = message;
            Severity = severity;
        }

        public string PropertyName { get; }
        public string Message { get; }
        public ValidationSeverity Severity { get; }
    }

    public enum ValidationSeverity
    {
        Error
    }

    public abstract class ValidatableBase : IValidationAware
    {
        private readonly List<ValidationIssue> errors = new List<ValidationIssue>();

        public virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            try
            {
                if (field?.Equals(value) ?? false)
                    return false;

                this.ApplyValidations(value, propertyName);
                this.SetField(ref field, value, propertyName);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void SetField<T>(ref T field, T value, string propertyName)
        {
            field = value;
            this.RaisePropertyChanged(propertyName);
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;

        private void ApplyValidations<T>(T value, string propertyName)
        {
            var attributes = this.GetValidationAttributes(propertyName).ToArray();
            if (attributes.Length == 0)
                return;

            ApplyValidations(value, propertyName, attributes);
        }

        private void ApplyValidations<T>(T value, string propertyName, ValidationAttribute[] attributes)
        {
            foreach (var attribute in attributes)
            {
                var result = attribute.IsValid(this, value);
                if (!result.IsValid)
                    this.AddValidation(result.ErrorMessage, ValidationSeverity.Error, propertyName);
            }

            this.RaiseErrorsChanged();
        }

        public void AddValidation(
            string errorMessage,
            ValidationSeverity severity,
            [CallerMemberName] string propertyName = null)
        {
            this.errors.Add(new ValidationIssue(propertyName, errorMessage, severity));
            this.RaiseErrorsChanged();
        }

        public void ClearValidations([CallerMemberName] string propertyName = null)
        {
            this.errors.RemoveAll(i => i.PropertyName == propertyName);
            this.RaiseErrorsChanged();
        }

        public void ClearAllValidations()
        {
            this.errors.Clear();
            this.RaiseErrorsChanged();
        }

        private IEnumerable<ValidationAttribute> GetValidationAttributes(string propertyName)
        {
            var property = this.GetType().GetProperty(propertyName);
            if (property == null)
                return new ValidationAttribute[0];

            return property.GetCustomAttributes<ValidationAttribute>();
        }

        public void RaiseErrorsChanged()
        {
            this.RaisePropertyChanged(nameof(this.Errors));
        }

        public IReadOnlyCollection<ValidationIssue> Errors => this.errors.ToArray();

        public bool HasErrors => this.errors.Count > 0;
    }
}
