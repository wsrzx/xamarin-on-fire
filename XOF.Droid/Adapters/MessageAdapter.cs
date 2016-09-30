using System;
using System.Collections.ObjectModel;
using Android.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using XOF.Droid.Models;

namespace XOF.Droid
{
    public class MessageAdapter : BaseAdapter
    {
        private ObservableCollection<ChatMessage> _messages;
        private Activity _activity;


        public MessageAdapter(Activity activity, ObservableCollection<ChatMessage> messages)
        {
            _activity = activity;
            _messages = messages;
        }

        public override int Count
        {
            get
            {
                return _messages.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.ListViewItemMessage, parent, false);

            var messageText = view.FindViewById<TextView>(Resource.Id.textView_message);
            var messageUser = view.FindViewById<TextView>(Resource.Id.textView_username);

            messageText.Text = _messages[position].Text;
            messageUser.Text = _messages[position].UserName;

            return view;
        }
    }
}
