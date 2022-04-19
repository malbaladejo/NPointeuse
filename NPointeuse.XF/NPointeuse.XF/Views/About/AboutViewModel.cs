using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NPointeuse.XF.ViewModels
{
    internal class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            this.Version = this.GetType().Assembly.GetName().Version.ToString();
        }

        public string Version { get; }
    }
}