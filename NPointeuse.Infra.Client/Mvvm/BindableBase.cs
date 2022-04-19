using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NPointeuse.Infra.Client
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (field?.Equals(value) ?? false) 
                return false;

            field = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
