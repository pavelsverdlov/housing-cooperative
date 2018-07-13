using HousingCoo.Presentation.Voting;
using HousingCoo.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage {
        private ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel) {
            this.viewModel = viewModel;
            InitializeComponent();

            //BindingContext = this.viewModel = viewModel;

            var vm = new VotingDetailPresenter();

            vm.HeaderViewModel.ViewState.Push(
                (nameof(ActivityViewState.ActorName), viewModel.Item.ActorName),
                (nameof(ActivityViewState.Body), viewModel.Item.Body),
                (nameof(ActivityViewState.Dates), viewModel.Item.Dates),
                (nameof(ActivityViewState.Verb), viewModel.Item.Verb)
            );


            vm.DetailViewModel.ViewState.Comments = new System.Collections.ObjectModel.ObservableCollection<MessageViewState> {
                    new MessageViewState{ IconSource="latenightseth.png", Title = "Vasiliy Pupkin", Message ="Agreed!"},
                    new MessageViewState{ IconSource="latenightseth.png", Title = "Vasiliy Pupkin 2",
                        Message ="mageSource property. If set, if error occurs while loading image, an error placeholder is shown. It supports UriImageSource, FileImageSource and StreamImageSource."},
                    new MessageViewState{ IconSource="latenightseth.png", Title = "Vasiliy Pupkin 3", Message ="Agreed!"},
                };

            BindingContext = vm;
        }

        public ItemDetailPage() {
            InitializeComponent();
        }
    }

   
    
}









//
public class FeedViewModel {


    public FeedViewModel() {
        // Example Data
        InstagramFeed = new List<Data>()
        {
                new Data()
                {
                    ViewCount = 10517,
                    Comment = "latenightseth Enjoy your well-deserved vacation, Mr President. #LNSM",
                    CommentCount = 45,
                    Image = "latenightsethfeedimage.jpg",
                    Name = "latenightseth",
                    PostedAt = "1 hour ago"
                },
                   new Data()
                {
                    ViewCount = 10517,
                    Comment = "latenightseth Enjoy your well-deserved vacation, Mr President. #LNSM",
                    CommentCount = 45,
                    Image = "latenightsethfeedimage.jpg",
                    Name = "latenightseth",
                    PostedAt = "1 hour ago"
                }


            };

    }

    public List<Data> InstagramFeed { get; set; }

}


public class Data {
    public string Name { get; set; }
    public string Image { get; set; }
    public string Comment { get; set; }
    public int CommentCount { get; set; }
    public string PostedAt { get; set; }
    public int ViewCount { get; set; }
}


public class Facebook {
    public ICommand TappedCommand { get; set; }
    public Facebook() {
        TappedCommand = new Command();
    }

    private class Command : ICommand {
        public event EventHandler CanExecuteChanged = (x, y) => { };

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {

        }
    }

}