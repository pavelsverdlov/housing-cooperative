using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Presentation;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Infrastructure;
using Xamarin.Presentation.Navigation;
using Xamarin.Presentation.Pages;

namespace HousingCoo.Presentation.Common {
    public abstract class HousingDefCollectionPresenter<TViewState, TController, TCollectionViewItem> : 
        DefaultCollectionPresenter<TViewState, TController, TCollectionViewItem>
       where TViewState : CollectionViewState<TCollectionViewItem>, new()
       where TController : BaseController, IItemSelectedController<TCollectionViewItem>, new() {

        protected HousingDefCollectionPresenter() : base(
             Bootstrapper.Instance.Resolver.Get<IXamLogger>(),
             Bootstrapper.Instance.Resolver.Get<ICommutator>()) { }
    }
}
