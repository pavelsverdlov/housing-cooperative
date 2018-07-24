using HousingCoo.Domain.Interactors;
using HousingCoo.Domain.Model;
using HousingCoo.Presentation.Common;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Presentation;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Infrastructure;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.People {
    public class PeopleListController : BaseController, IItemSelectedController<PeopleViewState> {
        internal PeopleListPagePresenter Presenter;
        public Command<PeopleViewState> ItemSelectedCommand { get; }

        public Command AddNewPerson { get; }
        public Command<PeopleViewState> RemovePerson { get; }


        public PeopleListController() {
            ItemSelectedCommand = new Command<PeopleViewState>(OnItemSelected);
            AddNewPerson = new Command(OnAddNewPerson);
            RemovePerson = new Command<PeopleViewState>(OnRemovePerson);
        }

        void OnRemovePerson(PeopleViewState obj) {
            
        }

        private void OnAddNewPerson(object obj) {
            Presenter.OpenEditProfile();
        }

        private void OnItemSelected(PeopleViewState vs) {
            Presenter.ShowPeoplePreview(vs);
        }
    }
    public class PeopleListViewState : CollectionViewState<PeopleViewState> {
        public PeopleListViewState() {
           
        }
    }
    public class PeopleListPagePresenter : HousingDefCollectionPresenter<PeopleListViewState, PeopleListController, PeopleViewState>,
        IPeopleListConsumer {
        readonly IPeopleListProducer producer;

        public PeopleListPagePresenter() : this(Bootstrapper.Instance.Resolver.Get<IPeopleListProducer>()) { }
        public PeopleListPagePresenter(IPeopleListProducer producer) {
            PageNavigator.Title = "People";
            PageNavigator.IconSource = StaticResources.Icons.PeopleWhite;
            producer.Receive(this);
            this.producer = producer;
        }
     
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

        internal async void ShowPeoplePreview(PeopleViewState vs) {
            try {
                PreviewPeoplePresenter pres = await commutator.GoToPage<PreviewPeoplePresenter>(PageNavigator.Navigation);
                pres.ShowPeople(vs);
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }


        public void OnReceived(IEnumerable<PeopleModel> people) {
            people.ForEach(x => ViewState.ViewCollection.Add(new PeopleViewState { IconSource = "person.png", Name = x.Name, Info = "some info" }));
        }

    }
}
