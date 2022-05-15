using NPointeuse.Infra.Client;

namespace NPointeuse.XF.Views
{
    public class MandatoryStringAttribute : ValidationAttribute
    {
        public override ValidationResult IsValid(object source, object value)
        {
            if (string.IsNullOrEmpty(value as string))
            {
                return ValidationResult.Fail("Can not be null.");
            }

            return ValidationResult.Success;
        }
    }
}
