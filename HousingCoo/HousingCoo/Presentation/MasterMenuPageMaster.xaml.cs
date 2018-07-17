using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Presentation.Pages;

namespace HousingCoo.Presentation {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenuPageMaster : ContentPage {
        public ListView ListView;

        public MasterMenuPageMaster() {
            InitializeComponent();

            //BindingContext = new MasterMenuPageMasterViewModel();
            //ListView = MenuItemsListView;
        }

        private class MasterMenuPageMasterViewModel : INotifyPropertyChanged {
            public ObservableCollection<NavPageMenuItem> MenuItems { get; set; }

            public Command<object> ItemSelectedCommand;

            public MasterMenuPageMasterViewModel() {
                MenuItems = new ObservableCollection<NavPageMenuItem>(new[]
                {
                    new NavPageMenuItem { Id = 0, Title = "Personal Information", TargetType = typeof(Profile.EditProfilePage) },
                    new NavPageMenuItem { Id = 1, Title = "Cashbox" ,TargetType = typeof(Profile.EditProfilePage) },
                    //new MasterMenuPageMenuItem { Id = 2, Title = "Page 3" },
                    //new MasterMenuPageMenuItem { Id = 3, Title = "Page 4" },
                    //new MasterMenuPageMenuItem { Id = 4, Title = "Page 5" },
                });
                ItemSelectedCommand = new Command<object>(OnItemSelected);
            }

            private void OnItemSelected(object obj) {
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged([CallerMemberName] string propertyName = "") {
                if (PropertyChanged == null) {
                    return;
                }

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}