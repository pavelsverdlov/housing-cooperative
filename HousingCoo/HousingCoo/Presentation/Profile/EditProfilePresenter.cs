using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Presentation.Pages;

namespace HousingCoo.Presentation.Profile {
    public class EditProfilePresenter : IPageNavigatorSupporting {
        public IPageNavigator PageNavigator { get; }
        public EditProfilePresenter() {
            PageNavigator = new PageNavigatorViewModel {
                Title = "Edit profile"
            };
        }
    }
}
