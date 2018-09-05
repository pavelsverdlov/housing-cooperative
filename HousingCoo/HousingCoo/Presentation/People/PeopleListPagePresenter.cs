using HousingCoo.Domain.Interactors;
using HousingCoo.Domain.Model;
using HousingCoo.Presentation.Common;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Presentation;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Infrastructure;
using Xamarin.Presentation.Pages.Tab;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.People {
    public class PeopleListController : BaseController, IItemSelectedController<PeopleVS> {
        internal PeopleListPagePresenter Presenter;
        public Command<PeopleVS> ItemSelectedCommand { get; }

        public Command AddNewPerson { get; }
        public Command<PeopleVS> RemovePerson { get; }


        public PeopleListController() {
            ItemSelectedCommand = new Command<PeopleVS>(OnItemSelected);
            AddNewPerson = new Command(OnAddNewPerson);
            RemovePerson = new Command<PeopleVS>(OnRemovePerson);
        }

        void OnRemovePerson(PeopleVS vs) {
            Presenter.Remove(vs);
        }

        private void OnAddNewPerson(object obj) {
            Presenter.OpenEditProfile();
        }

        private void OnItemSelected(PeopleVS vs) {
            Presenter.ShowPeoplePreview(vs);
        }
    }
    public class PeopleListViewState : CollectionViewState<PeopleVS> {
        public PeopleListViewState() {
           
        }
    }

    public class PeopleVS : PeopleViewState {
        public PeopleModel Model { get; set; }
    }

    public class PeopleListPagePresenter : HousingDefCollectionPresenter<PeopleListViewState, PeopleListController, PeopleVS>,
        IPeopleListConsumer, ITabPageNavigatorUpdating {
        readonly IPeopleListProducer producer;
        readonly IPeopleRemove repo;

        public PeopleListPagePresenter() : this(
            Bootstrapper.Instance.Resolver.Get<IPeopleListProducer>(),
            Bootstrapper.Instance.Resolver.Get<IPeopleRemove>()) { }
        public PeopleListPagePresenter(IPeopleListProducer producer, IPeopleRemove repo) {
            PageNavigator.Title = "People";
            PageNavigator.IconSource = StaticResources.Icons.PeopleWhite;

            producer.Receive(this);
            this.producer = producer;
            this.repo = repo;
        }
        //<ToolbarItem Icon="baseline_person_add_white_24dp.png" Command="{Binding Source={x:Reference Name=page}, Path=BindingContext.Controller.AddNewPerson}" />
        protected override void Init(PeopleListViewState vs, PeopleListController con) {
            base.Init(vs, con);
            con.Presenter = this;
        }

        protected override void OnListRefreshed() {
            producer.Receive(this);
        }

        internal async void OpenEditProfile() {
            try {
                Profile.EditProfilePresenter pres = await commutator.GoToPage<Profile.EditProfilePresenter>(PageNavigator.Navigation);
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }

        internal async void ShowPeoplePreview(PeopleVS vs) {
            try {
                PreviewPeoplePresenter pres = await commutator.GoToPage<PreviewPeoplePresenter>(PageNavigator.Navigation);
                pres.ShowPeople(vs);
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }


        public void OnReceived(IEnumerable<PeopleModel> people) {
            people.ForEach(x => ViewState.ViewCollection.Add(
                new PeopleVS {
                    Model = x,
                    IconSource = "person.png",
                    Name = x.Name,
                    Info = "some info" }));
        }

        internal void Remove(PeopleVS vs) {
            repo.Remove(vs.Model);
        }

        public void NavigatorPageChanged() {
            DispatcherEx.BeginRise(() => {
                PageNavigator.ToolbarMenu.Clear();
                PageNavigator.ToolbarMenu.Add(new ToolbarItem() {
                  Icon = "baseline_person_add_white_24dp.png",
                  Command = Controller.AddNewPerson
                });
            });
        }
    }
}
