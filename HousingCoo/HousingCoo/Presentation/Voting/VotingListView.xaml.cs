using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HousingCoo.Presentation.Voting
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VotingListView : ContentView
	{
		public VotingListView ()
		{
			InitializeComponent ();
		}
	}
}