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
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ActivityChat);

            _butEnviar = FindViewById<Button>(Resource.Id.butEnviar);
            _edtMensagem = FindViewById<EditText>(Resource.Id.edtMensagem);
            _lstChat = FindViewById<ListView>(Resource.Id.lstChat);

            var messages = await _firebaseService.QueryAsync();
            var adapter = new MessageAdapter(this, messages);
            _lstChat.Adapter = adapter;
            
            _butEnviar.Click += async (sender, e) =>
            {
                var userName = _firebaseService.CurrentUser.Email;
                var message = new ChatMessage
                {
                    Text = _edtMensagem.Text,
                    UserName = userName
                };

                await _firebaseService.PostAsync(message);

                _edtMensagem.Text = string.Empty;
                adapter.Messages = await _firebaseService.QueryAsync();
                adapter.NotifyDataSetChanged();

                _lstChat.SmoothScrollToPosition(adapter.Count);

            };
        }
    }
}

