using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Presentation.Pages;

namespace HousingCoo.Presentation {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenuPage : MasterDetailPage {
        public MasterMenuPage() {
            InitializeComponent();
           // MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            //var item = e.SelectedItem as NavPageMenuItem;
            //if (item == null) {
            //    return;
            //}

            //var page = (Page)Activator.CreateInstance(item.TargetType);
            //page.Title = item.Title;

            ////Detail = new NavigationPage(page);
            //IsPresented = false;

            //MasterPage.ListView.SelectedItem = null;
            //Detail.Navigation.PushAsync(page);
        }
    }
}