using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Presentation.Framework.VSVVM;

namespace HousingCoo.Presentation.Common {
    public interface IItemSelectedController<TItem> {
        Command<TItem> ItemSelectedCommand { get; }
    }
    public class CollectionViewState<TItem> : BaseViewState {
        public List<TItem> ViewCollection { get; set; }
        public CollectionViewState() {
            ViewCollection = new List<TItem>();
        }
    }
}
