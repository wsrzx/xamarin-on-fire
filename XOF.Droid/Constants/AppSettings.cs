using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XOF.Droid.Constants
{
    /// <summary>
    /// Firebase settings
    /// </summary>
    public class AppSettings
    {
        public static readonly string ApiKey = "<firebase_api_key>";
        public static readonly string AuthDomain = "<firebase_auth_domain>.firebaseapp.com";
        public static readonly string DbUrl = "https://<firebase_db>.firebaseio.com";
        public static readonly string StorageBucket = "<storage_bucket>.appspot.com";
        public static readonly string MessageSenderId = "<message_sender_id>";
        public static readonly string ProjectId = "<project_id>";
        public static readonly string FirebaseSecret = "<firebase_secret>";
    }
}