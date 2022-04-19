using System.Collections.Generic;
using System.ComponentModel;

namespace NPointeuse.Infra.Client
{
    public interface IValidationAware : INotifyPropertyChanged
    {
        IReadOnlyCollection<ValidationIssue> Errors { get; }

        bool HasErrors { get; }
    }
}
