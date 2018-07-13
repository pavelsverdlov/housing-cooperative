using HousingCoo.Models;
using HousingCoo.Services;
using HousingCoo.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.ViewModels {
    public class ItemsViewModel : BaseViewModel {
        public ObservableCollection<ActivityViewState> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        private readonly IDataStore<Item> DataStore;
        public ItemsViewModel() : this(DependencyService.Get<IDataStore<Item>>()) {

        }
        public ItemsViewModel(IDataStore<Item> ds) {
            DataStore = ds;
            Title = "Voiting";
            Items = new ObservableCollection<ActivityViewState>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, ActivityViewState>(this, "AddItem", async (obj, item) => {
                var _item = item as ActivityViewState;
                Items.Add(_item);
                await DataStore.AddItemAsync(new Models.Item { Text = _item.ActorName, Description = _item.Body });
            });
        }

        private async Task ExecuteLoadItemsCommand() {
            if (IsBusy) {
                return;
            }

            IsBusy = true;

            try {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items) {
                    Items.Add(new ActivityViewState {
                        ActorName = item.Text,
                        Verb = item.Text,
                        Body = "Для поездки необходимо здать кучу денег в размере 999$ с человека.",

                        Dates = new ActivityDatesState {
                            DateCreated = DateTime.Now.ToString("MM/dd HH:mm"),
                            DateClosed = DateTime.Now.ToString("MM/dd HH:mm"),
                        },
                    });
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            } finally {
                IsBusy = false;
            }
        }
    }
}