using System;

using HousingCoo.Models;
using HousingCoo.Views;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ActivityViewState Item { get; set; }
        public ItemDetailViewModel(ActivityViewState item = null)
        {
            Title = item?.Body;
            Item = item;
        }
    }
}
