using System;
using Android.App;
using Android.OS;
using Android.Widget;
using XOF.Droid.Models;
using XOF.Droid.Views.Base;

namespace XOF.Droid.Views
{
    [Activity(Label = "XOF.Droid", Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class ChatActivity : BaseActivity
    {
        private Button _butEnviar;
        private EditText _edtMensagem;
        private ListView _lstChat;
        System.IObservable incomingAppleShipment;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ActivityChat);

            _butEnviar = FindViewById<Button>(Resource.Id.butEnviar);
            _edtMensagem = FindViewById<EditText>(Resource.Id.edtMensagem);
            _lstChat = FindViewById<ListView>(Resource.Id.lstChat);

            _butEnviar.Click += async (sender, e) =>
            {
                var userName = "";
                var message = new ChatMessage { Text = _edtMensagem.Text, UserName = userName };

                await _firebaseService.PostAsync(message);

                _edtMensagem.Text = string.Empty;
            };
        }
    }
}

