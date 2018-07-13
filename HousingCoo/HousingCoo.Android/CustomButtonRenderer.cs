using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using HousingCoo;
//using Android.Widget;
using HousingCoo.Droid;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(CustomButtonRenderer))]
namespace HousingCoo.Droid {
    //public class CustomButtonRenderer : Xamarin.Forms.Platform.Android.ButtonRenderer {
    //    public CustomButtonRenderer(Context context) : base(context) {
    //    }
    //}

    public class CustomButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer {
        public CustomButtonRenderer(Context context) : base(context) {
        }

        protected override AppCompatButton CreateNativeControl() {
            var context = new ContextThemeWrapper(Context, Resource.Style.Widget_AppCompat_Button_Borderless);
            var button = new AppCompatButton(context, null, Resource.Style.Widget_AppCompat_Button_Borderless);

            button.SetPadding(0, 0, 0, 0);
            button.SetLineSpacing(0, 0);
            button.SetMinWidth(1);
            button.SetMinWidth(1);
            button.SetMinimumHeight(1);
            button.SetMinimumWidth(1);
            button.SetPaddingRelative(0,0,0,0);
            //button.Set(0,0,0,0);


            return button;
        }
    }
}