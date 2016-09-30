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
using User = Firebase.Xamarin.Auth.User;

namespace XOF.Droid.Services
{
    public class FirebaseService
    {
        private static FirebaseService _instance = null;

        public static FirebaseService Instance => _instance ?? new FirebaseService();

        private FirebaseConfig FirebaseConfig => new FirebaseConfig(AppSettings.ApiKey);
        private FirebaseClient FirebaseDbCliente => new FirebaseClient(AppSettings.DbUrl);

        public User CurrentUser { get; set; } = null;

        public async Task CreateUserAsync(string email, string pwd)
        {
            var firebaseAuthProvider = new FirebaseAuthProvider(FirebaseConfig);
            var firebaseAuthLink = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(email, pwd);
        }

        public async Task LoginAsync(string email, string pwd)
        {
            try
            {
                var firebaseAuthProvider = new FirebaseAuthProvider(FirebaseConfig);
                var firebaseAuthLink = await firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, pwd);
                CurrentUser = firebaseAuthLink.User;
            }
            catch
            {
                // ignored
            }
        }

        public async Task PostAsync(ChatMessage message)
        {
            var item = await FirebaseDbCliente
                  .Child("chatMessage")
                  //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                  .PostAsync(message);
        }

      
    }
}