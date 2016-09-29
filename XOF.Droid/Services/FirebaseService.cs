using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Streaming;
using XOF.Droid.Constants;
using XOF.Droid.Models;

namespace XOF.Droid.Services
{
    public class FirebaseService
    {
        private FirebaseService _instance = null;
        public FirebaseService Instance => _instance ?? new FirebaseService();

        private  FirebaseConfig FirebaseConfig => new FirebaseConfig(AppSettings.ApiKey);
        private FirebaseClient FirebaseDbCliente => new FirebaseClient(AppSettings.DbUrl);

        public async Task CreateUserAsync(string email, string pwd)
        {
            var firebaseAuthProvider = new FirebaseAuthProvider(FirebaseConfig);
            var firebaseAuthLink = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(email, pwd);
        }

        public async Task PostAsync(ChatMessage message)
        {
            var item = await FirebaseDbCliente
                  .Child("yourentity")
                  //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                  .PostAsync(message);
        }
    }
}