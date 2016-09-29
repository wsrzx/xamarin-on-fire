using Android.App;
using Android.Widget;
using Android.OS;

namespace XOF.Droid
{
    [Activity(Label = "XOF.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme")]
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

