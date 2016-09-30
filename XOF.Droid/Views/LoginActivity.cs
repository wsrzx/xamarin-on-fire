using Android.App;
using Android.OS;
using Android.Widget;
using XOF.Droid.Services;
using XOF.Droid.Views.Base;

namespace XOF.Droid.Views
{
    [Activity(Label = "XOF.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class LoginActivity : BaseActivity
    {
        private Button _loginButton, _registerButton;
        private EditText _passwordEditText, _emailEditText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ActivityLogin);

            _loginButton = FindViewById<Button>(Resource.Id.email_sign_in_button);
            _registerButton = FindViewById<Button>(Resource.Id.register);
            _passwordEditText = FindViewById<EditText>(Resource.Id.password);
            _emailEditText = FindViewById<EditText>(Resource.Id.email);

            _loginButton.Click += async (sender, args) =>
            {
                await _firebaseService.LoginAsync(_emailEditText.Text, _passwordEditText.Text);

                if (_firebaseService.CurrentUser != null)
                {
                    StartActivity(typeof(ChatActivity));
                }
                else
                    ShowToast(GetString(Resource.String.auth_failed));
            };

            _registerButton.Click += async (sender, args) =>
            {
                await _firebaseService.CreateUserAsync(_emailEditText.Text, _passwordEditText.Text);

                if (_firebaseService.CurrentUser != null)
                {
                    ShowToast(GetString(Resource.String.register_success));
                    StartActivity(typeof(ChatActivity));
                }
                else
                {
                    ShowToast(GetString(Resource.String.register_fail));
                }
            };
        }
    }
}

