using System.Collections.Generic;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social.Profiles;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.People {

    public class PreviewPeopleController : BaseController {
    }

    public class PreviewPeopleViewState : BaseViewState, IProfilePreviewViewState {
        public string IconSource { get; set; }
        public string ProfileName { get; set; }

        public List<ProfilePeopertyItem> ProfileProperties { get; set; }
        public PreviewPeopleViewState() {
            ProfileProperties = new List<ProfilePeopertyItem>();
        }
    }


    public class PreviewPeoplePresenter : BasePresenter<PreviewPeopleViewState, PreviewPeopleController>, IPageNavigatorSupporting {
        public IPageNavigator PageNavigator { get; }

        public PreviewPeoplePresenter() {
            PageNavigator = new PageNavigatorAdapter  { Title = "People" };
        }

        public void ShowPeople(PeopleViewState vs) {
            var list = new List<ProfilePeopertyItem> { new ProfilePeopertyItem() { Property = "Age", Value = "18" } };
            ViewState.Push(
                (nameof(PreviewPeopleViewState.ProfileProperties), list),
                (nameof(PreviewPeopleViewState.IconSource), vs.IconSource),
                (nameof(PreviewPeopleViewState.ProfileName), vs.Name)
                //(nameof(PreviewPeopleViewState.), vs.Info)
                );
            PageNavigator.Title = vs.Name;
        }
    }



}