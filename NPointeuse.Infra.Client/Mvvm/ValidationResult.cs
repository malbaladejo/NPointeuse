namespace NPointeuse.Infra.Client
{
    public class ValidationResult
    {
        private ValidationResult(bool isValid, string errorMessage = null)
        {
            this.IsValid = isValid;
            this.ErrorMessage = errorMessage;
        }

        public static ValidationResult Success => new ValidationResult(true);
        public static ValidationResult Fail(string errorMessage) => new ValidationResult(false, errorMessage);

        public bool IsValid { get; }
        public string ErrorMessage { get; }
    }
}
