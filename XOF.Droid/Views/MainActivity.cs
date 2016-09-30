using Android.App;
using Android.OS;

namespace XOF.Droid.Views
{
    [Activity(Label = "XOF.Droid", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.ActivityMain);
        }
    }
}

