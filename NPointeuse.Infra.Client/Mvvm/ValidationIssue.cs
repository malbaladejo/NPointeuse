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
}
