using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NPointeuse.XF.Views.SpecficExpected
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpecficExpectedTimesPage : ContentPage
    {
        public SpecficExpectedTimesPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           ((ISpecficExpectedTimeViewModel)e.Item).EditCommand.Execute(null);
        }
    }
}