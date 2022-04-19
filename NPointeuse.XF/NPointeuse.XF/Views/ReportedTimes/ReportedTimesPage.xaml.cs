using NPointeuse.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NPointeuse.XF.Views.ReportedTimes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportedTimesPage : ContentPage
    {
        public ReportedTimesPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            this.ViewModel.EditCommand.Execute((DateRange)e.Item);
        }

        private ReportedTimesViewModel ViewModel => (ReportedTimesViewModel)this.BindingContext;
    }
}