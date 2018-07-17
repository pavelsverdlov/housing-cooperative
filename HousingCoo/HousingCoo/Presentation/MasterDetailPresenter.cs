using HousingCoo.Presentation.Profile;
using System;
using System.Collections.Generic;
using Xamarin.Presentation;
using Xamarin.Presentation.Navigation;
using Xamarin.Presentation.Pages;

namespace HousingCoo.Presentation {
    public class MasterDetailPresenter : Xamarin.Presentation.Pages.MasterDetailPresenter {
        readonly IXamLogger logger;
        readonly ICommutator commutator;

        public MasterDetailPresenter() : this(
          Bootstrapper.Instance.Resolver.Get<IXamLogger>(),
          Bootstrapper.Instance.Resolver.Get<ICommutator>()) { }
        public MasterDetailPresenter(IXamLogger logger, ICommutator commutator) {
            this.logger = logger;
            this.commutator = commutator;
            Page.IsPresented = false;
            Page.Title = "Housing Cooperative";
        }

        protected override void Init(MasterDetailViewState vs, MasterDetailController con) {
            base.Init(vs, con);

            var items = new List<NavPageMenuItem>(new[]                {
                    new NavPageMenuItem { Id = 0, Title = "Personal Information", TargetType = typeof(Profile.EditProfilePage) },
                    new NavPageMenuItem { Id = 1, Title = "Cashbox" ,TargetType = typeof(Profile.EditProfilePage) },
                    //new MasterMenuPageMenuItem { Id = 2, Title = "Page 3" },
                    //new MasterMenuPageMenuItem { Id = 3, Title = "Page 4" },
                    //new MasterMenuPageMenuItem { Id = 4, Title = "Page 5" },
                });

            vs.Push((nameof(MasterDetailViewState.MenuItems), items));
        }

        public override async void OnMenuSelected(NavPageMenuItem item) {
            try {
                Page.IsPresented = false;
                EditProfilePresenter pres = await commutator.GoToPage<EditProfilePresenter>(Page.Navigation);
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }
    }
}
