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
using Firebase.Xamarin.Database.Query;
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

        private User _currentUser = null;

        public User CurrentUser { get { return _currentUser; } }
        protected AppPreferences AppPreferences => new AppPreferences(Android.App.Application.Context);


        public async Task CreateUserAsync(string email, string pwd)
        {
            try
            {

                var firebaseAuthProvider = new FirebaseAuthProvider(FirebaseConfig);
                var firebaseAuthLink = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(email, pwd);
                _currentUser = firebaseAuthLink.User;

                SaveToken(firebaseAuthLink.FirebaseToken);
            }
            catch
            {
                // ignored
            }
        }

        public async Task LoginAsync(string email, string pwd)
        {
            try
            {
                var firebaseAuthProvider = new FirebaseAuthProvider(FirebaseConfig);
                var firebaseAuthLink = await firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, pwd);
                _currentUser = firebaseAuthLink.User;

                SaveToken(firebaseAuthLink.FirebaseToken);
            }
            catch
            {
                // ignored
            }
        }

        public async Task PostAsync(ChatMessage message)
        {
            var token = AppPreferences.GetAccessKey();

            var item = await FirebaseDbCliente
                  .Child("chat")
                  .PostAsync(message);
        }

        public void SaveToken(string firebaseToken)
        {
            AppPreferences.SaveAccessKey(firebaseToken);
        }

        public async Task QueryAsync()
        {
            var items = await FirebaseDbCliente
              .Child("chat")
              .OrderByKey()
              .OnceAsync<ChatMessage>();

            return items;
        }
    }
}